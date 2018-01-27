using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TurnController : BaseShipController
{

    public float thrust = 1000;
    public float turnTorque = 100;
    public float brakeLerp = 1f;

    public float maxAngularVelocity = 1000;

    [Header("Visual")]
    public Transform wheelBaseTransform;
    public float wheelTurnMultiplier = 10;

    private Rigidbody rig;
    private bool isBraking = false;
    private float brakingAmountRequired = 1.0f;
    private float currentBrakingAmount = 1.0f;

    private float rotationAmount = 0;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    public void Turn(float amount)
    {
        if (rig.angularVelocity.sqrMagnitude < maxAngularVelocity)
        {
            rig.AddTorque(Vector3.up * amount * turnTorque * Time.deltaTime);
            rotationAmount = Mathf.Lerp(rotationAmount, amount, 2 * Time.deltaTime);
        }
        else
        {
            rig.angularVelocity = rig.angularVelocity.normalized * maxAngularVelocity * maxAngularVelocity;
        }
        rig.transform.rotation = Quaternion.Euler(0, rig.transform.rotation.eulerAngles.y, 0);
    }

    public override void LeftStick(Vector2 stick)
    {
        base.LeftStick(stick);

        Turn(stick.x);
    }

    protected override void Update()
    {
        rig.AddForce(transform.forward * thrust * brakingAmountRequired * Time.deltaTime);

        brakingAmountRequired = 1;

        currentBrakingAmount = Mathf.Lerp(currentBrakingAmount, brakingAmountRequired, brakeLerp * Time.deltaTime);

        wheelBaseTransform.Rotate(Vector3.up * rotationAmount * wheelTurnMultiplier * Time.deltaTime, Space.Self);

        rotationAmount = Mathf.Lerp(rotationAmount, 0, 1 * Time.deltaTime);

        base.Update();
    }

    public void Brake(float amount)
    {
        brakingAmountRequired = amount;
    }

    internal void PushBack(Vector3 directionAndForce)
    {
        rig.AddForce(directionAndForce * Time.deltaTime, ForceMode.Impulse);
        rig.AddTorque(Vector3.up * 0.0000001f * directionAndForce.sqrMagnitude);
    }
}
