using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float rotationSpeed = 10f;
    [SerializeField]
    private float attackRange = 7f;
    [SerializeField]
    private int monsterNum;

    [SerializeField] private NightmareAttackController nightmare;
    [SerializeField] private UsurperAttackController usurper;

    private Vector3 directionToPlayer;
    private Vector3 distanceToPlayer;

    private bool isMove = true;
    public bool isDead = false;

    public int patternNum;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        TargetToPlayer();
    }

    private void TargetToPlayer()
    {
        if (playerTransform == null)
        {
            Debug.Log("플레이어 탐색불가");
            return;
        }

        if (isMove && !isDead)
        {
            directionToPlayer = playerTransform.position - transform.position;
            directionToPlayer.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            Vector3 moveDirection = directionToPlayer.normalized;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            float distanceToPlayer = directionToPlayer.magnitude;

            animator.SetBool("isMove", true);

            if (distanceToPlayer <= attackRange)
            {
                isMove = false;
                animator.SetBool("isMove", false);
                BossPattern();
            }
        }
    }

    private void BossPattern()
    {
        if (monsterNum == 0)
            nightmare.RandPattern();
        else if (monsterNum == 1)
            nightmare.RandPattern();
        else if (monsterNum == 2)
            nightmare.RandPattern();
        else if (monsterNum == 3)
            usurper.RandPattern();
    }

    private void TargetResearch()
    {
        directionToPlayer = playerTransform.position - transform.position;
        directionToPlayer.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public IEnumerator PatternDelay(float _count)
    {
        yield return new WaitForSeconds(_count);
        animator.SetBool("isMove", true);
        for (int i = 0; i < 100; i++)
        {
            if (!isDead)
                TargetResearch();
            yield return new WaitForSeconds(0.01f);
        }
        isMove = true;
    }

    public void Die()
    {
        animator.SetTrigger("Dead");
        isDead = true;
        GameManager.Instance.clearUI.SetActive(true);
    }

    public int GetMonsterNum()
    {
        return monsterNum;
    }

    public float GetRotationSpeed()
    {
        return rotationSpeed; 
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
}
