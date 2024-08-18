using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private MonsterStatus monsterStatus;
    [SerializeField]
    private SetTarget setTarget;
    private bool isAttack;
    private int hashAttackCount = Animator.StringToHash("AttackCount");
    [SerializeField]
    private int[] attackDamage;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        TryAttack();
    }

    public int AttackCount
    {
        get => animator.GetInteger(hashAttackCount);
        set => animator.SetInteger(hashAttackCount, value);
    }

    private void TryAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("tryAttack");
        }
    }

    public void DoDamage1()
    {
        if (setTarget.currentTarget != null)
        {
            setTarget.currentTarget.TakeDamage(attackDamage[0]);
            SoundManager.Instance.SoundPlay(SoundManager.Instance.soundList[0]);
        }
    }

    public void DoDamage2()
    {
        if (setTarget.currentTarget != null)
        {
            setTarget.currentTarget.TakeDamage(attackDamage[1]);
            SoundManager.Instance.SoundPlay(SoundManager.Instance.soundList[0]);
        }
    }

    public void DoDamage3()
    {
        if (setTarget.currentTarget != null)
        {
            setTarget.currentTarget.TakeDamage(attackDamage[2]);
            SoundManager.Instance.SoundPlay(SoundManager.Instance.soundList[0]);
        }
    }
}
