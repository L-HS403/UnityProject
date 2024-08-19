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
    private Potion potion;

    private void Start()
    {
        notice.color = new Color(notice.color.r, notice.color.g, notice.color.b, 0);
    }
    void Update()
    {
        lifeText.text = "Life : " + GameManager.Instance.GetCurrentLife() + "/" + GameManager.Instance.GetLife();
        lifeText2.text = "Life : " + GameManager.Instance.GetCurrentLife() + "/" + GameManager.Instance.GetLife();
        potionText.text = "Potion : " + GameManager.Instance.currentPotionCount;
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
