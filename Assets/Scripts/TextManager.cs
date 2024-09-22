using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    [SerializeField]
    private Text lifeText;
    [SerializeField]
    private Text lifeText2;
    [SerializeField]
    private Text potionText;
    [SerializeField]
    private Text notice;
    [SerializeField]
    private Text stageText;
    [SerializeField]
    private Text clearTimeText;
    [SerializeField]
    private Text currentTimeText;
    [SerializeField]
    private Text clearDamagedText;
    [SerializeField]
    private Text clearScoreText;
    [SerializeField] 
    private Text totalScoreText;

    [SerializeField]
    private Potion potion;
    [SerializeField]
    private MonsterController monsterController;

    private int stageNum;
    private string monsterName;

    private void Start()
    {
        notice.color = new Color(notice.color.r, notice.color.g, notice.color.b, 0);
        stageNum = monsterController.GetMonsterNum();
    }

    void Update()
    {
        lifeText.text = "Life : " + GameManager.Instance.GetCurrentLife() + "/" + GameManager.Instance.GetLife();
        lifeText2.text = "Life : " + GameManager.Instance.GetCurrentLife() + "/" + GameManager.Instance.GetLife();
        potionText.text = "Potion : " + GameManager.Instance.currentPotionCount;
        stageText.text = "Stage " + stageNum + "  /  Boss : " + monsterController.monsterName;
        currentTimeText.text = "Time : " + string.Format("{0:D2}:{1:D2}", Timer.Instance.currentTimeMin, (int)Timer.Instance.currentTimeSec);
        clearTimeText.text = "Time : " + string.Format("{0:D2}:{1:D2}", Timer.Instance.currentTimeMin, (int)Timer.Instance.currentTimeSec);
        clearDamagedText.text = "Damaged : " + GameManager.Instance.receivedDamage;
        clearScoreText.text = "Score : " + GameManager.Instance.score.score[stageNum - 1] + " / 400";
        totalScoreText.text = "Total : " + GameManager.Instance.score.totalScore + " / 1600";
    }

    public void Notify(string str)
    {
        notice.text = str;
        notice.color = new Color(notice.color.r, notice.color.g, notice.color.b, 1);
        StartCoroutine(FadeNotice());
    }

    private IEnumerator FadeNotice()
    {
        yield return new WaitForSeconds(1f);
        while (notice.color.a > 0)
        {
            notice.color = new Color(notice.color.r, notice.color.g, notice.color.b, notice.color.a - (Time.deltaTime * 2.0f));
            yield return null;
        }
    }
}
