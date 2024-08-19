using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    private GameObject controls;
    private bool activeControls;

    public void ExitOnClick()
    {
        Application.Quit();
    }

    public void ControlsActiveOnClick()
    {
        if (activeControls)
        {
            controls.SetActive(false);
            activeControls = false;
        }
        else
        {
            controls.SetActive(true);
            activeControls = true;
        }
    }
}
