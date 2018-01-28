using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallItem : CarryItem {

	public StorageController storage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (transform.position.y < -2 || transform.position.y > 10) {

			storage.currentBalls -= 1;
			Destroy (gameObject);
		}
	}
}
