using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PhyBoxEntity : PhyBaseEntity 
{
    //float centerx;
    //float centery;
    //float centerz;

    //float width;
    //float height;
    //float length;
    //public float mass;
    //public bool isStatic;
    //BEPUphysics.Entities.Prefabs.Box phyEntity;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        //BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();
        //centerx = boxCollider.center.x * transform.localScale.x;
        //centery = boxCollider.center.y * transform.localScale.y;
        //centerz = boxCollider.center.z * transform.localScale.z;
        //width = boxCollider.size.x * transform.localScale.x+centerx;
        //height = boxCollider.size.y * transform.localScale.y + centerx;
        //length = boxCollider.size.z * transform.localScale.z + centerx;


        //BEPUphysics.EntityStateManagement.MotionState ms = new BEPUphysics.EntityStateManagement.MotionState();
        //ms.Position = ConversionHelper.MathConverter.Convert(transform.position);
        //ms.Orientation = ConversionHelper.MathConverter.Convert(transform.rotation);

        //if (isStatic)
        //{
        //    phyEntity = new BEPUphysics.Entities.Prefabs.Box(
        //     ms,
        //    (FixMath.NET.Fix64)this.width,
        //    (FixMath.NET.Fix64)height,
        //    (FixMath.NET.Fix64)length);
        //}
        //else
        //{
        //    phyEntity = new BEPUphysics.Entities.Prefabs.Box(
        //     ms,
        //    (FixMath.NET.Fix64)this.width,
        //    (FixMath.NET.Fix64)height,
        //    (FixMath.NET.Fix64)length,
        //    (FixMath.NET.Fix64)mass);
        //}
        //BEPUPhyMgr.instance.space.Add(phyEntity);


        BoxCollider box = this.gameObject.GetComponent<BoxCollider>();
        this.center = new Vector3( 
            box.center.x * transform.localScale.x,
            box.center.y * transform.localScale.y,
            box.center.z * transform.localScale.z



            );

        float width = box.size.x * transform.localScale.x ; 
        float height = box.size.y * transform.localScale.y; 
        float length = box.size.z * transform.localScale.z; 
        
        this.phyMat = box.material; 
        this.isTrigger = box.isTrigger;

        if (this.isStatic) {
            this.phyEntity = new BEPUphysics.Entities.Prefabs.Box(BEPUutilities.Vector3.Zero, 
                (FixMath.NET.Fix64)width, (FixMath.NET.Fix64)height, (FixMath.NET.Fix64)length); 
        } else { 
            this.phyEntity = new BEPUphysics.Entities.Prefabs.Box(BEPUutilities.Vector3.Zero, 
                (FixMath.NET.Fix64)width, (FixMath.NET.Fix64)height, (FixMath.NET.Fix64)length, (FixMath.NET.Fix64)this.mass); 
        }
        this.AddSelfToPhyWorld(); 
        this.SyncPhyTransformWithUnityTransform();

    }
    
    
    //private void LateUpdate()
    //{
    //    if (isStatic)
    //    {
    //        return;
    //    }
    //    else
    //    {
    //        BEPUutilities.Vector3 pos = this.phyEntity.position;
    //        Vector3 unityPosition = ConversionHelper.MathConverter.Convert(pos);
    //        transform.position = unityPosition;
    //    }
    //}
    // Update is called once per frame
    void Update()
    {
        
    }
}
