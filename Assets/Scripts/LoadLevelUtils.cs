using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LoadLevelUtils
{
    public static IEnumerator LoadLvlAfterDelay(float amount, int index, Movement rocketMovement, FuelManager rocketFuelManager)
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
}
