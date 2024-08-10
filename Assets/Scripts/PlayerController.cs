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

    // 카메라 한계
    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX = 0;

    private bool isWalk = false;
    private bool isRun = false;
    private bool isGuard = false;
    private bool isGround = true;

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
        IsGround();
        TryJump();
        TryRun();
    }

    private void FixedUpdate()
    {
        TryGuard();
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
        if (Input.GetKey(KeyCode.LeftShift) && statusController.GetCurrentSP() > 0)
        {
            Running();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || statusController.GetCurrentSP() <= 0)
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
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Guard();
        }
    }

    private void Guard()
    {
        if (isWalk)
        {
            isWalk = false;
        }

        isGuard = !isGuard;

        if (isGuard)
        {
            Debug.Log("가드 상태");
        }
        else
        {
            Debug.Log("비가드 상태");
        }

        //StartCoroutine(GuardCoroutine());
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
            Debug.Log(isWalk); // 디버그.
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

    private void ResetState()
    {

    }
}
