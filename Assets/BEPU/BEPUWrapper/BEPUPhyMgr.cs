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
        space.ForceUpdater.gravity = new BEPUutilities.Vector3(0, -99.81m, 0);//����
        space.TimeStepSettings.TimeStepDuration = 1 / 60m;//�������ʱ����
        
        Physics.autoSimulation = false;  //�ر�Ĭ������ģ��
        Physics.autoSyncTransforms = false; //�ر����߼��;
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
