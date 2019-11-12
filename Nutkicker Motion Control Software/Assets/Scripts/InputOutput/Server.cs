using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum ServerStatus
    {
        offline,
        starting,
        listening,
        connected,
        reading,
        shutting_down
    }

public class Server : MonoBehaviour
{
    public static bool ServerON = false;
    [SerializeField] private TMP_InputField IP_Input;
    [SerializeField] private string IP;
    [SerializeField] private int Port = 31090;

    public static ServerStatus Status = ServerStatus.offline;
    
    private void FixedUpdate()
    {
        IP = IP_Input.text;
    }

    public void StartServer()
    {
        if (!ServerON)
        {
            ServerON = true;
            Thread t = new Thread(RunServerRun);
            t.Start();
        }
        else
        {
            Debug.Log("Server already running");
        }

    }
    public void StopServer()
    {
        if (ServerON)
        {
            ServerON = false;
        }
        else
        {
            Debug.Log("Server is not running");
        }

    }
    void RunServerRun()
    {
        //IP = Utility.GetLocalIPAddress();
        //IP = "127.0.0.1";

        TcpListener listener = new TcpListener(IPAddress.Parse(IP), Port);

        try
        {
            listener.Start();
            Debug.Log("Server started.");
            Status = ServerStatus.starting;
            Thread.Sleep(400);

            while (ServerON)
            {
                Status = ServerStatus.listening;
                Debug.Log("Waiting for client connection...");
                while (!listener.Pending() && ServerON)
                {
                    Thread.Sleep(100);
                }

                if (!ServerON) break;

                TcpClient client = listener.AcceptTcpClient();
                Debug.Log("Client connected :-)");
                Status = ServerStatus.connected;

                StreamReader reader = new StreamReader(client.GetStream());
                StreamWriter writer = new StreamWriter(client.GetStream());

                string s = string.Empty;

                while (ServerON)
                {
                    s = reader.ReadLine();
                    Status = ServerStatus.reading;

                    if (s == "exit")
                    {
                        break;
                    }
                    //todo: make sure that a new datastring is added (Updated), even if there already is one, because the other side will only peek().
                    Airlock.Container.TryAdd(s);
                }
                reader.Close();
                writer.Close();
                client.Close();
            }

            listener.Stop();
            Debug.Log("Shutting down server :-(");
            Status = ServerStatus.shutting_down;
            Thread.Sleep(500);

            Status = ServerStatus.offline;

        }
        catch (Exception ex)
        {
            Debug.Log("Exception recieved:" + ex);
            listener.Stop();
            Debug.Log("Shutting down server :-(");
            Status = ServerStatus.shutting_down;
            Thread.Sleep(500);
        }
    }
}
