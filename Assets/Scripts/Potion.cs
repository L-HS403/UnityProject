using System.Collections;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField]
    private int healvalue;

    [SerializeField]
    private StatusController statusController;
    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private TextManager textManager;

    void Update()
    {
        UsePotion();
    }

    private void UsePotion()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (playerController.usingPotion == true)
            {
                textManager.Notify("이미 포션을 먹고 있습니다.");
            }
            else if (GameManager.Instance.currentPotionCount == 0)
            {
                textManager.Notify("포션이 존재하지 않습니다.");
            }
            else if (statusController.fullHp)
            {
                textManager.Notify("체력을 더 이상 회복할 수 없습니다.");
            }
            else if (playerController.isAttack)
            {

            }
            else
            {
                playerController.usingPotion = true;
                GameManager.Instance.currentPotionCount--;
                StartCoroutine(Healing());
            }
        }
    }

    public IEnumerator Healing()
    {
        for (int i = 0; i < healvalue; i++)
        {
            if (playerController.usingPotion)
            {
                statusController.IncreaseHP(1);
                yield return new WaitForSeconds((float)5 / healvalue);
            }
            else
                break;
        }
        playerController.usingPotion = false;
    }
}
