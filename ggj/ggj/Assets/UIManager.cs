using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public UIHealthbar healthbarTemplate;
    public UIPort portTemplate;
    public UITrackedObject shipTemplate;

    public void CreatePlayer(RPlayerController player)
    {
        UIHealthbar newBar = InstantiateTransform(healthbarTemplate.transform, healthbarTemplate.transform.parent).GetComponent<UIHealthbar>();
        newBar.Setup(player.health);
    }

    public void CreatePort(PortController port)
    {
        UIPort newPort = InstantiateTransform(portTemplate.transform, portTemplate.transform.parent).GetComponent<UIPort>();
        newPort.CreateForPort(port);
    }

    public void CreateShip(Transform ship)
    {
        UITrackedObject newPort = InstantiateTransform(shipTemplate.transform, shipTemplate.transform.parent).GetComponent<UITrackedObject>();
        newPort.Track(ship);
    }

    Transform InstantiateTransform (Transform prefab, Transform parent)
    {
        prefab.gameObject.SetActive(true);
        Transform newItem = Instantiate(prefab);
        newItem.SetParent(parent);
        newItem.localScale = prefab.localScale;
        newItem.localRotation = prefab.localRotation;
        prefab.gameObject.SetActive(false);
        return newItem;
    }
}
