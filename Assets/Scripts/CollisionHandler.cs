using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    #region VARIABLES
    // Parameters
    [SerializeField] private float delay = 5.0f;
    [SerializeField] private int hitFuelDrain = 20;
    [SerializeField] private int fuelRechargeAmount = 40;

    // Caches
    private FuelManager rocketFuelManager;
    private Movement rocketMovement;
    private AudioSource secondaryAudioSource;
    [SerializeField] private AudioClip crashAudioClip;
    [SerializeField] private AudioClip successAudioClip;

    // State variables

    #endregion

    private void Start()
    {
        rocketFuelManager = this.GetComponent<FuelManager>();
        rocketMovement = this.GetComponent<Movement>();
        secondaryAudioSource = this.GetComponents<AudioSource>()[1];
    }
    #region COLLISIONS&TRIGGERS
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Obstacle":
                if (!secondaryAudioSource.isPlaying) secondaryAudioSource.PlayOneShot(crashAudioClip);
                rocketFuelManager.ChangeFuelLevel(-hitFuelDrain);
                break;
            case "Finish":
                if (!secondaryAudioSource.isPlaying) secondaryAudioSource.PlayOneShot(successAudioClip);
                LoadNextLevel();
                break;
            default: // In default case (i.e. hitting terrain), we want to destroy the rocket
                rocketFuelManager.ChangeFuelLevel(-rocketFuelManager.MaxFuel);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.gameObject.tag)
        {
            case "Fuel":
                rocketFuelManager.ChangeFuelLevel(fuelRechargeAmount);
                GameObject.Destroy(other.gameObject);
                break;
            default:
                break;
        }
    }
    #endregion

    #region METHODS
    private void LoadNextLevel()
    {
        int nextLvlIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLvlIndex >= SceneManager.sceneCountInBuildSettings) nextLvlIndex = 0;
        StartCoroutine(LoadLvlAfterDelay(delay, nextLvlIndex));
    }

    IEnumerator LoadLvlAfterDelay(float amount, int index)
    {
        rocketMovement.canMove = false;
        yield return new WaitForSeconds(amount);
        if (rocketFuelManager.FuelLevel == 0)
        {
            index = SceneManager.GetActiveScene().buildIndex;
        }
        SceneManager.LoadScene(index);
        rocketMovement.canMove = true;
    }
    #endregion
}
