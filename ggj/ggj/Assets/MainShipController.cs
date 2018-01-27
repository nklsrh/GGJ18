using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainShipController : BaseShipController
{
    public HealthController health;
    public Transform playerSpawnPoint;

    void Start()
    {
        health.Setup(100);
        health.onDeath += OnDeath;
    }

    private void OnDeath()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    protected override void Update()
    {
        base.Update();
    }
}
