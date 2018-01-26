using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPort : UITrackedObject
{
    PortController myPort;

    internal void CreateForPort(PortController port)
    {

        Track(port.transform);
    }
}
