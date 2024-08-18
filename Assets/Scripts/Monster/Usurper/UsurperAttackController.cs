using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsurperAttackController : MonoBehaviour
{
    [SerializeField]
    private float[] patternDelay;

    private bool isMove = true;
    private bool canSkill = true;
    public bool doSkill = false;
    public bool isBreath = false;
    public bool isSkillBreath = false;

    public int patternNum;

    private float rotationSpeed;
    private float moveSpeed;

    private Vector3 directionToPlayer;
    private Vector3 distanceToPlayer;

    [SerializeField]
    private Transform playerTransform;
    private Animator animator;
    private MonsterController monsterController;

    void Start()
    {
        animator = GetComponent<Animator>();
        monsterController = GetComponent<MonsterController>();
        rotationSpeed = monsterController.GetRotationSpeed();
        moveSpeed = monsterController.GetMoveSpeed();
    }

    public void RandPattern()
    {
        patternNum = Random.Range(0, 3);

        switch (patternNum)
        {
            case 0:
                if (canSkill)
                    UsurperSkill();
                else
                    UsurperPattern1();
                break;
            case 1:
                UsurperPattern2();
                break;
            case 2:
                UsurperPattern3();
                break;
        }
    }

    private void UsurperSkill()
    {
        animator.SetTrigger("BossSkill");
        doSkill = true;
        StartCoroutine(monsterController.PatternDelay(patternDelay[3]));
        StartCoroutine(SkillDelay());
    }

    private void UsurperPattern1()
    {
        StartCoroutine(UsurperPattern1_Setdistance());
    }

    private void UsurperPattern2()
    {
        animator.SetInteger("PatternNum", 2);
        animator.SetTrigger("StartAttack");
        StartCoroutine(monsterController.PatternDelay(patternDelay[1]));
    }

    private void UsurperPattern3()
    {
        StartCoroutine(UsurperPattern3_Setdistance());
    }

    private IEnumerator SkillDelay()
    {
        yield return new WaitForSeconds(15f);
        canSkill = false;
        doSkill = false;
        yield return new WaitForSeconds(60f);
        canSkill = true;
    }

    private IEnumerator UsurperPattern1_Setdistance()
    {
        animator.SetBool("isRun", true);

        while (true)
        {
            directionToPlayer = playerTransform.position - transform.position;
            directionToPlayer.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            Vector3 moveDirection = directionToPlayer.normalized;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.position += moveDirection * moveSpeed * 2 * Time.deltaTime;

            float distanceToPlayer = directionToPlayer.magnitude;

            if (distanceToPlayer <= 14f)
            {
                isMove = false;
                animator.SetBool("isRun", false);
                animator.SetInteger("PatternNum", 1);
                animator.SetTrigger("StartAttack");
                StartCoroutine(monsterController.PatternDelay(patternDelay[0]));
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator UsurperPattern3_Setdistance()
    {
        animator.SetBool("isRun", true);

        while (true)
        {
            directionToPlayer = playerTransform.position - transform.position;
            directionToPlayer.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            Vector3 moveDirection = directionToPlayer.normalized;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.position -= moveDirection * moveSpeed * 2 * Time.deltaTime;

            float distanceToPlayer = directionToPlayer.magnitude;

            if (distanceToPlayer >= 25f)
            {
                isMove = false;
                animator.SetBool("isRun", false);
                animator.SetInteger("PatternNum", 3);
                animator.SetTrigger("StartAttack");
                StartCoroutine(monsterController.PatternDelay(patternDelay[2]));
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
