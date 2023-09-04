using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;
using Unity.VisualScripting;

public class SignalSender : MonoBehaviour
{
    public string pc2IpAddress = ""; 
    public int pc2Port = 7001; 

    private OSCTransmitter transmitter;

    private void Start()
    {
        transmitter = gameObject.AddComponent<OSCTransmitter>();
        transmitter.RemoteHost = pc2IpAddress;
        transmitter.RemotePort = pc2Port;
    }

    private void TriggerEventOnPC2()
    {
        var message = new OSCMessage("/event/trigger");
        message.AddValue(OSCValue.String("EventTriggered"));
        transmitter.Send(message);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TriggerEventOnPC2();
        }
    }
}
