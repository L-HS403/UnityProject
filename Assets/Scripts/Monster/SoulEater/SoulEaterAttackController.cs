using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulEaterAttackController : MonoBehaviour
{
    [SerializeField]
    private float[] patternDelay;

    private bool isMove = true;
    private bool canSkill = true;
    public bool doSkill = false;

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
                    SoulEaterSkill();
                else
                    SoulEaterPattern1();
                break;
            case 1:
                SoulEaterPattern2();
                break;
            case 2:
                SoulEaterPattern3();
                break;
        }
    }

    private void SoulEaterSkill()
    {
        animator.SetTrigger("BossSkill");
        doSkill = true;
        StartCoroutine(monsterController.PatternDelay(patternDelay[3]));
        StartCoroutine(SkillDelay());
    }

    private void SoulEaterPattern1()
    {
        animator.SetInteger("PatternNum", 1);
        animator.SetTrigger("StartAttack");
        StartCoroutine(monsterController.PatternDelay(patternDelay[0]));
    }

    private void SoulEaterPattern2()
    {
        animator.SetInteger("PatternNum", 2);
        animator.SetTrigger("StartAttack");
        StartCoroutine(monsterController.PatternDelay(patternDelay[1]));
    }

    private void SoulEaterPattern3()
    {
        animator.SetInteger("PatternNum", 3);
        animator.SetTrigger("StartAttack");
        StartCoroutine(monsterController.PatternDelay(patternDelay[2]));
    }

    private IEnumerator SkillDelay()
    {
        yield return new WaitForSeconds(15f);
        canSkill = false;
        doSkill = false;
        yield return new WaitForSeconds(60f);
        canSkill = true;
    }
}
