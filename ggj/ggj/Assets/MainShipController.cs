using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainShipController : BaseShipController
{
    public HealthController health;
    public Transform playerSpawnPoint;

    public int lootCollected = 0;

    public System.Action<Transform, float> onInterestingThingFound;
    public System.Action<Transform> onInterestingThingLost;

    public ExplosionObject explosion;

    void Start()
    {
        health.Setup(100);
        health.onDeath += OnDeath;
    }

    private void OnDeath()
    {
        if (explosion !=null)
        {
            explosion.Setup();
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    internal void CollectLoot(LootItem lootItem)
    {
        lootCollected += lootItem.lootAmount;

        Destroy(lootItem.gameObject);
    }

    public void LookAtInterestingThing(Transform thing, float time)
    {
        if (onInterestingThingFound != null)
        {
            onInterestingThingFound.Invoke(thing, time);
        }
    }
    public void StopLookingAtThing(Transform thing)
    {
        if (onInterestingThingLost != null)
        {
            onInterestingThingLost.Invoke(thing);
        }
    }
}
