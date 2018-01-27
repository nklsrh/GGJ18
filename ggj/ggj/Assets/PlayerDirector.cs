using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerDirector : MonoBehaviour
{
    public RPlayerController playerPrefab;
    public List<RPlayerController> Players
    {
        get
        {
            return players;
        }
    }
    List<RPlayerController> players = new List<RPlayerController>();

    public System.Action<RPlayerController> onPlayerCreated;


    VirtualDevice virtualKeyboardDevice;

    const int MAX_PLAYERS = 4;

    Transform spawnPoint;

    bool isActive = false;

    public void Setup(Transform playerSpawnPoint)
    {
        SetSpawnPoint(playerSpawnPoint);

        playerPrefab.gameObject.SetActive(false);

        virtualKeyboardDevice = new VirtualDevice();
        // We hook into the OnSetup callback to ensure the device is attached
        // after the InputManager has had a chance to initialize properly.
        InputManager.OnSetup += () => InputManager.AttachDevice(virtualKeyboardDevice);

        InputManager.OnDeviceDetached += OnDeviceDetached;

        isActive = true;
    }

    internal void SetSpawnPoint(Transform playerSpawnPoint)
    {
        spawnPoint = playerSpawnPoint;
    }

    void Update()
    {
        if (isActive)
        {
            InputDevice input = InputManager.ActiveDevice;

            if (virtualKeyboardDevice != null && PressedJoin(virtualKeyboardDevice))
            {
                input = virtualKeyboardDevice;
            }

            if (PressedJoin(input))
            {
                if (FindPlayerFromInput(input) == null)
                {
                    CreatePlayer(input);
                }
            }

            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].transform.position.y < -2 || players[i].transform.position.y > 10)
                {
                    Players[i].Respawn(spawnPoint);
                }
            }
        }
    }

    bool PressedJoin(InputDevice inputDevice)
    {
        return inputDevice.Action1.WasPressed 
            || inputDevice.Action2.WasPressed 
            || inputDevice.Action3.WasPressed 
            || inputDevice.Action4.WasPressed;
    }




    RPlayerController FindPlayerFromInput(InputDevice inputDevice)
    {
        var playerCount = players.Count;
        for (var i = 0; i < playerCount; i++)
        {
            var player = players[i];
            if (player.Input == inputDevice)
            {
                return player;
            }
        }

        return null;
    }

    void OnDeviceDetached(InputDevice inputDevice)
    {
        RPlayerController player = FindPlayerFromInput(inputDevice);
        if (player != null)
        {
            RemovePlayer(player);
        }
    }


    RPlayerController CreatePlayer(InputDevice inputDevice)
    {
        if (players.Count < MAX_PLAYERS)
        {
            playerPrefab.gameObject.SetActive(true);

            RPlayerController player = Instantiate(playerPrefab);
            player.transform.SetParent(playerPrefab.transform.parent);
            player.transform.position = spawnPoint.position;

            player.SetInput(inputDevice);
            players.Add(player);

            player.gameObject.SetActive(true);
            playerPrefab.gameObject.SetActive(false);

            if (onPlayerCreated != null)
            {
                onPlayerCreated.Invoke(player);
            }

            return player;
        }

        return null;
    }


    void RemovePlayer(RPlayerController player)
    {
        players.Remove(player);
        Destroy(player.gameObject);
    }
}
