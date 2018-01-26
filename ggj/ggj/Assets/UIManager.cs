using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public UIHealthbar healthbarTemplate;

    public void CreatePlayer(RPlayerController player)
    {
        UIHealthbar newBar = Instantiate(healthbarTemplate);
        newBar.transform.SetParent(healthbarTemplate.transform.parent);
        newBar.transform.localScale = Vector3.one;
        newBar.CreateForPlayer(player);
    }
}
