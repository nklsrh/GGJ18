using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour {

    public PlayerDirector playerDirector;

    public UIManager uiManager;


    public void StartGame()
    {
        playerDirector.onPlayerCreated += OnPlayerCreated;

        PortController[] ports = FindObjectsOfType<PortController>();
        for (int i = 0; i < ports.Length; i++)
        {
            uiManager.CreatePort(ports[i]);
        }
	}

    private void OnPlayerCreated(RPlayerController player)
    {
        uiManager.CreatePlayer(player);
    }
}
