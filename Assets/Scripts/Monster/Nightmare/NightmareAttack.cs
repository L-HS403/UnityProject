using System.Collections;
using UnityEngine;

public class NightmareAttack : MonoBehaviour
{
    [SerializeField]
    private Collider[] attackCollider;
    [SerializeField]
    private float[] attackDamage;
    private float currentAttackDamage;
    [SerializeField]
    private MonsterController monsterController;
    [SerializeField]
    private NightmareAttackController nightmare;

    private void Start()
    {
        DisableAttackCollider();
    }

    public void EnableAttackCollider()
    {
        Debug.Log(nightmare.patternNum);

        if (nightmare.doSkill == true)
        {
            attackCollider[3].enabled = true;
            currentAttackDamage = attackDamage[3];
        }
        else
        {
            if (nightmare.patternNum == 0)
            {
                attackCollider[0].enabled = true;
                currentAttackDamage = attackDamage[0];
            }
            else if (nightmare.patternNum == 1)
            {
                attackCollider[1].enabled = true;
                currentAttackDamage = attackDamage[1];
            }
            else if (nightmare.patternNum == 2)
            {
                attackCollider[2].enabled = true;
                currentAttackDamage = attackDamage[2];
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
        attackCollider[2].enabled = false;
        attackCollider[3].enabled = false;
    }

    public float GetDamage()
    {
        return currentAttackDamage;
    }
}
