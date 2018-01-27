using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LootItem : MonoBehaviour {

    public int lootAmount = 1;

    Rigidbody rig;

    void Start ()
    {
        rig = GetComponent<Rigidbody>();
	}

    void OnTriggerEnter(Collider other)
    {
        BaseShipController ship = other.GetComponent<BaseShipController>();
        if (ship != null)
        {
            ship.MainShip.CollectLoot(this);
        }
    }
}
