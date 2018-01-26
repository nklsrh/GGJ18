using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairControl : BaseShipController {

//	public BaseShipController ship;

//	void OnTriggerStay(Collider other)
//	{
//		RPlayerController rp = other.GetComponent<RPlayerController> ();
//		if (rp != null) {
//			if (rp.IsCarryingItem == false) {
//				rp.GiveControl (ship, this);
//			}
//		}
//	}
//
	public void Repair() {
		//shiphealth +
		Debug.Log("Ship Health ++");
	}

	public override void ActionButton()
	{
		base.ActionButton();

		Repair();
	}

}
