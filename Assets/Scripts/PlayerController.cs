using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;

    private float applySpeed;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private float lookSensitivity;

    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX = 0;

    private bool isWalk = false;
    private bool isRun = false;
    private bool isGround = true;
    private bool isAttack = false;
    public bool isGuard = false;

    private Vector3 movement = new Vector3();

    [SerializeField]
    private Camera theCamera;
    private Rigidbody myRigid;
    private CapsuleCollider capsuleCollider;
    private StatusController statusController;
    private Animator animator;

    private void Start()
    {
        myRigid = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        statusController = GetComponent<StatusController>();
        animator = GetComponent<Animator>();
        applySpeed = walkSpeed;
    }

    private void Update()
    {
        AttackCheck();
        IsGround();
        TryJump();
        TryRun();
        TryGuard();
    }

    private void FixedUpdate()
    {
        Move();
        CharacterRotation();
        MoveCheck();
    }

    private void IsGround()
    {
        Vector3 _position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        isGround = Physics.Raycast(_position, Vector3.down, capsuleCollider.bounds.extents.y + 0.2f);
    }

    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround && statusController.GetCurrentSP() > 0)
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (isGuard) { Guard(); }
        statusController.DecreaseStamina(20);
        myRigid.velocity = transform.up * jumpForce;
    }

    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift) && statusController.GetCurrentSP() > 0 && !isAttack)
        {
            Running();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || statusController.GetCurrentSP() <= 0 || isAttack)
        {
            RunningCancel();
        }
    }

    private void Running()
    {
        if (isGuard) { Guard(); }
        isRun = true;
        animator.SetBool("isRun", true);
        statusController.DecreaseStamina(30 * Time.deltaTime);
        applySpeed = runSpeed;
    }

    private void RunningCancel()
    {
        isRun = false;
        animator.SetBool("isRun", false);
        applySpeed = walkSpeed;
    }

    private void TryGuard()
    {
        if (Input.GetKey(KeyCode.Mouse1) && statusController.GetCurrentSP() > 0 && !isAttack)
        {
            Guard();
        }
        if (Input.GetKeyUp(KeyCode.Mouse1) || statusController.GetCurrentSP() <= 0 || isAttack)
        {
            GuardCancel();
        }
    }

    private void Guard()
    {
        isWalk = false;
        applySpeed = 0;
        isGuard = true;

        animator.SetBool("isGuard", true);
        statusController.DecreaseStamina(50 * Time.deltaTime);
        statusController.SetDP(100);
    }

    private void GuardCancel()
    {
        isGuard = false;
        animator.SetBool("isGuard", false);
        applySpeed = walkSpeed;
        statusController.SetDP(0);
    }

    private void Move()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * movement.x;
        Vector3 _moveVertical = transform.forward * movement.z;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    private void MoveCheck()
    {
        if (!isRun && !isGuard && isGround)
        {
            if (Mathf.Approximately(movement.x, 0) && Mathf.Approximately(movement.z, 0))
                isWalk = false;
            else
                isWalk = true;
            animator.SetBool("isWalk", isWalk);
        }
        animator.SetFloat("xDir", movement.x);
        animator.SetFloat("zDir", movement.z);
    }

    private void CharacterRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }

    private void AttackCheck()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        isAttack = stateInfo.IsName("Attack_1") || stateInfo.IsName("Attack_2") || stateInfo.IsName("Attack_3");
    }
}
