using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulEaterAttackController : MonoBehaviour
{
    [SerializeField]
    private float[] patternDelay;
    [SerializeField]
    private CameraMove cameraMove;
    [SerializeField]
    private TextManager textManager;

    private bool isMove = true;
    private bool canSkill = false;
    public bool doSkill = false;

    public int patternNum;

    private float rotationSpeed;
    private float moveSpeed;

    private Vector3 directionToPlayer;
    private Vector3 distanceToPlayer;

    [SerializeField]
    private Transform playerTransform;
    private Animator animator;
    private MonsterController monsterController;

    void Start()
    {
        animator = GetComponent<Animator>();
        monsterController = GetComponent<MonsterController>();
        rotationSpeed = monsterController.GetRotationSpeed();
        moveSpeed = monsterController.GetMoveSpeed();
        StartCoroutine(StartSkill());
    }

    public void RandPattern()
    {
        patternNum = Random.Range(0, 3);

        if (canSkill)
            SoulEaterSkill();
        else
        {
            switch (patternNum)
            {
                case 0:
                    SoulEaterPattern1();
                    break;
                case 1:
                    SoulEaterPattern2();
                    break;
                case 2:
                    SoulEaterPattern3();
                    break;
            }
        }
    }

    private void SoulEaterSkill()
    {
        animator.SetTrigger("BossSkill");
        doSkill = true;
        StartCoroutine(monsterController.PatternDelay(patternDelay[3]));
        StartCoroutine(SkillDelay());
        textManager.Notify("주의! 적이 날아올라 구체를 발사할 준비를 합니다. 타이밍에 맞춰 방패를 단단히 올리세요!");
    }

    private void SoulEaterPattern1()
    {
        animator.SetInteger("PatternNum", 1);
        animator.SetTrigger("StartAttack");
        StartCoroutine(monsterController.PatternDelay(patternDelay[0]));
    }

    private void SoulEaterPattern2()
    {
        animator.SetInteger("PatternNum", 2);
        animator.SetTrigger("StartAttack");
        StartCoroutine(monsterController.PatternDelay(patternDelay[1]));
    }

    private void SoulEaterPattern3()
    {
        animator.SetInteger("PatternNum", 3);
        animator.SetTrigger("StartAttack");
        StartCoroutine(monsterController.PatternDelay(patternDelay[2]));
    }

    private IEnumerator SkillDelay()
    {
        yield return new WaitForSeconds(3f);
        cameraMove.ChangeCameraTransform(1f, -15f);
        cameraMove.originPos = new Vector3(0f, 1f, -4f);
        yield return new WaitForSeconds(6f);
        cameraMove.ResetCameraTransform();
        cameraMove.originPos = new Vector3(0f, 2.2f, -4f);
        yield return new WaitForSeconds(5f);
        canSkill = false;
        doSkill = false;
        yield return new WaitForSeconds(30f);
        canSkill = true;
    }

    private IEnumerator StartSkill()
    {
        yield return new WaitForSeconds(15f);
        canSkill = true;
    }
}
