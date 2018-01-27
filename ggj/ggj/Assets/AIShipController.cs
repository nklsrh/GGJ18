using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AIShipController : MonoBehaviour
{
    [Header("Speed")]
    public float thrust = 10000;
    public float maxVelocity = 100;

    [Header("Health")]
    public HealthController healthController;

    [Header("Loot")]
    public LootDropper loot;

    [Header("Explosion")]
    public ExplosionObject explosion;

    Transform nextTarget;
    Rigidbody rig;

    public System.Action<AIShipController> onRouteComplete;

    void Start()
    {
        rig = GetComponent<Rigidbody>();

        healthController.onDeath += OnDeath;
    }

    private void OnDeath()
    {
        loot.DropLoot(transform.position);
        if (explosion != null)
        {
            explosion.Setup();
        }

        StartCoroutine(WaitThenDie());
    }

    internal void Setup(int health)
    {
        healthController.Setup(health);
    }

    public void SetTarget(Transform end)
    {
        nextTarget = end;
    }

    IEnumerator WaitThenDie()
    {
        yield return new WaitForSeconds(5.0f);

        if (onRouteComplete != null)
        {
            onRouteComplete.Invoke(this);
        }

        yield return null;
    }

    void Update()
    {
        if (nextTarget != null)
        {
            if ((nextTarget.position - transform.position).sqrMagnitude < 150)
            {
                if (onRouteComplete != null)
                {
                    onRouteComplete.Invoke(this);
                }
            }
            else
            {
                Debug.DrawLine(nextTarget.position, transform.position, Color.red);

                Vector3 di = transform.forward.normalized;
                di.Scale(new Vector3(1, 0, 1));

                if (rig.velocity.sqrMagnitude < maxVelocity)
                {
                    rig.AddForce(di * thrust * Time.deltaTime);
                }
                else
                {
                    rig.angularVelocity = rig.angularVelocity.normalized * maxVelocity * maxVelocity;
                }

                rig.rotation = Quaternion.LookRotation((nextTarget.position - transform.position).normalized, Vector3.up);
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            }
        }
    }
}
