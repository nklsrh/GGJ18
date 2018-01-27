using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StorageController : BaseShipController {

	bool inStorage = false;

	public GameObject storageItem;

	RPlayerController rp;

	public Transform[] spawnPoints;

	public override void ActionButton ()
	{
		base.ActionButton ();

		Pickup();
		stationIcon.SetActive (false);
	}

	void OnTriggerStay(Collider other)
	{
		rp = other.GetComponent<RPlayerController>();
		if (rp != null)
		{
			rp.inStorage = true;
			stationIcon.SetActive (true);
		}
	}

	void OnTriggerExit(Collider other)
	{
		rp = other.GetComponent<RPlayerController>();
		if (rp != null)
		{
			rp.inStorage = false;
			stationIcon.SetActive (false);
		}
	}

	void Pickup() {

		Debug.Log("Pick up");
		for (int i = 0; i < 5; i++) {

			GameObject item = Instantiate (storageItem, spawnPoints[i].position, Quaternion.identity);
		}

	}


    protected override void Update()
    {
        base.Update();
    }
}
