using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryItem : MonoBehaviour
{
    Rigidbody rig;
    RPlayerController playerControlled;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        RPlayerController rp = other.GetComponent<RPlayerController>();
        if (rp != null)
        {
            rp.CanCarryItem(this);
        }
    }

    void OnTriggerExit(Collider other)
    {
        RPlayerController rp = other.GetComponent<RPlayerController>();
        if (rp != null)
        {
            rp.CannotCarryItem(this);
        }
    }

    internal void PickUp(RPlayerController player)
    {
        playerControlled = player;
    }

    public void Drop(RPlayerController player)
    {
        if (player == playerControlled)
        {
            playerControlled = null;
        }
    }
}
