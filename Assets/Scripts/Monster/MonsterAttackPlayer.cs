using UnityEngine;

public class MonsterAttackPlayer : MonoBehaviour
{
    [SerializeField]
    private StatusController currentTarget;
    [SerializeField]
    private MonsterController monsterController;

    private int monsterNum;

    [SerializeField] private NightmareAttack nightmareAttack;
    [SerializeField] private TerrorBringerAttack terrorBringerAttack;
    [SerializeField] private SoulEaterAttack soulEaterAttack;
    [SerializeField] private UsurperAttack usurperAttack;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DamagePlayer(other);
        }
    }

    private void DamagePlayer(Collider other)
    {
        monsterNum = monsterController.GetMonsterNum();
        StatusController player = other.GetComponent<StatusController>();
        if (player != null)
        {
            if (monsterNum == 0)
                player.DecreaseHP(nightmareAttack.GetDamage());
            if (monsterNum == 1)
                player.DecreaseHP(terrorBringerAttack.GetDamage());
            if (monsterNum == 2)
                player.DecreaseHP(soulEaterAttack.GetDamage());
            if (monsterNum == 3)
                player.DecreaseHP(usurperAttack.GetDamage());
        }
    }
}
