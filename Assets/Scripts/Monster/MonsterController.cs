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
    private float[] patternDelay;

    private Vector3 directionToPlayer;
    private Vector3 distanceToPlayer;

    private bool isMove = true;
    private bool isDead = false;
    private bool canSkill = false;
    public bool doSkill = false;

    public int patternNum;

    private Animator animator;

    void Start()
    {
        canSkill = true;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Debug.Log(canSkill);
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
                if (canSkill)
                    BossSkill();
                else
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

    private void BossSkill()
    {
        animator.SetTrigger("BossSkill");
        doSkill = true;
        StartCoroutine(PatternDelay(patternDelay[3]));
        StartCoroutine(SkillDelay());
    }

    private void BossPattern1()
    {
        animator.SetInteger("PatternNum", 1);
        animator.SetTrigger("StartAttack");
        StartCoroutine(PatternDelay(patternDelay[0]));
    }

    private void BossPattern2()
    {
        animator.SetInteger("PatternNum", 2);
        animator.SetTrigger("StartAttack");
        StartCoroutine(PatternDelay(patternDelay[1]));
    }

    private void BossPattern3()
    {
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

    private IEnumerator SkillDelay()
    {
        yield return new WaitForSeconds(5f);
        canSkill = false;
        doSkill = false;
        yield return new WaitForSeconds(60f);
        canSkill = true;
    }
}
