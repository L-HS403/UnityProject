using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StatusController : MonoBehaviour
{
    // ü��
    [SerializeField]
    private float hp;
    private float currentHp;

    // ���׹̳�
    [SerializeField]
    private float sp;
    private float currentSp;

    // ���׹̳� ������
    [SerializeField]
    private float spIncreaseSpeed;

    // ���׹̳� ��ȸ�� ������
    [SerializeField]
    private float spRechargeTime;
    private float currentSpRechargeTime;

    // ���׹̳� ���� ����
    private bool spUsed;

    // ����
    [SerializeField]
    private float dp;
    private float currentDp;

    [SerializeField]
    private Image[] images_Gauge;
    private PlayerController playerController;
    private Animator animator;

    private const int HP = 0, SP = 1, DP = 2;

    void Start()
    {
        currentHp = hp;
        currentSp = sp;
        currentDp = dp;
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        SPRechargeTime();
        SPRecover();
        GaugeUpdate();
    }

    private void SPRechargeTime()
    {
        if (spUsed)
        {
            if (currentSpRechargeTime < spRechargeTime)
                currentSpRechargeTime++;
            else
                spUsed = false;
        }
    }

    private void SPRecover()
    {
        if (!spUsed && currentSp < sp)
        {
            currentSp += spIncreaseSpeed;
        }
    }

    private void GaugeUpdate()
    {
        images_Gauge[HP].fillAmount = currentHp / hp;
        images_Gauge[SP].fillAmount = currentSp / sp;
    }

    public void IncreaseHP(float _count)
    {
        if (currentHp + _count > hp)
            currentHp += _count;
        else
            currentHp = hp;
    }

    public void DecreaseHP(float _count)
    {
        _count -= currentDp;

        if (_count - currentDp < 0)
        {
            _count = 0;
        }
        currentHp -= _count;
        if (_count > 5)
            animator.SetTrigger("Hit");
        if (playerController.isGuard)
            DecreaseStamina(30);

        if (currentHp <= 0)
        {
            currentHp = 0;
            images_Gauge[HP].fillAmount = currentHp / hp;
            playerController.Disactivate();
            GameManager.Instance.deadUI.SetActive(true);
            GameManager.Instance.ActivePause();
        }
    }

    public void IncreaseSP(float _count)
    {
        if (currentSp + _count > sp)
            currentSp += _count;
        else
            currentSp = sp;
    }

    public void DecreaseSP(float _count)
    {
        currentSp -= _count;

        if (currentSp <= 0)
            Debug.Log("ĳ������ ���׹̳��� 0�� �Ǿ����ϴ�.");

    }

    public void DecreaseStamina(float _count)
    {
        spUsed = true;
        currentSpRechargeTime = 0;

        if (currentSp - _count > 0)
            currentSp -= _count;
        else
            currentSp = 0;
    }

    public float GetCurrentSP()
    {
        return currentSp;
    }

    public void SetDP(float _count)
    {
        currentDp = _count;
    }
}
