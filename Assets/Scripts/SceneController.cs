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
    }

    public void GoFirstOnClick()
    {
        StartCoroutine(GameManager.Instance.LoadScene("NightmareScene"));
        SoundManager.Instance.BgSoundPlay(SoundManager.Instance.bgList[1]);
    }

    public void GoSecondOnClick()
    {
        StartCoroutine(GameManager.Instance.LoadScene("TerrorBringerScene"));
        SoundManager.Instance.BgSoundPlay(SoundManager.Instance.bgList[1]);
    }

    public void GoThirdOnClick()
    {
        StartCoroutine(GameManager.Instance.LoadScene("SoulEaterScene"));
        SoundManager.Instance.BgSoundPlay(SoundManager.Instance.bgList[1]);
    }

    public void GoLastOnClick()
    {
        StartCoroutine(GameManager.Instance.LoadScene("UsurperScene"));
        SoundManager.Instance.BgSoundPlay(SoundManager.Instance.bgList[1]);
    }

    public void CountinueOnClick()
    {
        StartCoroutine(GameManager.Instance.LoadScene(SceneManager.GetActiveScene().name));
        GameManager.Instance.DecreaseLife();
        SoundManager.Instance.BgSoundPlay(SoundManager.Instance.bgList[1]);
    }
}
