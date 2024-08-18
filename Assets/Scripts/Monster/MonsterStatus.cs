using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatus : MonoBehaviour
{
    [SerializeField]
    private float monsterHP;
    private float currentHP;
    private bool invincible;
    [SerializeField]
    private MonsterController monsterController;

    private void Start()
    {
        currentHP = monsterHP;
    }

    public void TakeDamage(int amount)
    {
        if (!invincible)
            currentHP -= amount;

        if (currentHP <= 0)
        {
            monsterController.Die();
        }
    }

    public void SetInvincible(bool b)
    {
        invincible = b;
    }
}
