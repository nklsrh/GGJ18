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
            Die();
        }
    }
}
