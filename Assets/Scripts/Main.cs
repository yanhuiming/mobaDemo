using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;
public class Main : MonoBehaviour
{
    Transform playerTransform;
    public static byte playerid = 0;
    // Start is called before the first frame update
    public static int sendcount = 3;
    public static int sendokcount = 1;
    public Transform playerLight;
    //public Transform 
    public static Main instance;
    int touchTime;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Application.targetFrameRate = 60;
        //StartCoroutine(GETPlayerID());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UDPServer. keyDown = 1;
        }

    }

   
    public void setplayerid(int i)
    {
        playerid = (byte)i;
        playerTransform = FrameController.instance.Enemys[playerid];
        CameraFollow.instance.player = playerTransform;
        playerLight.parent = playerTransform;
        playerLight.localPosition = Vector3.up * 2;
        MainCanvasMgr.instance.playerSelectPanel.gameObject.SetActive(false);
        UDPServer.StartUDPServer();
    }
    //IEnumerator GETPlayerID()
    //{
    //    for (int i = 0; i < 10; i++)
    //    {
            
    //        if (Main.playerid !=0)
    //        {
    //            break;
    //        }
    //        if (UDPServer.UDPSocket != null)
    //        {
    //            Byte[] data = new byte[16];
    //            UDPServer.SendFrame(data);
    //            //Debug.Log("·¢ËÍÒ»´Î");
    //        }
    //        yield return new WaitForSeconds(1);
    //    }
        
    //}
    private void OnDestroy()
    {
        UDPServer.ClouseSocket();
    }
}
