using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MastController : BaseShipController
{
    public Transform mastTransform;

    public override void LeftStick(Vector2 stick)
    {
        mastTransform.Rotate(Vector3.up * stick.x);

        base.LeftStick(stick);
    }
}
