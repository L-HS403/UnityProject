using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void GoTitleOnClick()
    {
        StartCoroutine(GameManager.Instance.LoadScene("TitleScene"));
        SoundManager.Instance.BgSoundPlay(SoundManager.Instance.bgList[0]);
        GameManager.Instance.DisactivePause();
        GameManager.Instance.ResetLife();
        GameManager.Instance.currentPotionCount = 3;
        GameManager.Instance.CursorUnlock();
        Timer.Instance.StopTimer();
        GameManager.Instance.score.totalScore = 0;
    }

    public void GoFirstOnClick()
    {
        StartCoroutine(GameManager.Instance.LoadScene("NightmareScene"));
        DefalutSetting();
    }

    public void GoSecondOnClick()
    {
        StartCoroutine(GameManager.Instance.LoadScene("TerrorBringerScene"));
        DefalutSetting();
    }

    public void GoThirdOnClick()
    {
        StartCoroutine(GameManager.Instance.LoadScene("SoulEaterScene"));
        DefalutSetting();
    }

    public void GoLastOnClick()
    {
        StartCoroutine(GameManager.Instance.LoadScene("UsurperScene"));
        DefalutSetting();
    }

    public void CountinueOnClick()
    {
        StartCoroutine(GameManager.Instance.LoadScene(SceneManager.GetActiveScene().name));
        GameManager.Instance.DecreaseLife();
        DefalutSetting();
    }

    private void DefalutSetting()
    {
        SoundManager.Instance.BgSoundPlay(SoundManager.Instance.bgList[1]);
        GameManager.Instance.DisactivePause();
        GameManager.Instance.CursorLock();
        Timer.Instance.ResetTimer();
        Timer.Instance.StartTimer();
        GameManager.Instance.isStop = false;
        GameManager.Instance.receivedDamage = 0;
    }

    public void AllClearOnClick()
    {
        GameManager.Instance.GameAllClear();
    }
}
