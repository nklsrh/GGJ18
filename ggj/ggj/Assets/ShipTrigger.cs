using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTrigger : MonoBehaviour {

    public BaseShipController ship;
    public bool isPressAllowed = false;

    void OnTriggerStay(Collider other)
    {
        RPlayerController rp = other.GetComponent<RPlayerController>();
        if (rp != null)
        {

            if (!rp.IsCarryingItem)
            {
				ship.stationIcon.SetActive(true);
                rp.GiveControl(ship, this, isPressAllowed);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        RPlayerController rp = other.GetComponent<RPlayerController>();
        if (rp != null)
        {
            ship.stationIcon.SetActive(false);
        }
    }
}
