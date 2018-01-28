using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StorageController : BaseShipController {

	bool inStorage = false;

	public GameObject storageItem;

	public Transform[] spawnPoints;
    public int itemsToSpawn = 5;

	public float currentBalls;
	public float maxBalls = 5;
    

    internal override void RemoveControl(RPlayerController playerController)
    {
        Pickup();

        base.RemoveControl(playerController);
    }


    void Pickup()
    {
		for (int i = 0; i < itemsToSpawn; i++) {
			if (currentBalls < maxBalls) {
				GameObject item = Instantiate (storageItem, spawnPoints [i].position, Quaternion.identity);
				currentBalls++;
			}
		}
	}

    protected override void Update()
    {
        base.Update();
    }
}
