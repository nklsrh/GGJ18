using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SailController : BaseShipController
{
    public Transform sailTransform;
    public TurnController ship;

    float sailAmount = 0.0f;
    float brakingAmount = 0.0f;

    public override void LeftStick(Vector2 stick)
    {
        brakingAmount = Mathf.Clamp01((stick.y - 1.0f) / -2.0f);

        Braking(brakingAmount);

        base.LeftStick(stick);
    }

    void Braking(float amount)
    {
        ship.SetSailPercentage(amount);
    }

    protected override void Update()
    {
        sailAmount = Mathf.Lerp(sailAmount, brakingAmount, 0.5f * Time.deltaTime);

        sailTransform.localScale = new Vector3(1, 1, 0.2f + 0.65f * sailAmount);

        base.Update();
    }
    
}
