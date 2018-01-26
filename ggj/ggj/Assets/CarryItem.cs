using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryItem : MonoBehaviour
{


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
}
