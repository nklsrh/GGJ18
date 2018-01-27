using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballEnemy : Projectile
{
    void OnCollisionEnter(Collision other)
    {
        BaseShipController ship = other.gameObject.GetComponent<BaseShipController>();
        //Debug.Log("hit by " + other.gameObject.name);
        if (ship != null)
        {
            ship.MainShip.health.Damage(damage);
            Die();
        }
    }
}
