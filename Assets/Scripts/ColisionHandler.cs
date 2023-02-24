using UnityEngine;
using UnityEngine.SceneManagement;

public class ColisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Obstacle":
                Debug.Log("Obstacle hit!");
                break;
            case "Finish":
                Debug.Log("Level Completed!");
                LoadNextLevel();
                break;
            default:
                Debug.Log("BOOM");
                ReloadLevel();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.gameObject.tag)
        {
            case "Fuel":
                Debug.Log("Fuel Recharged!");
                break;
            default:
                break;
        }
    }

    private void ReloadLevel()
    {
        int currentLvlIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLvlIndex);
    }

    private void LoadNextLevel()
    {
        int nextLvlIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLvlIndex < SceneManager.sceneCountInBuildSettings) SceneManager.LoadScene(nextLvlIndex);
    }
}
