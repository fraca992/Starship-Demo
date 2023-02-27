using System.Collections;
using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool canMove = true;
    private bool hasMoved = false;
    private IEnumerator fuelDrainCR;
    private Rigidbody rb;
    private AudioSource thrusterSoundSource;
    private FuelManager rocketFuelManager;
    [SerializeField] private float thrust = 1000f;
    [SerializeField] private float torque = 350f;
    [SerializeField] private float correctionTorque = 130f;
    [SerializeField] private float torqueHeight = 4f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        thrusterSoundSource = GetComponent<AudioSource>();
        rocketFuelManager = GetComponent<FuelManager>();
        fuelDrainCR = FuelDrain();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    void LateUpdate()
    {
            EnforceConstraints();
    }

    #region METHODS
    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space) && canMove)
        {
            rb.AddRelativeForce(thrust * Vector3.up * Time.deltaTime);
            if (!thrusterSoundSource.isPlaying) thrusterSoundSource.Play();
            if (!hasMoved) StartFuelDrain();
        }
        else
        {
            thrusterSoundSource.Stop();
        }
        if (hasMoved && !canMove) StopCoroutine(fuelDrainCR);
    }


    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) && canMove)
        {
            ApplyRotation(-torque);
        }
        else if (Input.GetKey(KeyCode.D) && canMove)
        {
            ApplyRotation(torque);
        }
        else if (canMove)
        {
            SlowDownRotation();
        }
        //else
        //{
        //    //put stopping sound logic here
        //}
        if (hasMoved && !canMove) StopCoroutine(fuelDrainCR);
    }
    private void SlowDownRotation()
    {
        float slowingTorque = correctionTorque * rb.angularVelocity.z;

        if (MathF.Abs(rb.angularVelocity.z) > 0.1)
        {
            ApplyRotation(slowingTorque);
        }
        else if (MathF.Abs(rb.angularVelocity.z) > 0.01)
        {
            rb.angularVelocity = new Vector3(0, 0, 0);
        }
    }
    private void ApplyRotation(float torq)
    {
        Vector3 rotationPoint = rb.transform.localPosition + rb.transform.up * torqueHeight;
        rb.AddForceAtPosition(torq * rb.transform.right * Time.deltaTime, rotationPoint);
        if (!hasMoved) StartFuelDrain();
    }


    private void EnforceConstraints()
    {
        if (!canMove) return;

        rb.transform.localEulerAngles = new Vector3(0, 0, rb.transform.localEulerAngles.z);
        rb.transform.localPosition = new Vector3(rb.transform.localPosition.x, rb.transform.localPosition.y, 0);
    }

    private void StartFuelDrain()
    {
        hasMoved = true;
        StartCoroutine(fuelDrainCR);
    }
    IEnumerator FuelDrain()
    {
        while(true)
          {
            yield return new WaitForSeconds(1f);
            rocketFuelManager.ChangeFuelLevel(0);
        }
    }
    #endregion
}
