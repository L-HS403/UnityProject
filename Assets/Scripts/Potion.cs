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
                textManager.Notify("�̹� ������ �԰� �ֽ��ϴ�.");
            }
            else if (GameManager.Instance.currentPotionCount == 0)
            {
                textManager.Notify("������ �������� �ʽ��ϴ�.");
            }
            else if (statusController.fullHp)
            {
                textManager.Notify("ü���� �� �̻� ȸ���� �� �����ϴ�.");
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
