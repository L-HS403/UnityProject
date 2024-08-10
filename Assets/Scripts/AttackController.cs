using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private Animator animator;
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

    public void DoDamage()
    {
        Debug.Log("데미지를 입힘!");
    }
}
