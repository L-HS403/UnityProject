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

    private void Update()
    {
        Debug.Log(currentHP);
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        Debug.Log(gameObject.name + " took " + amount + " damage. Current health: " + currentHP);

        if (currentHP <= 0)
        {
            monsterController.Die();
        }
    }
}
