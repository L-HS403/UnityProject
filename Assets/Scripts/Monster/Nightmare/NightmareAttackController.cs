using System.Collections;
using UnityEngine;

public class NightmareAttackController : MonoBehaviour
{
    [SerializeField]
    private float[] patternDelay;

    private bool isMove = true;
    private bool canSkill = false;
    public bool doSkill = false;

    public int patternNum;

    private Animator animator;
    private MonsterController monsterController;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        monsterController = GetComponent<MonsterController>();
        StartCoroutine(StartSkill());
    }

    public void RandPattern()
    {
        patternNum = Random.Range(0, 3);

        if (canSkill)
            NightmareSkill();
        else
        {
            switch (patternNum)
            {
                case 0:
                    NightmarePattern1();
                    break;
                case 1:
                    NightmarePattern2();
                    break;
                case 2:
                    NightmarePattern3();
                    break;
            }
        }
    }

    private void NightmareSkill()
    {
        animator.SetTrigger("BossSkill");
        doSkill = true;
        StartCoroutine(monsterController.PatternDelay(patternDelay[3]));
        StartCoroutine(SkillDelay());
    }

    private void NightmarePattern1()
    {
        animator.SetInteger("PatternNum", 1);
        animator.SetTrigger("StartAttack");
        StartCoroutine(monsterController.PatternDelay(patternDelay[0]));
    }

    private void NightmarePattern2()
    {
        animator.SetInteger("PatternNum", 2);
        animator.SetTrigger("StartAttack");
        StartCoroutine(monsterController.PatternDelay(patternDelay[1]));
    }

    private void NightmarePattern3()
    {
        animator.SetInteger("PatternNum", 3);
        animator.SetTrigger("StartAttack");
        StartCoroutine(monsterController.PatternDelay(patternDelay[2]));
    }

    private IEnumerator SkillDelay()
    {
        yield return new WaitForSeconds(8f);
        canSkill = false;
        doSkill = false;
        yield return new WaitForSeconds(30f);
        canSkill = true;
    }

    private IEnumerator StartSkill()
    {
        yield return new WaitForSeconds(15f);
        canSkill = true;
    }
}
