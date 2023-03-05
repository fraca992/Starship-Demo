using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene(1);
        return;
    }

    public void QuitButton()
    {
        Application.Quit();
        return;
    }
}
