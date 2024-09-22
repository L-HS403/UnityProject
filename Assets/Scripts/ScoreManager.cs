using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private MonsterController monsterController;

    private float remainTime;
    private int monsterIndex;

    public void CalScore()
    {
        monsterIndex = monsterController.GetMonsterNum() - 1;
        TimeScore();
        DamageScore();
        Debug.Log(GameManager.Instance.score.timeScore[monsterIndex]);
        GameManager.Instance.score.score[monsterIndex] = (float)GameManager.Instance.score.timeScore[monsterIndex] + GameManager.Instance.score.damageScore[monsterIndex];
    }

    private void TimeScore()
    {
        remainTime = Timer.Instance.currentTimeMin * 60 + Timer.Instance.currentTimeSec;

        if (monsterIndex != 3)
        {
            if (remainTime >= 90f)
                GameManager.Instance.score.timeScore[monsterIndex] = 200;
            else if (remainTime >= 60f)
                GameManager.Instance.score.timeScore[monsterIndex] = 150;
            else if (remainTime >= 30f)
                GameManager.Instance.score.timeScore[monsterIndex] = 100;
            else
                GameManager.Instance.score.timeScore[monsterIndex] = 50;
        }
        else
        {
            if (remainTime >= 60f)
                GameManager.Instance.score.timeScore[monsterIndex] = 200;
            else if (remainTime >= 40f)
                GameManager.Instance.score.timeScore[monsterIndex] = 150;
            else if (remainTime >= 20f)
                GameManager.Instance.score.timeScore[monsterIndex] = 100;
            else
                GameManager.Instance.score.timeScore[monsterIndex] = 50;
        }
    }

    private void DamageScore()
    {
        GameManager.Instance.score.damageScore[monsterIndex] = 200 - GameManager.Instance.receivedDamage;

        if (GameManager.Instance.score.damageScore[monsterIndex] < 0)
            GameManager.Instance.score.damageScore[monsterIndex] = 0;
    }
}
