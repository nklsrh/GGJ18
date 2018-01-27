using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDirector : MonoBehaviour
{
    public int shipHealth = 30;

    List<PortController> ports = new List<PortController>();

    List<AIShipController> aiShips = new List<AIShipController>();
    public List<AIShipController> AiShips
    {
        get
        {
            return aiShips;
        }
    }

    public void Setup()
    {
        PortController[] portList = FindObjectsOfType<PortController>();

        ports.AddRange(portList);
        for (int i = 0; i < ports.Count; i++)
        {
            ports[i].Setup();
        }

        AIShipController[] shipList = FindObjectsOfType<AIShipController>();

        aiShips.AddRange(shipList);

        for (int i = 0; i < aiShips.Count; i++)
        {
            aiShips[i].Setup(shipHealth);
            SetupShipRouteRandom(aiShips[i], ports[i]);
            aiShips[i].onRouteComplete += OnRouteComplete;
        }
    }

    private void OnRouteComplete(AIShipController ship)
    {
        SetupShipRouteRandom(ship, ports[Random.Range(0, ports.Count)]);
    }

    void SetupShipRouteRandom(AIShipController ship, PortController port)
    {
        int randomPortIndex = Random.Range(0, port.connectedPorts.Length);
        ship.SetTarget(port.connectedPorts[randomPortIndex].transform);
        ship.transform.position = port.transform.position + (port.connectedPorts[randomPortIndex].transform.position - port.transform.position) * 0.1f;
    }
}
