using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioWaveController : MonoBehaviour {

	public Vector3 startPos;
	public Vector3 endPos;

	int isReturning = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float dist= 10000;
		if (isReturning == 0) {
			dist = Vector3.Distance (gameObject.transform.position, endPos);
			transform.position = Vector3.Lerp (gameObject.transform.position, endPos, 0.1f);

			if (dist < 5) {
                //isReturning = 1;
                transform.position = startPos;
			}

		} else {
			dist = Vector3.Distance (gameObject.transform.position, startPos);
			transform.position = Vector3.Lerp (gameObject.transform.position, startPos, 0.1f);
			if (dist < 5) {
				isReturning = 0;
			}
		}

	}
}
