using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhyPlane : MonoBehaviour
{
    public BEPUphysics.Entities.Entity phyEntity = null;
    public int px;
    public int py;
    public int pz;
    // Start is called before the first frame update
    void Start()
    {
        this.phyEntity = new BEPUphysics.Entities.Prefabs.Box(BEPUutilities.Vector3.Zero,
                (FixMath.NET.Fix64)100, (FixMath.NET.Fix64)2, (FixMath.NET.Fix64)100);
        this.phyEntity.position = new BEPUutilities.Vector3((FixMath.NET.Fix64)px / 10, (FixMath.NET.Fix64)py / 10, (FixMath.NET.Fix64)pz / 10);

        BEPUPhyMgr.space.Add(this.phyEntity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
