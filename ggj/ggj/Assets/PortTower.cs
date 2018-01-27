using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortTower : MonoBehaviour
{
    public HealthController health;
    public LootDropper loot;
    public Transform lootSpawnTransform;

    void Start()
    {
        health.onDeath += OnDeath;
    }

    private void OnDeath()
    {
        loot.DropLoot(lootSpawnTransform.position);
    }
}
