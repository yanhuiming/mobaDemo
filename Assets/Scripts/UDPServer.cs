using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
//using System.Diagnostics;
using System.IO;
using System.Collections;
using System.Net.NetworkInformation;
class UDPServer:MonoBehaviour
{
    public static Socket UDPSocket = null;
    public static Thread UDPListeningThread;
    public static Thread FrameSyncThread;
    public static int allFrameCount;
    public static byte[][] match_syncFrames = new byte[65536][];//保存所有帧数据
    public static int keyDown = 0;
    public static void StartUDPServer()
    {
        //Console.WriteLine("StartUDPServer");
        //端口号（用来监听的）
        int port = 55577;
        if (Application.platform == RuntimePlatform.WindowsEditor|| Application.platform == RuntimePlatform.WindowsPlayer)
        {
            //Debug.Log("这是Windows编辑器模式。。。");
            port = AvailablePort();
        }
        if (Application.platform == RuntimePlatform.IPhonePlayer) // 使用Unity切换Platform无法模拟
        {
            //Debug.Log("这是iPhone平台。。。");
        }
        if (Application.platform == RuntimePlatform.Android)// 使用Unity切换Platform无法模拟
        {
            //Debug.Log("这是安卓平台。。。");
        }

        //string ip = "127.0.0.1";
        //将IP地址和端口号绑定到网络节点point上  
        //IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ip), port);
        //
        //定义一个套接字用于监听客户端发来的消息，包含三个参数（IP4寻址协议，流式连接，Tcp协议）  
        UDPSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        UDPSocket.Bind(new IPEndPoint(IPAddress.Any, port));//绑定端口号和IP
        //Console.WriteLine("服务端已经开启");
        UDPListeningThread = new Thread(new ThreadStart(UDPListening_Thread));    //创建监听线程
        UDPListeningThread.Start();

        


        FrameSyncThread = new Thread(new ThreadStart(FrameSyncThread_Thread));    //创建帧同步线程
        FrameSyncThread.Start();
    }
    static void FrameSyncThread_Thread()    //帧同步线程
    {
        while (true)
        {
            byte mx = (byte)((PlayWnd.newdir.x + 1) * 100);
            byte my = (byte)((PlayWnd.newdir.y + 1) * 100);
            //Debug.Log(" send "+mx+" "+my);
            Byte[] data;
            data = new byte[13];
            byte[] m_SyncFrameID = BitConverter.GetBytes(FrameController.m_SyncFrameID);
            data[0] = 1;
            Array.Copy(m_SyncFrameID, 0, data, 1, 4);
            data[5] = Main.playerid;

            data[6] = mx;
            data[7] = my;
            
            
            data[8] = (byte)keyDown;
            //if (mx==100&&my==100)
            //{

            //    data = new byte[6];
            //    byte[] m_SyncFrameID = BitConverter.GetBytes(FrameController.m_SyncFrameID);
            //    data[0] = 1;
            //    Array.Copy(m_SyncFrameID, 0, data, 1, 4);
            //    data[5] = Main.playerid;
            //}
            //else
            //{

            //}

            UDPServer.SendFrame(data);
            keyDown = 0;
            Thread.Sleep(50);//启动定时器，每隔一帧的时间触发一次逻辑帧
        }
    }
    public static int UDPDataLength;
    public static int allx = 0;
    static void UDPListening_Thread()
    {
        while (true)
        {
            try
            {
                //Thread.Sleep(100);
                EndPoint point = new IPEndPoint(IPAddress.Any, 0);//用来保存发送方的ip和端口号
               byte[] UDPData = new byte[1024];
                lock (match_syncFrames)
                {
                    //int SIO_UDP_CONNRESET = -1744830452;
                    //UDPSocket.IOControl((IOControlCode)SIO_UDP_CONNRESET, new byte[] { 0, 0, 0, 0 }, null);

                    UDPDataLength = UDPSocket.ReceiveFrom(UDPData, ref point);//接收数据报

                    //Debug.Log("收到 第" + BitConverter.ToInt32(UDPData, 2) + "帧" + UDPData[8] + " " + UDPData[9] + " " + allx);                                                          //string message = Encoding.UTF8.GetString(buffer, 0, length);
                                                                                                                                                                                      //Console.WriteLine(point.ToString() + message);
                }                                                                                        //Debug.Log("收到长度："+length  + " UDPData[0]=" + UDPData[0]);    //平均47
                if (UDPData[0] == 1)
                {
                    byte[] frame = new byte[UDPDataLength];
                    lock (match_syncFrames)
                    {
                        
                        Array.Copy(UDPData, 0, frame, 0, UDPDataLength);

                       
                    }                                              //Debug.Log("收到 第" + frameID + "帧"  + allx);
                    int frameID = BitConverter.ToInt32(UDPData, 2);  //此同步帧ID
                    if (frameID == FrameController.m_SyncFrameID)
                        {
                        //int frameCount = UDPData[6];
                        //Debug.Log("UDPData.Length=" + UDPData.Length + "  frameCount=" + frameCount);
                        lock (match_syncFrames)
                        {
                            byte[] framesData = new byte[40];
                            Array.Copy(UDPData, 7, framesData, 0, 40);
                            //byte[] frameData = new byte[FrameController.FrameLength];
                            //for (int f = 0; f < frameCount; f++)
                            //{
                            //    Array.Copy(UDPData, f * FrameController.FrameLength + 7, frameData, 0, FrameController.FrameLength);
                            //    framesData[f] = frameData;
                            //}

                            match_syncFrames[frameID] = framesData;//保存当前帧的操作放入所有帧中去，
                        }
                        //Debug.Log("玩家1   第" + UDPServer.allFrameCount + "帧=" + x + " " + z + " = " + allx);
                        lock (match_syncFrames)
                        {
                            allx += (int)UDPData[8] + (int)UDPData[9] - 200;
                            //Debug.Log("验证 第" + frameID + "帧=" + match_syncFrames[frameID][1] + " " + match_syncFrames[frameID][2] + " = " + allx);

                            FrameController.m_SyncFrameID = frameID+1;
                            FrameController.Frames.Enqueue(frame);

                        }




                            //byte mx = (byte)((PlayWnd. newdir.x + 1) * 100);
                            //byte my = (byte)((PlayWnd.newdir. y + 1) * 100);
                            //Byte[] data = new byte[13];
                            //byte[] m_SyncFrameID = BitConverter.GetBytes(FrameController.m_SyncFrameID);
                            //data[0] = 1;
                            //Array.Copy(m_SyncFrameID, 0, data, 1, 4);
                            //data[5] = Main.playerid;

                            //data[6] = mx;
                            //data[7] = my;
                            //UDPServer.SendFrame(data);
                        }
                        else
                        {
                            Debug.Log("丢弃多余的" + "第" + frameID + "帧=" + match_syncFrames[frameID][1] + " " + match_syncFrames[frameID][2] + " = " + allx);
                        }
                    
                }
            }
            catch(Exception e)
            {
                
                Debug.LogError(e.Message);
            }
            
            
        }
    }
    public static void SendFrame(byte[] frameData)
    {
        //byte[] frameData = new Byte[16];
        IPAddress ip = IPAddress.Parse("192.168.0.106");
        //IPAddress ip = IPAddress.Parse("49.233.209.250");

        IPEndPoint endPoint = new IPEndPoint(ip, 55566);
        //Debug.Log(frameData);
        //Debug.Log(endPoint);
        int sendmFrameid = BitConverter.ToInt32(frameData,1);
        //Debug.Log("发送玩家帧"+ sendmFrameid+"");
        UDPSocket.SendTo(frameData, endPoint);
    }
    public static void ClouseSocket()
    {
        if (UDPSocket!=null)
        {
            UDPSocket.Close();
            UDPSocket = null;
        }
        if (UDPListeningThread.IsAlive)
        {
            UDPListeningThread.Abort();
        }
        if (FrameSyncThread.IsAlive)
        {
            FrameSyncThread.Abort();
        }
    }


    /// <summary>        
    /// 获取操作系统已用的端口号     
    /// </summary>        
    /// <returns></returns>       
    public static int AvailablePort()
    {
        //获取本地计算机的网络连接和通信统计数据的信息            
        IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
        //返回本地计算机上的所有Tcp监听程序            
        IPEndPoint[] ipsTCP = ipGlobalProperties.GetActiveTcpListeners();
        //返回本地计算机上的所有UDP监听程序            
        IPEndPoint[] ipsUDP = ipGlobalProperties.GetActiveUdpListeners();
        //返回本地计算机上的Internet协议版本4(IPV4 传输控制协议(TCP)连接的信息。            
        TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();
        List<int> allPorts = new List<int>();
        foreach (IPEndPoint ep in ipsTCP)
        {
            allPorts.Add((int)ep.Port);

        }
        foreach (IPEndPoint ep in ipsUDP)
        {
            allPorts.Add(ep.Port);
        }
        foreach (TcpConnectionInformation conn in tcpConnInfoArray)
        {
            allPorts.Add(conn.LocalEndPoint.Port);
        }
        int testPort = 2048;

        //for (int i = 0; i < allPorts.Count; i++)
        //{
        //    Console.WriteLine("usedPort=" + allPorts[i].ToString());

        //}
        for (int i = testPort; i < 65535; i++)
        {
            Console.WriteLine("ipport=" + testPort.ToString());
            if (!allPorts.Contains(testPort))
            {
                break;
            }
            else
            {
                testPort += 1;
            }
        }
        return testPort;
    }
}

