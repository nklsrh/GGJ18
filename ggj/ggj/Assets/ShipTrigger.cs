using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTrigger : MonoBehaviour {

    public BaseShipController ship;

    void OnTriggerEnter(Collider other)
    {
        RPlayerController rp = other.GetComponent<RPlayerController>();
        if (rp != null)
        {
            rp.GiveControl(ship, this);
        }
    }

    //void OnTriggerExit(Collider other)
    //{
    //    RPlayerController rp = other.GetComponent<RPlayerController>();
    //    if (rp != null)
    //    {
    //        rp.RemoveControlOfCurrentShip(ship);
    //    }
    //}
}
