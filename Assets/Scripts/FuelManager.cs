using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FuelManager : MonoBehaviour
{
    private int _fuelLevel;
    public int FuelLevel { get { return _fuelLevel; } private set { _fuelLevel = Mathf.Clamp(value,0,MaxFuel); } }
    public int MaxFuel { get; private set; } = 100;
    [SerializeField] private int fuelDrainPerSecond = 2;
    [SerializeField] private float delay = 5.0f;
    [SerializeField] private GameObject fuelBar;
    private FuelBarManager fuelBarManager;
    private Movement rocketMovement;

    private void Start()
    {
        FuelLevel = MaxFuel;
        fuelBarManager = fuelBar.GetComponent<FuelBarManager>();
        fuelBarManager.SetBarMax(MaxFuel);
        rocketMovement = this.GetComponent<Movement>();
    }

    // metti coroutine per fuel drain & explosion
    public int ChangeFuelLevel(int amount)
    {
        if (amount == 0) amount = -fuelDrainPerSecond;

        FuelLevel += amount;
        fuelBarManager.SetFuelLvl(FuelLevel);

        if (FuelLevel <= 0)
        {
            //TODO insert sound, effects etc. for explosion
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
}
