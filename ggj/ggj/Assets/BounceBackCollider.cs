using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBackCollider : MonoBehaviour {

    public float pushBackForce = 1000;

    void OnCollisionEnter(Collision other)
    {
        TurnController ship = other.gameObject.GetComponent<TurnController>();
        if (ship != null)
        {
            Debug.Log("DASHED SHIP");

            ship.PushBack(-other.relativeVelocity * pushBackForce);
        }
    }
}
