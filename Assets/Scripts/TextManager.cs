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

    void Update()
    {
        lifeText.text = "Life : " + GameManager.Instance.GetCurrentLife() + "/" + GameManager.Instance.GetLife();
        lifeText2.text = "Life : " + GameManager.Instance.GetCurrentLife() + "/" + GameManager.Instance.GetLife();
    }
}
