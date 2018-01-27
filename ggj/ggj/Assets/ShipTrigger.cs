using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTrigger : MonoBehaviour {

    public BaseShipController ship;

    void OnTriggerStay(Collider other)
    {
        RPlayerController rp = other.GetComponent<RPlayerController>();
        if (rp != null)
        {
            if (rp.IsCarryingItem == false)
            {
                rp.GiveControl(ship, this);
            }
        }
    }
}
