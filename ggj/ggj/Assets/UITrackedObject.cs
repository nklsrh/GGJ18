using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITrackedObject : MonoBehaviour
{
    public Vector3 offset;
    public bool trackObjectin3D = true;

    Transform trackedObject;

    public void Track(Transform trackObject)
    {
        trackedObject = trackObject;
    }
    
    void Update()
    {
        if (trackedObject != null && trackObjectin3D)
        {
            transform.position = trackedObject.position + offset;
        }
    }
}
