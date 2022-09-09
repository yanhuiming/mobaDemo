using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using QTool.FOV;
using System.Text;
public class FrameController : MonoBehaviour
{
    public static Queue<byte[]> Frames = new Queue<byte[]>();
    public Transform[] Enemys;
    public Transform PlayerTransform;
    public Transform fov;
    Vector3 dir ;
    CharacterController cc;
    public static int FrameLength = 8;
    public static int m_SyncFrameID = 0;

    PhyPlayer pbe1 ;
    PhyPlayer pbe2 ;
    public static FrameController instance;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        Enemys = new Transform[PlayerTransform.childCount];
        for (int i = 0; i < PlayerTransform.childCount; i++)
        {
            Enemys[i] = PlayerTransform.GetChild(i);
        }
        pbe1 = Enemys[0].gameObject.GetComponent<PhyPlayer>();
        pbe2 = Enemys[1].gameObject.GetComponent<PhyPlayer>();

        StartCoroutine(syncFrameUpdate());
    }

    IEnumerator syncFrameUpdate ()
    {
        while (true)
        {
            //Debug.Log("update"+ Frames.Count);
            if (Frames.Count != 0)
            {
                
                if (Frames.Count == 1)
                {
                    byte[] UDPData = Frames.Dequeue();
                    RunFrame(UDPData);
                    yield return new WaitForEndOfFrame();
                    RunFrame(UDPData);
                    yield return new WaitForEndOfFrame();
                    RunFrame(UDPData);
                }
                else if (Frames.Count > 1)
                {
                    for (int i = 0; i < Frames.Count; i++)
                    {
                        byte[] UDPData = Frames.Dequeue();
                        RunFrame(UDPData); RunFrame(UDPData); RunFrame(UDPData);
                    }
                    
                }
            }
            yield return new WaitForEndOfFrame();
        }
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
        //    if (Frames.Count != 0)
        //    {
        //        if (Frames.Count == 1)
        //        {
        //            StartCoroutine(syncFrameUpdate());
        //        }
        //        else if (Frames.Count > 1)
        //        {
        //            for (int i = 0; i < Frames.Count; i++)
        //            {
        //                syncFrameRun(Frames.Dequeue());
        //            }
        //        }
        //    }
        





        if (Frames.Count!=0)
        {
            for (int i = 0; i < Frames.Count; i++)
            {


                
                
                //Debug.Log("处理数据" + UDPData.Length);
                //int dataType = (int)UDPData[0];
                //int syncFramesCount = (int)UDPData[1];

                //int frameID = BitConverter.ToInt32(UDPData, 2);  //此同步帧ID
                


                //多帧合并方案
                //int frameCount = 0;
                //byte[] framesData;
                //for (int s = 0; s < syncFramesCount; s++)//遍历所有同步帧，大多数情况为1帧
                //{
                //    readIndex += 1;
                //    int tempSyncFrameID = BitConverter.ToInt32(UDPData, readIndex);    //读取同步帧ID号   UDPData 2-6 为同步帧ID号
                //    Debug.Log("  tempSyncFrameID=" + tempSyncFrameID+ "  m_SyncFrameID=" + m_SyncFrameID);
                //    if (tempSyncFrameID==m_SyncFrameID)   //如果读到下一帧则执行，否则丢弃。
                //    {
                //        m_SyncFrameID += 1;
                //        readIndex += 4;
                //        frameCount = UDPData[readIndex];
                //        int framesDataLength = frameCount * FrameLength;
                //        framesData = new byte[framesDataLength];
                //        Array.Copy(UDPData,readIndex, framesData,0, framesDataLength);
                //        readIndex += framesDataLength;
                //        RunFrame(framesData, frameCount);
                //    }
                //    else
                //    {
                //        Debug.Log("此同步帧已执行或丢包");
                //    }
                //}
                //end



                //if (dataType == 2 && Main.playerid == 0)//分配玩家ID号
                //{

                //    Main.playerid = (byte)(dataType - 100);
                //    Debug.Log("分配玩家ID号 " + Main.playerid);
                //    PlayerTransform = Enemys[Main.playerid - 1];

                //    fov.parent = PlayerTransform;
                //    fov.localPosition = Vector3.zero;
                //    CameraFollow.instance.player = PlayerTransform;
                //    Main.instance.playerLight.parent = PlayerTransform;
                //    Main.instance.playerLight.localPosition = Vector3.up * 2;
                //}
                //if (dataType > 100)
                //{
                //    dataType -= 100;
                //}
                //byte movex = UDPData[2];
                //byte movey = UDPData[3];
                //int enemyindex = dataType - 1;
                //dir = new Vector3((int)movex / 100f - 1, (int)movey / 100f - 1, 0);
                //var dirr = new Vector3(Enemys[enemyindex].position.x + dir.x, Enemys[enemyindex].position.y, Enemys[enemyindex].position.z + dir.y);//移动方向
                //Enemys[enemyindex].LookAt(dirr);
                //var speed = Enemys[enemyindex].rotation * Vector3.forward * Time.deltaTime * 10;//移动速度
                //cc = Enemys[enemyindex].gameObject.GetComponent<CharacterController>();
                //if (cc != null)
                //{
                //    if (!cc.isGrounded)
                //    {
                //        speed += new Vector3(0, -9.8f * Time.deltaTime, 0);
                //    }
                //    cc.Move(speed);
                //}
            }
            
        }
    }
    
    FixMath.NET.Fix64 fx;
    FixMath.NET.Fix64 fz;


    FixMath.NET.Fix64 fx2;
    FixMath.NET.Fix64 fz2;
    public void RunFrame(byte[] UDPData)
    {
        //Debug.Log(BitConverter.ToString(UDPData));
        int frameCount = UDPData[6];
        //Debug.Log("UDPData.Length=" + UDPData.Length + "  frameCount=" + frameCount + "  FrameLength=" + FrameLength);

        //byte[][] framesData = new byte[frameCount][];
        byte[] frameData = new byte[FrameLength];
        for (int f = 0; f < frameCount; f++)
        {
            Array.Copy(UDPData, f * FrameLength + 7, frameData, 0, FrameLength);
            //framesData[f] = frameData;
            //Debug.Log(BitConverter.ToString(frameData));

            int playerid = frameData[0];
            if (playerid!=5)
            {
                PhyPlayer pbe = Enemys[playerid].gameObject.GetComponent<PhyPlayer>();
                int x = (int)frameData[1];
                int z = (int)frameData[2];
                byte keyDown = frameData[3];
                Debug.Log(keyDown);
                //Debug.Log(pbe + " x =" + x + " y =" + z);
                fx = (((FixMath.NET.Fix64)x / 100) - 1) * 8;
                fz = (((FixMath.NET.Fix64)z / 100) - 1) * 8;
                FixMath.NET.Fix64 y = pbe.phyEntity.linearVelocity.Y;
                //Debug.Log(pbe+" fx =" + fx + " fy =" + fz);
                if (playerid == 0)
                {
                    UDPServer.allFrameCount += 1;
                    fx2 += x-100;
                    fz2 += z-100;
                }
                if (keyDown == 1)
                {
                    y = 20;
                }
                pbe.phyEntity.linearVelocity = new BEPUutilities.Vector3(fx, y, fz);
                
                //pbe.phyEntity.position += new BEPUutilities.Vector3(fx / 100, 0, fz / 100);
                

                pbe.phyEntity.BecomeDynamic(1);
                
            }


            
            //pbe.phyEntity.material = m;
            //pbe.phyEntity.angularVelocity = new BEPUutilities.Vector3(0, 0, 0);


            //pbe1.phyEntity.linearVelocity = new BEPUutilities.Vector3(fx, 0, fz);
            //pbe1.phyEntity.material = m;
            //pbe1.phyEntity.angularVelocity = new BEPUutilities.Vector3(0, 0, 0);
            //pbe1.phyEntity.BecomeDynamic(1);

        }
        BEPUPhyMgr.space.Update();
        //for (int f = 0; f < framesData.Length; f++)
        //{
        //    Debug.Log(BitConverter.ToString(framesData[f]));
        //    int playerid = framesData[f][0];

        //    PhyPlayer pbe = Enemys[playerid].gameObject.GetComponent<PhyPlayer>();
        //    //PhyPlayer pbe0 = Enemys[1].gameObject.GetComponent<PhyPlayer>();
        //    int x = framesData[f][1];
        //    int z = framesData[f][2];
        //    //Debug.Log(x+"  "+z);




        //}
    }

    
    private void OnGUI()
    {


        GUI.Label(new Rect(40, 40, 1000, 200), "Frame:" + m_SyncFrameID
            + "\r\nudp:" + UDPServer.UDPDataLength
            + "\r\nfcount:" + UDPServer.allFrameCount


            + "\r\nplayer1x:" + pbe1.phyEntity.position.X
            + "\r\nplayer1y:" + pbe1.phyEntity.position.Y
            + "\r\nplayer1z:" + pbe1.phyEntity.position.Z
            + "\r\nplayer2x:" + pbe2.phyEntity.position.X
            + "\r\nplayer2y:" + pbe2.phyEntity.position.Y
            + "\r\nplayer2z:" + pbe2.phyEntity.position.Z


            + "\r\nx" + fx
            + "\r\nz" + fz
            + "\r\nfx2 " + fx2
            + "\r\nfz2 " + fz2
        + "\r\nallx " + UDPServer.allx);

    }
}
