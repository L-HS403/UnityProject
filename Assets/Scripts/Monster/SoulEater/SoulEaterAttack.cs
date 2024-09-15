using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulEaterAttack : MonoBehaviour
{
    [SerializeField]
    private Collider[] attackCollider;
    [SerializeField]
    private float[] attackDamage;

    private float currentAttackDamage;

    [SerializeField]
    private MonsterController monsterController;
    [SerializeField]
    private SoulEaterAttackController soulEater;
    [SerializeField]
    private SoulEaterShootBall soulEaterShootBall;
    private Animator animator;

    private void Start()
    {
        DisableAttackCollider();
        animator = GetComponent<Animator>();
    }

    public void EnableAttackCollider()
    {
        if (soulEater.doSkill == true)
        {
            soulEaterShootBall.TryShootBall2();
            return;
        }
        else
        {
            if (soulEater.patternNum == 0)
            {
                attackCollider[0].enabled = true;
                currentAttackDamage = attackDamage[0];
            }
            else if (soulEater.patternNum == 1)
            {
                attackCollider[1].enabled = true;
                currentAttackDamage = attackDamage[1];
            }
            else if (soulEater.patternNum == 2)
            {
                soulEaterShootBall.TryShootBall();
                return;
            }
        }
        StartCoroutine(AttackDuration());
    }

    private IEnumerator AttackDuration()
    {
        yield return new WaitForSeconds(0.1f);
        DisableAttackCollider();
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
