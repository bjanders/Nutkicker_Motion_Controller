using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

public class Utility
{
    public static float DEG_from_RAD(float R)
    {
        return Convert.ToSingle(R / (2 * Math.PI) * 360);
    }
    public static float RAD_from_DEG(float D)
    {
        return Convert.ToSingle( (D / 360.0f) * 2 * Math.PI);
    }
    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        return "No network adapter found";
    }
    public static float InvertHeading(float hdg)
    {
        if (hdg < 180) return hdg + 180;
        else return hdg - 180;
    }
}



