using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhyPlayer : MonoBehaviour
{
    public BEPUphysics.Entities.Entity phyEntity = null;
    public int px;
    public int py;
    public int pz;
    public FixMath.NET.Fix64 pos;
    // Start is called before the first frame update

    void Start()
    {
        this.phyEntity = new BEPUphysics.Entities.Prefabs.Sphere
            (BEPUutilities.Vector3.Zero, (FixMath.NET.Fix64)1 / (int)2, (FixMath.NET.Fix64)1);
        this.phyEntity.position = new BEPUutilities.Vector3((FixMath.NET.Fix64)px/10, (FixMath.NET.Fix64)py / 10, (FixMath.NET.Fix64)pz / 10);
        BEPUphysics.Materials.Material m = new BEPUphysics.Materials.Material();
        m.kineticFriction = 1000;
        m.staticFriction = 1000;
        
        this.phyEntity.material = m;
        BEPUPhyMgr.space.Add(this.phyEntity);

    }

    // Update is called once per frame
    void Update()
    {
        BEPUutilities.Vector3 pos = this.phyEntity.position;
        Vector3 unityPosition = ConversionHelper.MathConverter.Convert(pos);
        transform.position = unityPosition;
    }
}
