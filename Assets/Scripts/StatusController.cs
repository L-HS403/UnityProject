using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StatusController : MonoBehaviour
{
    // 체력
    [SerializeField]
    private float hp;
    private float currentHp;

    // 스테미나
    [SerializeField]
    private float sp;
    private float currentSp;

    // 스테미나 증가량
    [SerializeField]
    private float spIncreaseSpeed;

    // 스테미나 재회복 딜레이
    [SerializeField]
    private float spRechargeTime;
    private float currentSpRechargeTime;

    // 스테미나 감소 여부
    private bool spUsed;

    // 방어력
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
            Debug.Log("캐릭터의 스테미나가 0이 되었습니다.");

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
