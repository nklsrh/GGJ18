using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreenManager : MonoBehaviour
{
    public UICompass compass;
    public UIHealthbar health;
    public UICompass minimap;

    MainShipController myShip;
    
    public void Setup(MainShipController ship)
    {
        myShip = ship;

        compass.Setup(ship);
        health.Setup(ship.health);
        health.trackObjectin3D = false;

        minimap.Setup(ship);
    }
}
