﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour {

    public PlayerDirector playerDirector;
    public WorldDirector worldDirector;
    public CameraController cameraController;
    public Boombox boomBox;
    public UIManager uiManager;
    public UIScreenManager uiScreenManager;

    public MainShipController ship;

	public int portsAlive = 7;

    public void StartGame()
    {
        playerDirector.onPlayerCreated += OnPlayerCreated;

        PortController[] ports = FindObjectsOfType<PortController>();
        for (int i = 0; i < ports.Length; i++)
        {
            uiManager.CreatePort(ports[i]);
			ports [i].health.onDeath += OnPortDead;
        }

        playerDirector.Setup(ship.playerSpawnPoint);

        uiScreenManager.Setup(ship);

        worldDirector.Setup();

        for (int i = 0; i < worldDirector.AiShips.Count; i++)
        {
            uiManager.CreateShip(worldDirector.AiShips[i].transform);
        }

        ship.onInterestingThingFound += OnInterestingThingFound;
        ship.onInterestingThingLost += OnInterestingThingLost;
    }

    private void OnInterestingThingFound(Transform thing, float time)
    {
        cameraController.LookAtThingOfInterest(thing, time);
        //TODO interesting music

        boomBox.PlayBattleMusic();
    }
    private void OnInterestingThingLost(Transform thing)
    {
        cameraController.StopLookAtThing(thing);
        //TODO interesting music

        boomBox.PlayTrack(playerDirector.Players.Count);
    }

    void OnPortDead () {

		portsAlive -=1;

		if (portsAlive < 1) {

			//Game End;
		}
	}
    private void OnPlayerCreated(RPlayerController player)
    {
        uiManager.CreatePlayer(player);

        if (boomBox)
        {
            boomBox.PlayTrack(playerDirector.Players.Count);
        }
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.K))
            {
                ship.health.Damage(1.0f);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                worldDirector.AiShips[0].healthController.Damage(10);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                CannonController[] cannons = ship.GetComponentsInChildren<CannonController>();
                foreach (CannonController c in cannons)
                {
                    c.loadedAmmo++;
                    c.Fire();
                }
                //worldDirector.Ports[0].health.Damage(10);
            }
        }
#endif
    }
}
