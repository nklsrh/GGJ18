using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBackCollider : MonoBehaviour {

    public float pushBackForce = 100000;
    public float damageToShip = 5f;

    void OnCollisionEnter(Collision other)
    {
        TurnController ship = other.gameObject.GetComponent<TurnController>();
        if (ship != null)
        {
            Debug.Log("DASHED SHIP");

            ship.PushBack(-other.relativeVelocity * pushBackForce);
            ship.MainShip.health.Damage(damageToShip);
        }
    }
}
