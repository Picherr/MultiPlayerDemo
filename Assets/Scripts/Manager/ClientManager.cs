using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System;
using UnityEngine;
using SocketGameProtocol;
using System.Threading;
using System.Net;
using System.Net.NetworkInformation;

public enum ADDRESSFAM
{
    IPv4,
    IPv6
}

public class ClientManager : BaseManager
{
    private Socket socket;
    private Message message;
    private Thread aucThread;
    public static string nativeip = GetIP(ADDRESSFAM.IPv4);
    private string ip = "119.91.211.118";

    public ClientManager(GameFace face) : base(face) { }

    public override void OnInit()
    {
        base.OnInit();
        message = new Message();
        InitSocket();

        InitUDP();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        message = null;
        CloseSocket();

        if (aucThread != null)
        {
            aucThread.Abort();
            aucThread = null;
        }
    }

    public static string GetIP(ADDRESSFAM Addfam)
    {
        if (Addfam == ADDRESSFAM.IPv6 && !Socket.OSSupportsIPv6)
        {
            return null;
        }

        string output = "";

        foreach(NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
        {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            NetworkInterfaceType _type1 = NetworkInterfaceType.Wireless80211;
            NetworkInterfaceType _type2 = NetworkInterfaceType.Ethernet;

            if((item.NetworkInterfaceType==_type1||item.NetworkInterfaceType==_type2)&&item.OperationalStatus==OperationalStatus.Up)
#endif
            {
                foreach(UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                {
                    // IPv4
                    if (Addfam == ADDRESSFAM.IPv4)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            output = ip.Address.ToString();
                            Debug.Log("IP:" + output);
                        }
                    }

                    // IPv6
                    else if (Addfam == ADDRESSFAM.IPv6)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetworkV6)
                        {
                            output = ip.Address.ToString();
                        }
                    }
                }
            }
        }
        return output;
    }

    /// <summary>
    /// 初始化socket
    /// </summary>
    private void InitSocket()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            socket.Connect(ip, 6666);
            // 连接成功
            StartReceive();
            face.ShowMessage("连接成功！");
            Debug.Log("TCP连接成功！");
        }
        catch (Exception e)
        {
            // 连接出错
            Debug.LogWarning(e);
            face.ShowMessage("连接失败！");
        }
    }

    /// <summary>
    /// 关闭socket
    /// </summary>
    private void CloseSocket()
    {
        if (socket.Connected && socket != null)
        {
            socket.Close();
        }
    }

    private void StartReceive()
    {
        socket.BeginReceive(message.Buffer, message.StartIndex, message.Remsize, SocketFlags.None, ReceiveCallback, null);
    }

    private void ReceiveCallback(IAsyncResult iar)
    {
        try
        {
            if (socket == null || socket.Connected == false)
            {
                return;
            }
            int len = socket.EndReceive(iar);
            if(len == 0)
            {
                Debug.Log("数据为0");
                CloseSocket();
                return;
            }

            message.ReadBuffer(len, HandleResponse);
            StartReceive();
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
        }
    }

    private void HandleResponse(MainPack pack)
    {
        face.HandleResponse(pack);
    }

    public void Send(MainPack pack)
    {
        if (socket.Connected == false || socket == null)
        {
            return;
        }
        socket.Send(Message.PackData(pack));
    }


    // UDP协议
    private Socket udpClient;
    private IPEndPoint ipEndPoint;
    private EndPoint EPoint;
    private Byte[] buffer = new Byte[1024];

    private void InitUDP()
    {
        udpClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), 6667);
        EPoint = ipEndPoint;
        try
        {
            udpClient.Connect(EPoint);
            Debug.Log("UDP连接成功！");
        }
        catch (Exception e)
        {
            Debug.Log("UDP连接失败！" + e.ToString());
            return;
        }

        Loom.RunAsync(() =>
        {
            aucThread = new Thread(ReceiveMsg);
            aucThread.Start();
        });
    }

    private void ReceiveMsg()
    {
        Debug.Log("UDP开始接收");
        while (true)
        {
            int len = udpClient.ReceiveFrom(buffer, ref EPoint);
            MainPack pack = (MainPack)MainPack.Descriptor.Parser.ParseFrom(buffer, 0, len);
            // Debug.Log("接收数据：" + pack.Actioncode.ToString() + pack.User);
            Loom.QueueOnMainThread((param) =>
            {
                HandleResponse(pack);
            }, null);
        }
    }

    public void SendTo(MainPack pack)
    {
        Byte[] sendBuff = Message.PackDataUDP(pack);
        udpClient.Send(sendBuff, sendBuff.Length, SocketFlags.None);
    }
}
