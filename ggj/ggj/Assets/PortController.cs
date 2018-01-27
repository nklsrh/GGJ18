using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortController : MonoBehaviour
{
    public CannonController cannon;
    public float fireRate = 1.0f;
    public PortTower tower;

    float fireTime = 90.0f;
    BaseShipController detectedShip;

	public PortController[] connectedPorts;
	public GameObject radioWave;
    public Transform shipDockTransform;

	void Start() {
		SetUpPorts ();

	}


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

	void SetUpPorts() {

		foreach (PortController port in connectedPorts)
		{
			Vector3 startPos = gameObject.transform.position + new Vector3 (0, 2, 0);

			GameObject wave = Instantiate (radioWave, startPos, Quaternion.identity);

			wave.GetComponent<RadioWaveController> ().endPos = port.transform.position + new Vector3 (0, 2, 0);
			wave.GetComponent<RadioWaveController> ().startPos = startPos;
		}
	}
}
