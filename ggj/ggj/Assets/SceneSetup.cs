using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SceneSetup : MonoBehaviour
{
    public GameObject levelObject;
    public GameObject gameplayObject;

    public bool useDebug = false;

    public GameDirector gameDirector;

    public CameraController cam;

    public Canvas overlayCanvas;
    public UIScreenManager uiManager;

    bool isGameStarted = false;
    float waitPeriod = 3.0f;
    VirtualDevice virtualKeyboardDevice;

    void Start()
    {
        cam.enabled = false;
        overlayCanvas.gameObject.SetActive(true);
        uiManager.gameObject.SetActive(false);


        gameDirector.onGameEnd += OnGameEnd;

        virtualKeyboardDevice = new VirtualDevice();
        // We hook into the OnSetup callback to ensure the device is attached
        // after the InputManager has had a chance to initialize properly.
        InputManager.OnSetup += () => InputManager.AttachDevice(virtualKeyboardDevice);

        if (useDebug)
        {
            levelObject.gameObject.SetActive(true);
        }
        else
        {
            levelObject.gameObject.SetActive(false);
            Application.LoadLevelAdditive("WorldScene");
        }
    }

    private void OnGameEnd(bool isWin)
    {
        //if (!isWin)
        {
            StartCoroutine(WaitThenRestartGame());
        }
    }

    IEnumerator WaitThenRestartGame()
    {
        yield return new WaitForSeconds(8.0f);

        Application.LoadLevel(Application.loadedLevel);
    }

    void Update()
    {
        if (!isGameStarted && waitPeriod < 0)
        {
            InputDevice input = InputManager.ActiveDevice;

            //if (virtualKeyboardDevice != null)
            //{
            //    input = virtualKeyboardDevice;
            //}

            if (input.Action1.IsPressed || virtualKeyboardDevice.Action1.IsPressed)
            {
                gameDirector.StartGame();
                waitPeriod = 0;
                isGameStarted = true;
                cam.enabled = true;
                overlayCanvas.gameObject.SetActive(false);
                uiManager.gameObject.SetActive(true);
            }
        }
        else if (waitPeriod > 0)
        {
            waitPeriod -= Time.deltaTime;
        }
    }
}
