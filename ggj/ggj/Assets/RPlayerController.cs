using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class RPlayerController : MonoBehaviour
{
    public float thrust = 100;

    public CharacterAnimationController animationController;

    public float lookLerp = 10.0f;

	public bool inStorage = false;

    public Vector3 Velocity
    {
        get
        {
            return rig.velocity;
        }
    }

    Rigidbody rig;


    InputDevice inputDevice;
    public InputDevice Input
    {
        get
        {
            return inputDevice;
        }
    }

    BaseShipController ship;

    Transform stuckTransform;
    Vector3 relativePositionToStuckTransform;

    CarryItem currentCarryItem;
    bool isCarryingItem = false;

    public bool IsCarryingItem
    {
        get
        {
            return currentCarryItem != null;
        }
    }
    public CarryItem CarryingItem
    {
        get
        {
            return currentCarryItem;
        }
    }

    public GameObject indicatorThingy;

    public HealthController health;

    public static System.Action<RPlayerController, CarryItem> onCanPickUp;
    public static System.Action<RPlayerController, CarryItem> onCannotPickUp;


    Vector3 lookingAt;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
        animationController.ShowHand(false);

        ShowIndicator(false);

        onCanPickUp += OnCanPickUpFunction;
        onCannotPickUp += OnCannotPickUpFunction;

        health.Setup(100);
    }

    private void OnCannotPickUpFunction(RPlayerController arg1, CarryItem arg2)
    {

    }

    private void OnCanPickUpFunction(RPlayerController arg1, CarryItem arg2)
    {

    }

    internal void GiveControl(BaseShipController ship, ShipTrigger trigger)
    {
		if (Input.Action1.IsPressed)
        {
			if (!ship.IsControlledByPlayer (this)) {
				//Debug.Log ("TAKE CONTROL OF " + ship.gameObject.name);
				ship.TakeControl (this);
				this.ship = ship;
				stuckTransform = trigger.transform;
				relativePositionToStuckTransform = trigger.transform.InverseTransformPoint (transform.position);
			}
		}
    }

    internal void RemoveControlOfCurrentShip(BaseShipController ship = null)
    {
        //Debug.Log("REMOVE CONTROL");
        this.ship.RemoveControl(this);
        this.ship = null;
    }



	
	void Update ()
    {
        if (ship != null)
        {
            if (Input.Action4.IsPressed)
            {
                RemoveControlOfCurrentShip();
            }
            else
            {
                transform.position = stuckTransform.TransformPoint(relativePositionToStuckTransform);

                ship.isActionButtonDown = Input.Action1.Value > 0;

                if (Input.Action1.WasPressed)
                {
                    ship.ActionButton();
                }
                if (Input.LeftStick.IsPressed)
                {
                    ship.LeftStick(Input.LeftStick.Value);
                }
                if (Input.RightStick.IsPressed)
                {
                    ship.RightStick(Input.RightStick.Value);
                }
                if (Input.RightTrigger.IsPressed)
                {
                    ship.RightTrigger(Input.RightTrigger.Value);
                }
            }
        }
        else
        {
            if (currentCarryItem != null)
            {
                if (isCarryingItem)
                {
                    if (Input.Action1.IsPressed)
                    {
                        currentCarryItem.transform.position = animationController.rootHand.position;
                        currentCarryItem.transform.rotation = animationController.rootHand.rotation;
                    }
                    else
                    {
						DropItem (currentCarryItem);
                    }
                }
                else
                {
                    if (Input.Action1.IsPressed)
                    {
						PickupItem (currentCarryItem);
                    }
                }
            }

            Vector2 movementInput = Input.LeftStick.Value;
            Vector3 movementInput3 = new Vector3(movementInput.x, 0, movementInput.y);

            Vector3 finalMovement = Camera.main.transform.TransformDirection(movementInput3);
            finalMovement = finalMovement.normalized;
            Vector2 movementFlattened = new Vector2(finalMovement.x, finalMovement.z);
            movementFlattened = movementFlattened.normalized;

            finalMovement = new Vector3(movementFlattened.x, 0, movementFlattened.y);
            rig.MovePosition(finalMovement * thrust * Time.deltaTime + transform.position);

            // slowly look at the target
            lookingAt = Vector3.Slerp(lookingAt, 9999 * finalMovement + rig.transform.position, lookLerp * Time.deltaTime);

            rig.MoveRotation(Quaternion.LookRotation(lookingAt, Vector3.up));
        }
    }


    internal void CanCarryItem(CarryItem carryItem)
    {
        if (currentCarryItem == null)
        {
            currentCarryItem = carryItem;
        }
        if (onCanPickUp != null)
        {
            onCanPickUp(this, carryItem);
        }
    }

    internal void CannotCarryItem(CarryItem carryItem)
    {
        if (currentCarryItem == carryItem)
        {
            currentCarryItem = null;
        }
        if (onCannotPickUp != null)
        {
            onCannotPickUp(this, carryItem);
        }
    }


	public void PickupItem(CarryItem i)
	{
		isCarryingItem = true;
		currentCarryItem.PickUp(this);
        //animationController.ShowHand(true);
    }

	public void DropItem(CarryItem i)
	{
		isCarryingItem = false;
		currentCarryItem.Drop(this);
		currentCarryItem = null;
	}

    internal void SetInput(InputDevice inputDevice)
    {
        this.inputDevice = inputDevice;
    }

    public void ShowIndicator(bool shouldShow)
    {
        indicatorThingy.SetActive(shouldShow);
    }
}



// An example of how to map keyboard/mouse input (or anything else) to a virtual device.
//
public class VirtualDevice : InputDevice
{
    const float sensitivity = 0.1f;
    const float mouseScale = 0.05f;

    // To store keyboard x, y for smoothing.
    float kx, ky;

    // To store mouse x, y for smoothing.
    float mx, my;


    public VirtualDevice()
        : base("Virtual Controller")
    {
        // We need to add the controls we want to emulate here.
        // For this example we'll only have analog sticks and four action buttons.

        AddControl(InputControlType.LeftStickLeft, "Left Stick Left");
        AddControl(InputControlType.LeftStickRight, "Left Stick Right");
        AddControl(InputControlType.LeftStickUp, "Left Stick Up");
        AddControl(InputControlType.LeftStickDown, "Left Stick Down");

        AddControl(InputControlType.RightStickLeft, "Right Stick Left");
        AddControl(InputControlType.RightStickRight, "Right Stick Right");
        AddControl(InputControlType.RightStickUp, "Right Stick Up");
        AddControl(InputControlType.RightStickDown, "Right Stick Down");

        AddControl(InputControlType.Action1, "A");
        AddControl(InputControlType.Action2, "B");
        AddControl(InputControlType.Action3, "X");
        AddControl(InputControlType.Action4, "Y");
    }


    // This method will be called by the input manager every update tick.
    // You are expected to update control states where appropriate passing
    // through the updateTick and deltaTime unmodified.
    //
    public override void Update(ulong updateTick, float deltaTime)
    {
        // Get a smoothed vector from keyboard input (see methods below).
        var leftStickVector = GetVectorFromKeyboard(deltaTime, false);

        // With a vector you can use UpdateLeftStickWithValue()
        UpdateLeftStickWithValue(leftStickVector, updateTick, deltaTime);

        // Get a smoothed vector from mouse input (see methods below).
        var rightStickVector = GetVectorFromMouse(deltaTime, true);

        // Submit it as a raw value so it doesn't get processed down to -1.0 to +1.0 range.
        UpdateRightStickWithRawValue(rightStickVector, updateTick, deltaTime);

        // Read from keyboard input presses to submit into action buttons.
        UpdateWithState(InputControlType.Action1, Input.GetKey(KeyCode.Space), updateTick, deltaTime);
        UpdateWithState(InputControlType.Action2, Input.GetKey(KeyCode.R), updateTick, deltaTime);
        UpdateWithState(InputControlType.Action3, Input.GetKey(KeyCode.F), updateTick, deltaTime);
        UpdateWithState(InputControlType.Action4, Input.GetKey(KeyCode.E), updateTick, deltaTime);

        // Commit the current state of all controls.
        // This may only be done once per update tick. Updates submissions (like those above)
        // can be done multiple times per tick (for example, to aggregate multiple sources) 
        // but must be followed by a single commit to submit the final value to the control.
        Commit(updateTick, deltaTime);
    }


    Vector2 GetVectorFromKeyboard(float deltaTime, bool smoothed)
    {
        if (smoothed)
        {
            kx = ApplySmoothing(kx, GetXFromKeyboard(), deltaTime, sensitivity);
            ky = ApplySmoothing(ky, GetYFromKeyboard(), deltaTime, sensitivity);
        }
        else
        {
            kx = GetXFromKeyboard();
            ky = GetYFromKeyboard();
        }
        return new Vector2(kx, ky);
    }


    float GetXFromKeyboard()
    {
        var l = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) ? -1.0f : 0.0f;
        var r = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) ? 1.0f : 0.0f;
        return l + r;
    }


    float GetYFromKeyboard()
    {
        var u = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) ? 1.0f : 0.0f;
        var d = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) ? -1.0f : 0.0f;
        return u + d;
    }


    Vector2 GetVectorFromMouse(float deltaTime, bool smoothed)
    {
        if (smoothed)
        {
            mx = ApplySmoothing(mx, Input.GetAxisRaw("mouse x") * mouseScale, deltaTime, sensitivity);
            my = ApplySmoothing(my, Input.GetAxisRaw("mouse y") * mouseScale, deltaTime, sensitivity);
        }
        else
        {
            mx = Input.GetAxisRaw("mouse x") * mouseScale;
            my = Input.GetAxisRaw("mouse y") * mouseScale;
        }
        return new Vector2(mx, my);
    }


    float ApplySmoothing(float lastValue, float thisValue, float deltaTime, float sensitivity)
    {
        sensitivity = Mathf.Clamp(sensitivity, 0.001f, 1.0f);

        if (Mathf.Approximately(sensitivity, 1.0f))
        {
            return thisValue;
        }

        return Mathf.Lerp(lastValue, thisValue, deltaTime * sensitivity * 100.0f);
    }
}
