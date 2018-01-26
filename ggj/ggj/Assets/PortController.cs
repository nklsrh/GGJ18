using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortController : MonoBehaviour
{
    public CannonController cannon;

    void OnTriggerEnter(Collider other)
    {
        TurnController ship = other.GetComponent<TurnController>();
        if (ship != null)
        {
            Debug.Log("SHIP ENTERED PORT");
        }
    }
}
