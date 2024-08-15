using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    [SerializeField]
    private Collider[] attackCollider;
    [SerializeField]
    private float[] attackDamage;
    private float currentAttackDamage;
    [SerializeField]
    private MonsterController monsterController;

    private void Start()
    {
        DisableAttackCollider();
    }

    public void EnableAttackCollider()
    {
        Debug.Log(monsterController.patternNum);
        if (monsterController.patternNum == 0)
        {
            attackCollider[0].enabled = true;
            currentAttackDamage = attackDamage[0];
        }
        else if (monsterController.patternNum == 1)
        {
            attackCollider[1].enabled = true;
            currentAttackDamage = attackDamage[1];
        }
        else if (monsterController.patternNum == 2)
        {
            attackCollider[2].enabled = true;
            currentAttackDamage = attackDamage[2];
        }
        StartCoroutine(AttackDuration());
    }

    private IEnumerator AttackDuration()
    {
        yield return new WaitForSeconds(0.5f);
        DisableAttackCollider();
    }

    private void DisableAttackCollider()
    {
        attackCollider[0].enabled = false;
        attackCollider[1].enabled = false;
        attackCollider[2].enabled = false;
    }

    public float GetDamage()
    {
        return currentAttackDamage;
    }
}
