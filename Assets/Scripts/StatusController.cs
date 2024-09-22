using UnityEngine;
using UnityEngine.UI;


public class StatusController : MonoBehaviour
{
    [SerializeField]
    private float hp;
    private float currentHp;

    public bool fullHp;

    [SerializeField]
    private float sp;
    private float currentSp;

    [SerializeField]
    private float spIncreaseSpeed;

    [SerializeField]
    private float spRechargeTime;
    private float currentSpRechargeTime;

    private bool spUsed;

    [SerializeField]
    private float dp;
    private float currentDp;

    [SerializeField]
    private Image[] images_Gauge;
    private PlayerController playerController;
    [SerializeField]
    private CameraMove cameraMove;
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

    private void Update()
    {
        CheckFullHP();
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
        {
            currentHp = hp;
            playerController.usingPotion = false;
        }
        else
            currentHp += _count;
    }

    public void DecreaseHP(float _count)
    {
        _count -= currentDp;

        if (_count < 0)
        {
            _count = 0;
        }
        currentHp -= _count;
        GameManager.Instance.receivedDamage += _count;
        cameraMove.CameraShake();

        if (_count > 5)
        {
            animator.SetTrigger("Hit");
            StartCoroutine(playerController.HitCoroutine());
        }

        playerController.usingPotion = false;

        if (playerController.isGuard)
        {
            DecreaseStamina(30);
            SoundManager.Instance.GuardSoundPlay(SoundManager.Instance.soundList[1]);
        }

        if (currentHp <= 0)
        {
            currentHp = 0;
            GameManager.Instance.Die();
        }
    }

    public void IncreaseSP(float _count)
    {
        if (currentSp + _count > sp)
            currentSp += _count;
        else
            currentSp = sp;
    }

    public void DecreaseStamina(float _count)
    {
        spUsed = true;
        currentSpRechargeTime = 0;

        if (currentSp - _count >= 0)
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

    private void CheckFullHP()
    {
        if (currentHp == hp)
            fullHp = true;
        else
            fullHp = false;
    }
}
