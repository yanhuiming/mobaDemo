using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //[SerializeField]
    public Transform player;

    float cameraX;
    float cameraY;
    float cameraZ;

    public float y = 10;
    public float z = 5;
    public static CameraFollow instance;
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
        if (player)
        {
            cameraX = player.position.x;
            cameraY = player.position.y+15;
            cameraZ = player.position.z;
            transform.position = new Vector3(cameraX, cameraY, cameraZ + z);
        }
    }
}
