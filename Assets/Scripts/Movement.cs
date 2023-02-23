using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float thrust = 1000f;
    [SerializeField] private float torque = 350f;
    [SerializeField] private float correctionTorque = 130f;
    [SerializeField] private float torqueHeight = 4f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    void LateUpdate()
    {
        if (true) //TODO disable when touching game over to allow ship to roll around
        {
            EnforceConstraints();
        }
    }

    #region METHODS
    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(thrust * Vector3.up * Time.deltaTime);
        }
    }


    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(-torque);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(torque);
        }
        else
        {
            SlowDownRotation();
        }
    }
    private void SlowDownRotation()
    {
        float slowingTorque = correctionTorque * rb.angularVelocity.z;

        if (MathF.Abs(rb.angularVelocity.z) > 0.1)
        {
            ApplyRotation(slowingTorque);
        }
        else if (rb.angularVelocity.z > 0.01)
        {
            //rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, Vector3.zero, Time.deltaTime);
            rb.angularVelocity = new Vector3(0, 0, 0);
        }
    }
    private void ApplyRotation(float torq)
    {
        Vector3 rotationPoint = rb.transform.localPosition + rb.transform.up * torqueHeight;
        rb.AddForceAtPosition(torq * rb.transform.right * Time.deltaTime, rotationPoint);
    }


    private void EnforceConstraints()
    {
        rb.transform.localEulerAngles = new Vector3(0, 0, rb.transform.localEulerAngles.z);
        rb.transform.localPosition= new Vector3(rb.transform.localPosition.x, rb.transform.localPosition.y, 0);
    }
    #endregion
}
