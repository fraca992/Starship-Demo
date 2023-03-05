using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    #region VARIABLES
    // Parameters
    [SerializeField] private float delay = 2f;
    [SerializeField] private float rotationSpeed = 5;

    // Cache
    private Movement rocketMovement;

    // State variables
    private IEnumerator rotateAfterDelayCR;
    private bool startRotating;
    private bool isWaiting;
    #endregion


    void Start()
    {
        rocketMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        rotateAfterDelayCR = RotateAfterDelay();
        startRotating = false;
        isWaiting = false;
    }

    void Update()
    {
        if (rocketMovement.hasMoved && !startRotating && !isWaiting) StartCoroutine(rotateAfterDelayCR);
        
        if (startRotating) RotateWorld();
    }

    IEnumerator RotateAfterDelay()
    {
        Debug.Log($"rocket has moved, waiting for {delay} seconds");
        isWaiting = true;

        yield return new WaitForSeconds(delay);

        Debug.Log("starting rotating");
        startRotating = true;
        isWaiting = false;
    }

    private void RotateWorld()
    {
        Vector3 rotation = new Vector3(0, 0, 0);
        rotation.z = rotationSpeed * Time.deltaTime;

        this.transform.Rotate(rotation);    
    }
}
