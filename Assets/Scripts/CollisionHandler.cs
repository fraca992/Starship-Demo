using System.Collections;
using Unity.VisualScripting;
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
    private bool isTransitioning;
    #endregion

    private void Start()
    {
        rocketFuelManager = this.GetComponent<FuelManager>();
        rocketMovement = this.GetComponent<Movement>();
        secondaryAudioSource = this.GetComponents<AudioSource>()[1];

        isTransitioning = false;
    }

    #region COLLISIONS&TRIGGERS
    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning) return;

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Obstacle":
                ProcessHit();
                break;
            case "Finish":
                LoadNextLevel();
                break;
            default:
                DestroyRocket();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isTransitioning || rocketFuelManager.isDestroyed) return;

        switch(other.gameObject.tag)
        {
            case "Fuel":
                ProcessFuel();
                GameObject.Destroy(other.gameObject);
                break;
            default:
                break;
        }
    }

    private void ProcessFuel()
    {
        isTransitioning = true;
        rocketFuelManager.ChangeFuelLevel(fuelRechargeAmount);
        
        isTransitioning = false;
    }
    #endregion

    #region METHODS
    private void ProcessHit()
    {
        isTransitioning = true;
        if (!secondaryAudioSource.isPlaying) secondaryAudioSource.PlayOneShot(crashAudioClip);
        rocketFuelManager.ChangeFuelLevel(-hitFuelDrain);
        isTransitioning = false;
    }

    private void LoadNextLevel()
    {
        isTransitioning = true;
        if (!secondaryAudioSource.isPlaying) secondaryAudioSource.PlayOneShot(successAudioClip);
        int nextLvlIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLvlIndex >= SceneManager.sceneCountInBuildSettings) nextLvlIndex = 0;
        StartCoroutine(LoadLvlAfterDelay(delay, nextLvlIndex));
        isTransitioning = false;
    }

    private void DestroyRocket()
    {
        isTransitioning = true;
        rocketFuelManager.ChangeFuelLevel(-rocketFuelManager.MaxFuel);
        isTransitioning = false;
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
