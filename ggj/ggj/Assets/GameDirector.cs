using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour {

    public PlayerDirector playerDirector;

    public UIManager uiManager;

	void Start ()
    {
        playerDirector.onPlayerCreated += OnPlayerCreated;
	}

    private void OnPlayerCreated(RPlayerController player)
    {
        uiManager.CreatePlayer(player);
    }
}
