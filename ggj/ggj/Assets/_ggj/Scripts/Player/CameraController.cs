using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerDirector playerDirector;
    public TurnController ship;
    public SailController sail;

    public Vector3 offset = Vector3.up + Vector3.back * 20;
    public Vector3 offsetShipDriving = Vector3.up + Vector3.back * 20;
    public Vector3 offsetLookDriving = Vector3.up + Vector3.back * 20;

    public bool isLookingAtTarget = false;
    public bool whenDrivingShipAlignWithForward = true;

    public float FOVDriving = 25;
    public float FOVRunning = 35;

    public float lookLerp = 10.0f;
    public float moveLerp = 10.0f;
    public float fovLerp = 10.0f;

    public float lookLerpDrive = 0.5f;
    public float lookLerpLerp = 10.0f;

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

    Vector3 movementPosition;
    Vector3 lookAtPosition;

    float fovCurrent = 35;
    float currentLookLerp = 0;

    void FixedUpdate()
    {
        transform.position = movementPosition;

        if (isLookingAtTarget)
        {
            transform.LookAt(lookAtPosition);
        }

        Vector3 targetPosition = Vector3.zero;
        if (playerDirector.Players.Count > 0)
        {
            foreach (RPlayerController rp in playerDirector.Players)
            {
                targetPosition += rp.transform.position;
            }
            targetPosition /= playerDirector.Players.Count;
        }
        else
        {
            targetPosition = ship.transform.position;
        }

        float targetFOV = FOVRunning;
        Vector3 lookatOffset = Vector3.zero;
        float lookLerpFinal = lookLerp;

        Vector3 off = offset;
        if (ship.IsPlayerControlled() || sail.IsPlayerControlled())
        {
            off = whenDrivingShipAlignWithForward ? 
                ship.transform.TransformPoint(offsetShipDriving) - targetPosition :
                offsetShipDriving;

            lookatOffset = ship.transform.TransformPoint(offsetLookDriving) - targetPosition;

            targetFOV = FOVDriving;

            lookLerpFinal = lookLerpDrive;
        }

        currentLookLerp = Mathf.Lerp(currentLookLerp, lookLerpFinal, lookLerpLerp * Time.deltaTime);

        Camera.fieldOfView = Mathf.Lerp(Camera.fieldOfView, targetFOV, fovLerp * Time.deltaTime);

        movementPosition = Vector3.Slerp(movementPosition, targetPosition + off, moveLerp * Time.deltaTime);
        lookAtPosition = Vector3.Slerp(lookAtPosition, targetPosition + lookatOffset, lookLerpFinal * Time.deltaTime);
    }

}
