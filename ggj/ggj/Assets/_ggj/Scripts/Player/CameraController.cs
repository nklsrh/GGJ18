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
    public bool whenNOTDrivingShipAlignWithForward = false;

    public float FOVDriving = 25;
    public float FOVRunning = 35;

    public float lookLerp = 10.0f;
    public float moveLerp = 10.0f;
    public float fovLerp = 10.0f;

    public float lookLerpDrive = 0.5f;
    public float lookLerpLerp = 10.0f;

    public Transform thingOfInterest = null;
    public float thingOfInterstLookatLerp = 10.0f;
    public Vector3 interestThinglookAtOFfset = Vector3.up * 5;

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

    Vector3 finalLookAtPosition;

    public void LookAtThingOfInterest(Transform thing, float howLong)
    {
        // DISABLE FOR GAEMJAM

        //thingOfInterest = thing;
    }

    public void StopLookAtThing(Transform thing)
    {
        // DISABLE FOR GAEMJAM

        //if (thingOfInterest == thing)
        //{
        //    thingOfInterest = null;
        //}
    }

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


        bool isLookingAtInterestingThing = false;

        if (thingOfInterest != null)
        {
            float dot = Vector3.Dot(transform.forward, thingOfInterest.transform.position - transform.position);
            Debug.Log("DOT: " + dot + ":  DISTSQ"+ (transform.position - thingOfInterest.transform.position).sqrMagnitude);
            if (dot > 140 && (transform.position - thingOfInterest.transform.position).sqrMagnitude < 85000)
            {
                isLookingAtInterestingThing = true;
            }
        }

        finalLookAtPosition = Vector3.Lerp(finalLookAtPosition,
            isLookingAtInterestingThing ? 
            (targetPosition + interestThinglookAtOFfset + 
            (thingOfInterest.position + interestThinglookAtOFfset - targetPosition) * 0.05f) : targetPosition, 

            (isLookingAtInterestingThing ? thingOfInterstLookatLerp : 1000) * Time.deltaTime);

        float targetFOV = FOVRunning;
        Vector3 lookatOffset = Vector3.zero;
        float lookLerpFinal = lookLerp;

        Vector3 off = whenNOTDrivingShipAlignWithForward ?
            ship.transform.TransformPoint(offset) - finalLookAtPosition :
            offset;

        if (ship.IsPlayerControlled() || sail.IsPlayerControlled())
        {
            off = whenDrivingShipAlignWithForward ? 
                ship.transform.TransformPoint(offsetShipDriving) - finalLookAtPosition :
                offsetShipDriving;

            lookatOffset = ship.transform.TransformPoint(offsetLookDriving) - finalLookAtPosition;

            targetFOV = FOVDriving;

            lookLerpFinal = lookLerpDrive;
        }

        if (isLookingAtInterestingThing)
        {
            lookatOffset = Vector3.zero;
        }

        currentLookLerp = Mathf.Lerp(currentLookLerp, lookLerpFinal, lookLerpLerp * Time.deltaTime);

        Camera.fieldOfView = Mathf.Lerp(Camera.fieldOfView, targetFOV, fovLerp * Time.deltaTime);

        movementPosition = Vector3.Slerp(movementPosition, finalLookAtPosition + off, moveLerp * Time.deltaTime);
        lookAtPosition = Vector3.Slerp(lookAtPosition, finalLookAtPosition + lookatOffset, lookLerpFinal * Time.deltaTime);
    }

}
