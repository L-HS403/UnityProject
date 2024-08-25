using System.Collections;
using UnityEngine;

public class TerrorBringerAttack : MonoBehaviour
{
    [SerializeField]
    private Collider[] attackCollider;
    [SerializeField]
    private float[] attackDamage;
    private float currentAttackDamage;
    [SerializeField]
    private MonsterController monsterController;
    [SerializeField]
    private TerrorBringerAttackController terrorBringer;

    private void Start()
    {
        DisableAttackCollider();
    }

    public void EnableAttackCollider()
    {
        if (terrorBringer.patternNum == 0)
        {
            if (terrorBringer.doSkill == true)
            {
                terrorBringer.isSkillBreath = true;
                StartCoroutine(SkillBreathDuration());
                return;
            }
            else
            {
                attackCollider[0].enabled = true;
                currentAttackDamage = attackDamage[0];
            }
        }
        else if (terrorBringer.patternNum == 1)
        {
            attackCollider[1].enabled = true;
            currentAttackDamage = attackDamage[1];
        }
        else if (terrorBringer.patternNum == 2)
        {
            terrorBringer.isBreath = true;
            StartCoroutine(BreathDuration());
            return;
        }
        StartCoroutine(AttackDuration());
    }

    private IEnumerator AttackDuration()
    {
        yield return new WaitForSeconds(0.1f);
        DisableAttackCollider();
    }

    private IEnumerator BreathDuration()
    {
        yield return new WaitForSeconds(1.5f);
        terrorBringer.isBreath = false;
    }

    private IEnumerator SkillBreathDuration()
    {
        yield return new WaitForSeconds(1.8f);
        terrorBringer.isSkillBreath = false;
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
