﻿using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 offset = Vector3.up + Vector3.back * 20;
    public bool useLocalOffset = false;

    public float lookForward = 1;
    public float lookUsingVelocity = 1;
    public bool disableRotation = false;

    public float lookLerp = 10.0f;
    public float moveLerp = 10.0f;

    public Camera Camera
    {
        get
        {
            if (thisCamera != null)
            {
                return thisCamera;
            }
            return (thisCamera = GetComponent<Camera>());
        }
    }
    
    Camera thisCamera;

    public Transform targetTransform;
    public RPlayerController targetPlayer;

    Vector3 movementPosition;
    Vector3 lookAtPosition;

    public void Setup(Transform otherTarget)
    {
        this.targetTransform = otherTarget;
        this.targetPlayer = null;
    }

    public void Setup(RPlayerController player)
    {
        this.targetPlayer = player;
        this.targetTransform = player.transform;

        lookAtPosition = player.transform.position;
    }

    void Update()
    {
        transform.position = movementPosition;

        if (!disableRotation)
        {
            transform.LookAt(lookAtPosition);
        }

        if (targetTransform)
        {
            Vector3 wantedPosition = useLocalOffset ?
                targetTransform.TransformPoint(offset) :
                targetTransform.position + offset;

            movementPosition = Vector3.Slerp(movementPosition, wantedPosition, moveLerp * Time.deltaTime);
            lookAtPosition = Vector3.Slerp(lookAtPosition, 
                targetTransform.position + (targetTransform.forward * lookForward) + 
                ((targetPlayer != null ? targetPlayer.Velocity : Vector3.zero) 
                * lookUsingVelocity), lookLerp * Time.deltaTime);
        }
    }

}
