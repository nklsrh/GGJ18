﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSetup : MonoBehaviour
{
    public GameObject levelObject;
    public GameObject gameplayObject;

    public bool useDebug = false;

    public GameDirector gameDirector;

    void Start()
    {
        if (useDebug)
        {
            levelObject.gameObject.SetActive(true);
        }
        else
        {
            levelObject.gameObject.SetActive(false);
            Application.LoadLevelAdditive("WorldScene");
        }

        StartCoroutine(WaitThenStartGame());
    }


    IEnumerator WaitThenStartGame()
    {
        yield return new WaitForSeconds(0.5f);


        gameDirector.StartGame();
    }

}
