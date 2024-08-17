using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public void StartGameOnClick()
    {
        StartCoroutine(GameManager.Instance.LoadScene("NightmareScene"));
    }
}
