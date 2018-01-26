using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerDirector playerDirector;
    public ShipController ship;

    public Vector3 offset = Vector3.up + Vector3.back * 20;
    public Vector3 offsetShipDriving = Vector3.up + Vector3.back * 20;

    public bool isLookingAtTarget = false;
    public bool whenDrivingShipAlignWithForward = true;

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

    Vector3 movementPosition;
    Vector3 lookAtPosition;

    void Update()
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

        Vector3 off = offset;
        if (ship.IsPlayerControlled())
        {
            off = whenDrivingShipAlignWithForward ? 
                ship.transform.TransformPoint(offsetShipDriving) :
                offsetShipDriving;
        }

        movementPosition = Vector3.Slerp(movementPosition, targetPosition + off, moveLerp * Time.deltaTime);
        lookAtPosition = Vector3.Slerp(lookAtPosition, targetPosition, lookLerp * Time.deltaTime);
    }

}
