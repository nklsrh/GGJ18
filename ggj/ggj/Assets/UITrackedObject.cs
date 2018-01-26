using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITrackedObject : MonoBehaviour
{
    public Vector3 offset;

    Transform trackedObject;

    public void Track(Transform trackObject)
    {
        trackedObject = trackObject;
    }
    
    void Update()
    {
        if (trackedObject != null)
        {
            transform.position = trackedObject.position + offset;
        }
    }
}
