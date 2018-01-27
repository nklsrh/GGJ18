using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDirector : MonoBehaviour
{
    public int shipHealth = 30;
    public int portHealth = 50;

    List<PortController> ports = new List<PortController>();
    public List<PortController> Ports
    {
        get
        {
            return ports;
        }
    }

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
            ports[i].health.Setup(portHealth);
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
        if (port.connectedPorts.Length > 0)
        {
            int randomPortIndex = Random.Range(0, port.connectedPorts.Length);
            PortController newPort = port.connectedPorts[randomPortIndex];
            ship.SetTarget(newPort.transform);
            ship.transform.position = port.transform.position + (newPort.transform.position - port.transform.position) * 0.1f;
        }
        else
        {
            Debug.LogError("Port: " + port.gameObject.name + " has no connected ports!");
        }
    }
}
