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
    private float[] patternDelay;

    private Vector3 directionToPlayer;
    private Vector3 distanceToPlayer;

    private bool isMove = true;
    private bool isDead = false;

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

            if (distanceToPlayer <= 7f)
            {
                isMove = false;
                animator.SetBool("isMove", false);
                RandBossPattern();
            }
        }
    }

    private void TargetResearch()
    {
        directionToPlayer = playerTransform.position - transform.position;
        directionToPlayer.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void RandBossPattern()
    {
        patternNum = Random.Range(0, 3);

        switch (patternNum)
        {
            case 0:
                BossPattern1();
                break;
            case 1:
                BossPattern2();
                break;
            case 2:
                BossPattern3();
                break;
        }
    }

    private void BossPattern1()
    {
        Debug.Log("물기 공격");
        animator.SetInteger("PatternNum", 1);
        animator.SetTrigger("StartAttack");
        StartCoroutine(PatternDelay(patternDelay[0]));
    }

    private void BossPattern2()
    {
        Debug.Log("발톱 공격");
        animator.SetInteger("PatternNum", 2);
        animator.SetTrigger("StartAttack");
        StartCoroutine(PatternDelay(patternDelay[1]));
    }

    private void BossPattern3()
    {
        Debug.Log("박치기 공격");
        animator.SetInteger("PatternNum", 3);
        animator.SetTrigger("StartAttack");
        StartCoroutine(PatternDelay(patternDelay[2]));
    }

    private IEnumerator PatternDelay(float _count)
    {
        yield return new WaitForSeconds(_count);
        animator.SetBool("isMove", true);
        for (int i = 0; i < 100; i++)
        {
            TargetResearch();
            yield return new WaitForSeconds(0.01f);
        }
        isMove = true;
    }

    public void Die()
    {
        Debug.Log(gameObject.name + " died.");
        animator.SetTrigger("Dead");
        isDead = true;
    }
}
