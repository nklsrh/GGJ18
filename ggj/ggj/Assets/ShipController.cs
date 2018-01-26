using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : BaseShipController
{
    public HealthController health;

    void Start()
    {
        health.Setup(100);
    }
    
}
