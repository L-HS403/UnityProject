using System.Collections;
using UnityEngine;

public class NightmareAttackController : MonoBehaviour
{
    [SerializeField]
    private float[] patternDelay;

    private bool isMove = true;
    private bool canSkill = true;
    public bool doSkill = false;

    public int patternNum;

    private Animator animator;
    private MonsterController monsterController;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        monsterController = GetComponent<MonsterController>();
    }

    public void RandPattern()
    {
        patternNum = Random.Range(0, 3);

        switch (patternNum)
        {
            case 0:
                if (canSkill)
                    NightmareSkill();
                else
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
        yield return new WaitForSeconds(5f);
        canSkill = false;
        doSkill = false;
        yield return new WaitForSeconds(60f);
        canSkill = true;
    }
}
