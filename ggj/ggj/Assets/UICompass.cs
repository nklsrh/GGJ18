using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICompass : MonoBehaviour
{
    ShipController myShip;

    public void Setup(ShipController ship)
    {
        myShip = ship;
    }

    void Update()
    {
        if (myShip)
        {
            transform.localRotation = Quaternion.Euler(0, 0, myShip.transform.rotation.y);
        }
    }
}
