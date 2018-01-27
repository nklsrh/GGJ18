﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TurnController : BaseShipController
{
    public float thrust = 1000;
    public float turnTorque = 100;
    public float brakeLerp = 1f;

    private Rigidbody rig;
    private bool isBraking = false;
    private float brakingAmountRequired = 1.0f;
    private float currentBrakingAmount = 1.0f;


    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    public void Turn(float amount)
    {
        rig.AddTorque(Vector3.up * amount * turnTorque * Time.deltaTime);
    }

    public override void LeftStick(Vector2 stick)
    {
        base.LeftStick(stick);

        Turn(stick.x);
    }

    void Update()
    {
        rig.AddForce(transform.forward * thrust * brakingAmountRequired * Time.deltaTime);

        brakingAmountRequired = 1;

        currentBrakingAmount = Mathf.Lerp(currentBrakingAmount, brakingAmountRequired, brakeLerp * Time.deltaTime);
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