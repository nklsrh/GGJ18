using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : Projectile
{
    void OnCollisionEnter(Collision other)
    {
        AIShipController ship = other.gameObject.GetComponent<AIShipController>();
        if (ship != null)
        {
            ship.healthController.Damage(damage);
            HitSomething();
        }

        PortController port = other.gameObject.GetComponent<PortController>();
        if (port != null)
        {
            port.health.Damage(damage);
            HitSomething();
        }
    }
}
