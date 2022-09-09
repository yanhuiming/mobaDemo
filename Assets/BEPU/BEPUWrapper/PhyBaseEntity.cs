using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhyBaseEntity : MonoBehaviour
{
    public BEPUphysics.Entities.Entity phyEntity = null;
    protected Vector3 center = Vector3.zero;
    [SerializeField]
    protected float mass = 1;
    protected bool isTrigger = false;
    protected PhysicMaterial phyMat = null;
    [SerializeField]
    protected bool isStatic = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddSelfToPhyWorld()
    {
        if (this.phyEntity == null) { 
            return;
        }
        BEPUPhyMgr.space.Add(this.phyEntity);
    }
    public void SyncPhyTransformWithUnityTransform()     // ��U3D���ݸ�ֵ��BEPU
    {
        if (this.phyEntity == null){ 
            return; 
        }

        // λ��        
        Vector3 unityPos = this.transform.position;
        unityPos += this.center;
        this.phyEntity.position = ConversionHelper.MathConverter.Convert(unityPos);
        // end
        // ��ת        
        Quaternion rot = this.transform.rotation;
        this.phyEntity.orientation = ConversionHelper.MathConverter.Convert(rot);
        // end   
    }
    public void SyncUnityTransformWithPhyTransform()      // ��BEPU���ݸ�ֵ��U3D
    {
        if (this.phyEntity == null)
        { 
            return; 
        }
        // λ��        
        BEPUutilities.Vector3 pos = this.phyEntity.position;        
        Vector3 unityPosition = ConversionHelper.MathConverter.Convert(pos);        
        unityPosition -= this.center;        
        this.transform.position = unityPosition;        
        // end
        // ��ת        
        BEPUutilities.Quaternion rot = this.phyEntity.orientation;        
        Quaternion r = ConversionHelper.MathConverter.Convert(rot);        
        this.transform.rotation = r;        
        // end    
    }
        // ͬ������entity��λ�õ�transform;    
    void LateUpdate() {        
        if (this.phyEntity == null || this.isStatic)
        {            
            return;       
        }
        this.SyncUnityTransformWithPhyTransform();
    }
}
