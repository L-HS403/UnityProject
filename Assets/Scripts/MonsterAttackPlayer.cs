using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackPlayer : MonoBehaviour
{
    [SerializeField]
    private StatusController currentTarget;
    [SerializeField]
    private MonsterAttack monsterAttack;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision Detected with: " + other.name);
        if (other.CompareTag("Player"))
        {
            StatusController player = other.GetComponent<StatusController>();
            if (player != null)
            {
                player.DecreaseHP(monsterAttack.GetDamage());
            }
        }
    }
}
