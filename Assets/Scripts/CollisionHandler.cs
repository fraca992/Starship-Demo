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
    [SerializeField] private AudioClip hitAudioClip;
    [SerializeField] private AudioClip successAudioClip;
    [SerializeField] private ParticleSystem successParticles;

    // Caches
    private FuelManager rocketFuelManager;
    private Movement rocketMovement;
    private AudioSource secondaryAudioSource;

    // State variables
    private bool isTransitioning;
    public bool isEnabled;
    #endregion

    private void Start()
    {
        rocketFuelManager = this.GetComponent<FuelManager>();
        rocketMovement = this.GetComponent<Movement>();
        secondaryAudioSource = this.GetComponents<AudioSource>()[1];

        isTransitioning = false;
        isEnabled = true;
    }

    #region COLLISIONS&TRIGGERS
    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || !isEnabled) return;


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
        if (isTransitioning || rocketFuelManager.isDestroyed || !isEnabled) return;

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
        if (!secondaryAudioSource.isPlaying) secondaryAudioSource.PlayOneShot(hitAudioClip);
        rocketFuelManager.ChangeFuelLevel(-hitFuelDrain);
        isTransitioning = false;
    }

    private void DestroyRocket()
    {
        isTransitioning = true;
        rocketFuelManager.ChangeFuelLevel(-rocketFuelManager.MaxFuel);
        isTransitioning = false;
    }

    public void LoadNextLevel()
    {
        isTransitioning = true;
        if (!secondaryAudioSource.isPlaying) secondaryAudioSource.PlayOneShot(successAudioClip);
        successParticles.Play();
        int nextLvlIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLvlIndex >= SceneManager.sceneCountInBuildSettings) nextLvlIndex = 1;
        StartCoroutine(LoadLevelUtils.LoadLvlAfterDelay(delay, nextLvlIndex,rocketMovement,rocketFuelManager));
        isTransitioning = false;
    }
    #endregion
}
