using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    #region VARIABLES
    // Paramteres
    [SerializeField] private Vector3 movementVector;
    [SerializeField] private float period = 5f;

    // State variables
    private float movementFactor;
    private Vector3 startingPosition;

    #endregion


    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        const float tau = Mathf.PI * 2;

        float cycles = Time.time / period;        
        float rawSignWave = Mathf.Sin(cycles * tau);

        // goes from 0 to 1 following Sin function
        movementFactor = (rawSignWave + 1f) / 2f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
