using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortController : MonoBehaviour
{
    public CannonController cannon;
    public float fireRate = 1.0f;

    float fireTime = 90.0f;
    BaseShipController detectedShip;

	public PortController[] connectedPorts;
	public GameObject radioWave;


    public Transform shipDockTransform;

    public HealthController health;


    public LootDropper loot;
    public Transform lootSpawnTransform;
    public ExplosionObject explosion;

    void Start()
    {
        health.onDeath += OnDeath;
    }

    private void OnDeath()
    {
        loot.DropLoot(lootSpawnTransform.position);
        if (explosion != null)
        {
            explosion.Setup();
        }
    }

    public void Setup()
    {
		SetUpPorts ();
	}


    void Update()
    {
        if (health.IsAlive)
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

    internal void DetectShip(BaseShipController ship)
    {
        detectedShip = ship;
    }
    internal void UndetectShip(BaseShipController ship)
    {
        if (detectedShip == ship)
        {
            detectedShip = null;
        }
    }

    void SetUpPorts() {

		foreach (PortController port in connectedPorts)
		{
            Vector3 startPos = shipDockTransform.position;

			GameObject wave = Instantiate (radioWave, startPos, Quaternion.identity);

			wave.GetComponent<RadioWaveController> ().endPos = port.transform.position;
			wave.GetComponent<RadioWaveController> ().startPos = startPos;
		}
	}
}
