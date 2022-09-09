using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePhyEntity : MonoBehaviour
{
    // Start is called before the first frame update
    PhyBoxEntity pbe;
    void Start()
    {
        pbe = gameObject.GetComponent<PhyBoxEntity>();
        //StartCoroutine(move());
    }

    // Update is called once per frame
    void Update()
    {
        float x = 0;
        float y = 0;
        float z = 0;
        if (Input.GetKey(KeyCode.W))
        {
            z = 3;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            z = -3;
        }
        else
        {
            z = (float)pbe.phyEntity.linearVelocity.Z;
        }
        if (Input.GetKey(KeyCode.A))
        {
            x = -3;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            x = 3;
        }
        else
        {
            x = (float)pbe.phyEntity.angularVelocity.Y; 
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            y = 7;
        }
        else
        {
            y = (float)pbe.phyEntity.linearVelocity.Y;
        }
        if (x==0&y==0&z==0)
        {
            pbe.phyEntity.BecomeDynamic(1);
        }
        else
        {
            pbe.phyEntity.linearVelocity = ConversionHelper.MathConverter.Convert(new Vector3(x, y, z));
            pbe.phyEntity.angularVelocity = ConversionHelper.MathConverter.Convert(
                new Vector3((float)pbe.phyEntity.angularVelocity.X, x, (float)pbe.phyEntity.angularVelocity.Z));
            //pbe.phyEntity.BecomeKinematic();
            pbe.phyEntity.BecomeDynamic(1);
            //pbe.phyEntity.LinearVelocity.Normalize();
            
        }
        
        

    }
    IEnumerator move()
    {
        yield return new WaitForSeconds(0.1f);
        if (pbe)
        {
            for (int i = 0; i < 1000; i++)
            {
                yield return new WaitForEndOfFrame();
                //transform.position += new Vector3(0, 0, 0.1f);
                //pbe.SyncPhyTransformWithUnityTransform();
                Debug.Log("Ìí¼ÓÁ¦");
                
            }



        }
    }
}
