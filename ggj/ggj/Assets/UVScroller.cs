using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVScroller : MonoBehaviour
{
    public Vector2 offsetPerSecond;

    Material m;

	void Start ()
    {
        m = GetComponent<MeshRenderer>().sharedMaterial;
	}
	
	void Update ()
    {
        m.SetTextureOffset("_MainTex", m.GetTextureOffset("_MainTex") + offsetPerSecond * Time.deltaTime);

    }
}
