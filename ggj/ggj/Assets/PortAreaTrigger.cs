using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortAreaTrigger : MonoBehaviour {

    public PortController port;

    void OnTriggerEnter(Collider other)
    {
        BaseShipController ship = other.GetComponent<BaseShipController>();
        if (ship != null)
        {
            port.DetectShip(ship);
        }
    }
    void OnTriggerExit(Collider other)
    {
        BaseShipController ship = other.GetComponent<BaseShipController>();
        if (ship != null)
        {
            port.UndetectShip(ship);
        }
    }
}
