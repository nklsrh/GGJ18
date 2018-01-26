using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreenManager : MonoBehaviour
{
    public UICompass compass;
    public UIHealthbar health;

    ShipController myShip;
    
    public void Setup(ShipController ship)
    {
        myShip = ship;

        compass.Setup(ship);
        health.Setup(ship.health);
        health.trackObjectin3D = false;
    }
}
