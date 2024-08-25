using System.Collections;
using UnityEngine;

public class TerrorBringerAttackController : MonoBehaviour
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
                    TerrorBringerSkill();
                else
                    TerrorBringerPattern1();
                break;
            case 1:
                TerrorBringerPattern2();
                break;
            case 2:
                TerrorBringerPattern3();
                break;
        }
    }

    private void TerrorBringerSkill()
    {
        animator.SetTrigger("BossSkill");
        doSkill = true;
        StartCoroutine(monsterController.PatternDelay(patternDelay[3]));
        StartCoroutine(SkillDelay());
    }

    private void TerrorBringerPattern1()
    {
        animator.SetInteger("PatternNum", 1);
        animator.SetTrigger("StartAttack");
        StartCoroutine(monsterController.PatternDelay(patternDelay[0]));
    }

    private void TerrorBringerPattern2()
    {
        animator.SetInteger("PatternNum", 2);
        animator.SetTrigger("StartAttack");
        StartCoroutine(monsterController.PatternDelay(patternDelay[1]));
    }

    private void TerrorBringerPattern3()
    {
        StartCoroutine(TerrorBringerPattern3_Setdistance());
    }

    private IEnumerator SkillDelay()
    {
        yield return new WaitForSeconds(15f);
        canSkill = false;
        doSkill = false;
        yield return new WaitForSeconds(60f);
        canSkill = true;
    }

    private IEnumerator TerrorBringerPattern3_Setdistance()
    {
        animator.SetBool("isRun", true);

        while (true)
        {
            directionToPlayer = playerTransform.position - transform.position;
            directionToPlayer.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            Vector3 moveDirection = directionToPlayer.normalized;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.position -= moveDirection * moveSpeed * 3 * Time.deltaTime;

            float distanceToPlayer = directionToPlayer.magnitude;

            if (distanceToPlayer >= 15f)
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
