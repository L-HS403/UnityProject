using UnityEngine;
using UnityEngine.UI;

public class MonsterStatus : MonoBehaviour
{
    [SerializeField]
    private float monsterHP;
    private float currentHP;

    [SerializeField]
    private Image hp_Gauge;
    [SerializeField]
    private MonsterController monsterController;

    private void Start()
    {
        currentHP = monsterHP;
    }

    private void FixedUpdate()
    {
        GaugeUpdate();
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;

        if (currentHP <= 0)
        {
            monsterController.Die();
        }
    }

    private void GaugeUpdate()
    {
        hp_Gauge.fillAmount = currentHP / monsterHP;
    }
}
