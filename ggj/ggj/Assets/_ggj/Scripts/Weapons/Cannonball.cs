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

        PortCollider port = other.gameObject.GetComponent<PortCollider>();
        if (port != null)
        {
            port.portController.health.Damage(damage);
            HitSomething();
        }
    }
}
