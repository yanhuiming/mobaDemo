using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvasMgr : MonoBehaviour
{
    public static MainCanvasMgr instance;
    public Transform playerSelectPanel;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void JumpOnClick()
    {
        UDPServer.keyDown = 1;
    }
}
