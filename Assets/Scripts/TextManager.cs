using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    [SerializeField]
    private Text lifeText;

    void Update()
    {
        lifeText.text = "Life : " + GameManager.Instance.GetCurrentLife() + "/" + GameManager.Instance.GetLife();
    }
}
