using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsurperAttack : MonoBehaviour
{
    [SerializeField]
    private Collider[] attackCollider;
    [SerializeField]
    private float[] attackDamage;
    private float currentAttackDamage;
    [SerializeField]
    private MonsterController monsterController;
    [SerializeField]
    private UsurperAttackController usurper;

    private void Start()
    {
        DisableAttackCollider();
    }

    public void EnableAttackCollider()
    {
        if (usurper.patternNum == 0)
        {
            if (usurper.doSkill == true)
            {
                usurper.isSkillBreath = true;
                StartCoroutine(SkillBreatheDuration());
                return;
            }
            else
            {
                attackCollider[0].enabled = true;
                currentAttackDamage = attackDamage[0];
            }
        }
        else if (usurper.patternNum == 1)
        {
            attackCollider[1].enabled = true;
            currentAttackDamage = attackDamage[1];
        }
        else if (usurper.patternNum == 2)
        {
            usurper.isBreath = true;
            StartCoroutine(BreatheDuration());
            return;
        }
        StartCoroutine(AttackDuration());
    }

    private IEnumerator AttackDuration()
    {
        yield return new WaitForSeconds(0.1f);
        DisableAttackCollider();
    }

    private IEnumerator BreatheDuration()
    {
        yield return new WaitForSeconds(1.5f);
        usurper.isBreath = false;
    }

    private IEnumerator SkillBreatheDuration()
    {
        yield return new WaitForSeconds(2f);
        usurper.isSkillBreath = false;
    }

    private void DisableAttackCollider()
    {
        attackCollider[0].enabled = false;
        attackCollider[1].enabled = false;
    }

    public float GetDamage()
    {
        return currentAttackDamage;
    }
}
