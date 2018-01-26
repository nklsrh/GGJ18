using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSetup : MonoBehaviour
{

    public GameObject levelObject;
    public GameObject gameplayObject;

    public bool useDebug = false;

    void Start()
    {
        if (useDebug)
        {

        }
        else
        {
            levelObject.gameObject.SetActive(false);
            Application.LoadLevelAdditive("WorldScene");
        }
    }

}
