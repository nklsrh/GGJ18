using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairControl : BaseShipController {

    public float regenAmountPerSecond = 10.0f;

	public void Repair()
    {
        MainShip.health.Regen(regenAmountPerSecond * Time.deltaTime);
	}

    public override void ActionButtonDown()
    {
        base.ActionButtonDown();

        Repair();

		stationIcon.SetActive (false);
    }

	void OnTriggerEnter() {

		stationIcon.SetActive (true);
	}

	void OnTriggerExit() {
		stationIcon.SetActive (false);
	}


}
