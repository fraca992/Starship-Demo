using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationManager : MonoBehaviour
{
    private float tS;
    private bool isPausing;
    private bool isRunning;

    void Start()
    {
        tS = Time.timeScale;
        isPausing = false;
        isRunning = true;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P) && !isPausing && isRunning)
        {
            Time.timeScale = 0;
            isPausing = true;
            isRunning = false;
        }
        if (Input.GetKeyDown(KeyCode.P) && !isPausing && !isRunning)
        {
            Time.timeScale = 1;
            isPausing = true;
            isRunning = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene(0);

        isPausing = false;
    }
}
