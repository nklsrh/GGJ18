using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDirector : MonoBehaviour
{
    List<PortController> ports = new List<PortController>();

    List<AIShipController> aiShips = new List<AIShipController>();

    public void Setup()
    {
        PortController[] portList = FindObjectsOfType<PortController>();

        ports.AddRange(portList);


        AIShipController[] shipList = FindObjectsOfType<AIShipController>();

        aiShips.AddRange(shipList);


        for (int i = 0; i < aiShips.Count; i++)
        {
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
        //ship.transform.position = port.shipDockTransform.position;
        //port.transform.rotation = port.shipDockTransform.rotation;
        Debug.Log("SET UP SHIP : " + ship.gameObject.name + " WITH PORT: " + port.gameObject.name);
        int randomPortIndex = Random.Range(0, port.connectedPorts.Length);
        Debug.Log("GET RANDOM PORT: " + randomPortIndex);
        ship.SetTarget(port.connectedPorts[randomPortIndex].transform);

        ship.transform.position = port.transform.position + (port.connectedPorts[randomPortIndex].transform.position - port.transform.position) * 0.1f;
    }
}
