using UnityEngine;
using UnityEngine.UI;

public class FuelBarManager : MonoBehaviour
{
    [SerializeField] private Slider fuelSlider;

    private void Start()
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
    }
}
