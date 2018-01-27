using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour {

    public PlayerDirector playerDirector;

    public UIManager uiManager;
    public UIScreenManager uiScreenManager;

    public ShipController ship;

    public void StartGame()
    {
        playerDirector.onPlayerCreated += OnPlayerCreated;

        playerDirector.SetSpawnPoint(ship.playerSpawnPoint);

        PortController[] ports = FindObjectsOfType<PortController>();
        for (int i = 0; i < ports.Length; i++)
        {
            uiManager.CreatePort(ports[i]);
        }

        uiScreenManager.Setup(ship);

    }

    private void OnPlayerCreated(RPlayerController player)
    {
        uiManager.CreatePlayer(player);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
            ship.health.Damage(1.0f);
        }
    }
}
