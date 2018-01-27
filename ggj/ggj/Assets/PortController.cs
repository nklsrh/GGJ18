using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortController : MonoBehaviour
{
    public CannonController cannon;
    public float fireRate = 1.0f;

    float fireTime = 90.0f;
    BaseShipController detectedShip;

    void OnTriggerEnter(Collider other)
    {
        BaseShipController ship = other.GetComponent<BaseShipController>();
        if (ship != null)
        {
            detectedShip = ship;
        }
    }

    void Update()
    {
        if (detectedShip != null)
        {
            if (fireTime >= fireRate)
            {
                cannon.transform.LookAt(detectedShip.transform.position + Vector3.up * 100);
                cannon.Fire();
                fireTime = 0;
            }
            else
            {
                fireTime += Time.deltaTime;
            }
        }
    }
}
