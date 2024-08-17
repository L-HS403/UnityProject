using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void GoTitleOnClick()
    {
        StartCoroutine(GameManager.Instance.LoadScene("TitleScene"));
        GameManager.Instance.DisactivePause();
    }

    public void GoFirstOnClick()
    {
        StartCoroutine(GameManager.Instance.LoadScene("NightmareScene"));
    }

    public void GoSecondOnClick()
    {
        StartCoroutine(GameManager.Instance.LoadScene("TerrorBringerScene"));
    }

    public void GoThirdOnClick()
    {
        StartCoroutine(GameManager.Instance.LoadScene("SoulEaterScene"));
    }

    public void GoLastOnClick()
    {
        StartCoroutine(GameManager.Instance.LoadScene("UsurperScene"));
    }

    public void CountinueOnClick()
    {
        StartCoroutine(GameManager.Instance.LoadScene(SceneManager.GetActiveScene().name));
    }
}
