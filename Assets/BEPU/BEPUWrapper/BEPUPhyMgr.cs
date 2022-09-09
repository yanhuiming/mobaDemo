using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEPUPhyMgr : MonoBehaviour
{
    public static BEPUPhyMgr instance;
    public static BEPUphysics.Space space;
    private void Awake()
    {
        instance = this;
        space = new BEPUphysics.Space();
        space.ForceUpdater.gravity = new BEPUutilities.Vector3(0, -99.81m, 0);//重力
        space.TimeStepSettings.TimeStepDuration = 1 / 60m;//物理迭代时间间隔
        
        Physics.autoSimulation = false;  //关闭默认物理模拟
        Physics.autoSyncTransforms = false; //关闭射线检测;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //space.Update();
    }
}
