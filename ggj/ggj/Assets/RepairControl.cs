using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairControl : BaseShipController {

    public float regenAmountPerSecond = 10.0f;

	public AudioSource repairSound;

	public void Repair()
    {
        MainShip.health.Regen(regenAmountPerSecond * Time.deltaTime);
	}

    public override void ActionButtonDown()
    {
        base.ActionButtonDown();

        Repair();
		if (repairSound.isPlaying == false) {
			repairSound.Play ();
		}
    }

	internal override void RemoveControl (RPlayerController playerController)
	{

		repairSound.Stop ();
		base.RemoveControl (playerController);
	}

    protected override void Update()
    {
        base.Update();
    }
}
