using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatus : MonoBehaviour
{
    [SerializeField]
    private float monsterHP;
    private float currentHP;
    [SerializeField]
    private MonsterController monsterController;

    private void Start()
    {
        currentHP = monsterHP;
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;

        if (currentHP <= 0)
        {
            monsterController.Die();
        }
    }
}
