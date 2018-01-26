using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CannonController : BaseShipController {
    
    public float firePower = 1000;

    public Transform projectileSpawnTransform;

    public Projectile templateProjectile;

    ProjectilePoolManager poolManager;

    void Start()
    {
        poolManager = new ProjectilePoolManager(5);
        poolManager.Setup();
    }

    public void Fire()
    {
        Projectile p = poolManager.AddProjectile(templateProjectile);

        p.transform.position = projectileSpawnTransform.position;
        p.transform.rotation = projectileSpawnTransform.rotation;
        p.transform.localScale = Vector3.one;

        p.SetVelocity(firePower * p.transform.forward);
    }

    public override void ActionButton()
    {
        base.ActionButton();

        Fire();
    }
}
