using UnityEngine;
using UnityEngine.UI;

public class FuelBarManager : MonoBehaviour
{
    private Slider fuelSlider;

    private void Awake()
    {
        fuelSlider = this.GetComponent<Slider>();
    }

    public void SetFuelLvl(int fuel)
    {
        fuelSlider.value = fuel;
    }
    public void SetBarMax(int maxLvl)
    {
        fuelSlider.maxValue = maxLvl;
        fuelSlider.value = maxLvl;
    }
}
