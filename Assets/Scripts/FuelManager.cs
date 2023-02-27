using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FuelManager : MonoBehaviour
{
    #region VARIABLES
    // Parameters
    private int _fuelLevel;
    public int FuelLevel { get { return _fuelLevel; } private set { _fuelLevel = Mathf.Clamp(value, 0, MaxFuel); } }
    public int MaxFuel { get; private set; } = 100;
    [SerializeField] private int fuelDrainPerSecond = 2;
    [SerializeField] private float delay = 5.0f;
    // Caches
    [SerializeField] private GameObject fuelBar;
    private FuelBarManager fuelBarManager;
    private Movement rocketMovement;
    private AudioSource secondaryAudioSource;
    [SerializeField] private AudioClip destroiedAudioClip;

    // State variables
    public bool isDestroyed;
    #endregion

    private void Start()
    {
        FuelLevel = MaxFuel;
        fuelBarManager = fuelBar.GetComponent<FuelBarManager>();
        fuelBarManager.SetBarMax(MaxFuel);
        rocketMovement = this.GetComponent<Movement>();
        secondaryAudioSource = this.GetComponents<AudioSource>()[1];

        isDestroyed = false;
    }

    #region METHODS
    public int ChangeFuelLevel(int amount)
    {
        if (!isDestroyed)
        {
            if (amount == 0) amount = -fuelDrainPerSecond;
            FuelLevel += amount;
            fuelBarManager.SetFuelLvl(FuelLevel);
        }
        if (FuelLevel <= 0)
        {
            if (!isDestroyed)
            {
                secondaryAudioSource.PlayOneShot(destroiedAudioClip);
                isDestroyed = true;
            }
            ReloadLevel();
        }
        return FuelLevel;
    }

    private void ReloadLevel()
    {
        int currentLvlIndex = SceneManager.GetActiveScene().buildIndex;     
        StartCoroutine(LoadLvlAfterDelay(delay,currentLvlIndex));
    }

    IEnumerator LoadLvlAfterDelay(float amount,int index)
    {
        rocketMovement.canMove = false;
        yield return new WaitForSeconds(amount);
        SceneManager.LoadScene(index);
        rocketMovement.canMove = true;
    }
    #endregion
}
