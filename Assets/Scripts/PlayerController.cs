using System.Collections;
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

    private bool isWalk = false;
    private bool isRun = false;
    private bool isGround = true;
    public bool isAttack = false;
    private bool isHit = false;
    public bool usingPotion = false;
    public bool isGuard = false;

    private Vector3 movement = new Vector3();
    private Vector3 velocity = new Vector3();

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
        if (!GameManager.Instance.isStop)
        {
            AttackCheck();
            IsGround();
            TryJump();
            TryRun();
            TryGuard();
        }
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.isStop)
        {
            Move();
            CharacterRotation();
        }
        MoveCheck();
    }

    private void IsGround()
    {
        Vector3 _position = new Vector3(transform.position.x, transform.position.y + 0.001f, transform.position.z);
        isGround = Physics.Raycast(_position, Vector3.down, capsuleCollider.bounds.extents.y + 0.001f);
    }

    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround && statusController.GetCurrentSP() > 0 && !usingPotion)
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (isGuard) { Guard(); }
        statusController.DecreaseStamina(40);
        myRigid.velocity = transform.up * jumpForce;
    }

    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift) && statusController.GetCurrentSP() > 0 && !isAttack && !usingPotion)
        {
            Running();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || statusController.GetCurrentSP() <= 0 || isAttack || usingPotion)
        {
            RunningCancel();
        }
    }

    private void Running()
    {
        if (isGuard) { Guard(); }
        isRun = true;
        animator.SetBool("isRun", true);
        statusController.DecreaseStamina(50 * Time.deltaTime);
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
        if (Input.GetKey(KeyCode.Mouse1) && statusController.GetCurrentSP() > 0 && !isAttack && !isHit && !usingPotion)
        {
            Guard();
        }
        if (Input.GetKeyUp(KeyCode.Mouse1) || statusController.GetCurrentSP() <= 0 || isAttack || isHit || usingPotion)
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
        statusController.SetDP(40);
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
        if (!isHit)
        {   
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.z = Input.GetAxisRaw("Vertical");

            Vector3 _moveHorizontal = transform.right * movement.x;
            Vector3 _moveVertical = transform.forward * movement.z;

            velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;
            myRigid.MovePosition(transform.position + velocity * Time.deltaTime);
        }
    }

    private void MoveCheck()
    {
        if (!isRun && !isGuard && isGround && !isAttack && !isHit)
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
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity * Time.deltaTime;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }

    private void AttackCheck()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        isAttack = stateInfo.IsName("Attack_1") || stateInfo.IsName("Attack_2") || stateInfo.IsName("Attack_3");
    }

    public IEnumerator HitCoroutine()
    {
        if (isHit == false)
        {
            isHit = true;
            Debug.Log(isHit);
            yield return new WaitForSeconds(1f);
            isHit = false;
        }
    }
}
