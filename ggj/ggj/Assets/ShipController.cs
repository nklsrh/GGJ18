using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipController : BaseShipController {
    
    public float thrust = 1000;
    public float turnTorque = 100;

    private Rigidbody rig;

	void Start ()
    {
        rig = GetComponent<Rigidbody>();
	}
	
    public void Drive()
    {
        rig.AddForce(transform.forward * thrust * Time.deltaTime);
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

    public override void ActionButton()
    {
        base.ActionButton();

        Drive();
    }
}
