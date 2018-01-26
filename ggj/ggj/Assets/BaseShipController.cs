using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShipController : MonoBehaviour
{
    [NonSerialized]
    public List<RPlayerController> playersControlling = new List<RPlayerController>();

    public virtual void ActionButton()
    {

    }

    public virtual void LeftTrigger(float amount)
    {

    }

    public virtual void RightTrigger (float amount)
    {

    }

    public virtual void LeftStick (Vector2 stick)
    {

    }

    public virtual void RightStick (Vector2 stick)
    {

    }

    public void TakeControl (RPlayerController playerController)
    {
        playersControlling.Add(playerController);
    }

    internal void RemoveControl(RPlayerController playerController)
    {
        playersControlling.Remove(playerController);
    }

    internal bool IsPlayerControlled()
    {
        return playersControlling != null && playersControlling.Count > 0;
    }
    internal bool IsControlledByPlayer(RPlayerController playerController)
    {
        return playersControlling != null && playersControlling.Contains(playerController);
    }
}
