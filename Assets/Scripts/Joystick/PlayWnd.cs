

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Text;
public class PlayWnd : WindowRoot
{
    public Transform testObjTrans;
    public float speedMultipler;

    public Image imgTouch;
    public Image imgDirBg;
    public Image imgDirPoint;
    public Transform ArrowRoot;
    //private PlayerAttack playerattack;
    private bool sendEnd = true;
    float pointDis = 135;
    Vector2 startPos = Vector2.zero;
    Vector2 defaultPos = Vector2.zero;
    private CharacterController cc;
    int m_sendFrameID = 1;
    
    void Start()
    {
        pointDis = Screen.height * 1.0f / ClientConfig.ScreenStandardHeight * ClientConfig.ScreenOPDis;
        SetActive(ArrowRoot, false);
        defaultPos = imgDirBg.transform.position;
        if (testObjTrans)
        {
            //playerattack = testObjTrans.GetComponent<PlayerAttack>();
            cc = testObjTrans.GetComponent<CharacterController>();
        }
        RegisterMoveEvts();


        


    }

    void RegisterMoveEvts()
    {
        SetActive(ArrowRoot, false);

        OnClickDown(imgTouch.gameObject, (PointerEventData evt, object[] args) =>
        {
            startPos = evt.position;
            //Debug.Log($"evt.pos:{evt.position}");
            imgDirPoint.color = new Color(1, 1, 1, 1f);
            imgDirBg.transform.position = evt.position;
        });
        OnClickUp(imgTouch.gameObject, (PointerEventData evt, object[] args) =>
        {
            imgDirBg.transform.position = defaultPos;
            imgDirPoint.color = new Color(1, 1, 1, 0.5f);
            imgDirPoint.transform.localPosition = Vector2.zero;
            SetActive(ArrowRoot, false);

            InputMoveKey(Vector2.zero);
        });
        OnDrag(imgTouch.gameObject, (PointerEventData evt, object[] args) =>
        {
            Vector2 dir = evt.position - startPos;
            float len = dir.magnitude;
            if (len > pointDis)
            {
                Vector2 clampDir = Vector2.ClampMagnitude(dir, pointDis);
                imgDirPoint.transform.position = startPos + clampDir;
            }
            else
            {
                imgDirPoint.transform.position = evt.position;
            }

            if (dir != Vector2.zero)
            {
                SetActive(ArrowRoot);
                float angle = Vector2.SignedAngle(new Vector2(1, 0), dir);
                ArrowRoot.localEulerAngles = new Vector3(0, 0, angle);
            }

            InputMoveKey(dir.normalized);
        });
    }

    void InputMoveKey(Vector2 dir)
    {
        //Debug.Log($"Input Dir:{dir}");
        newdir = dir;
    }


    public static Vector3 newdir;//摇杆输出位置
    void FixedUpdate()
    {
        //if (playerattack.HP && playerattack.HP.hp && playerattack.HP.hp.hpvalue == 0)
        //{
        //    SetActive(ArrowRoot, false);
        //    return;
        //}


        



        //if (dir != Vector3.zero && testObjTrans)
        //{
            //Debug.Log(dir);
            
            
            


            //sendEnd = false;
            //var dirr = new Vector3(testObjTrans.position.x + dir.x, testObjTrans.position.y, testObjTrans.position.z + dir.y);//移动方向
            //testObjTrans.LookAt(dirr);
            //var speed = testObjTrans.rotation * Vector3.forward * Time.deltaTime * speedMultipler;//移动速度
            //if (cc != null)
            //{
            //    if (!cc.isGrounded)
            //    {
            //        speed += new Vector3(0, -9.8f * Time.deltaTime, 0);
            //    }
            //    cc.Move(speed);
            //}



            //移动玩家的位置（按朝向位置移动）
            //testObjTrans.Translate(speed);

            //播放奔跑动画
            //if (playerattack)
            //{
            //    //print("播放奔跑");
            //    playerattack.Run();
            //}
        //}
        //else
        //{
            //if (playerattack && !sendEnd)
            //{
            //    print("播放静止");
            //    playerattack.ResetIdle();
            //    playerattack.StopFly();
            //    sendEnd = true;
            //}
       // }
    }
}
