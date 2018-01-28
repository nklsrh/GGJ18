using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiShipDetectTrigger : MonoBehaviour {

    public AIShipController aiShip;

    void OnTriggerEnter(Collider other)
    {
        BaseShipController ship = other.GetComponent<BaseShipController>();
        if (ship != null)
        {
            aiShip.DetectShip(ship);
        }
    }
    void OnTriggerExit(Collider other)
    {
        BaseShipController ship = other.GetComponent<BaseShipController>();
        if (ship != null)
        {
            aiShip.UndetectShip(ship);
        }
    }
}
