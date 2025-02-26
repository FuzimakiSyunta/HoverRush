using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenScript : MonoBehaviour
{
    
    public RectTransform Door_Up;
    public RectTransform Door_Down;
    private float MoveSpeed = 0.5f;
    private bool isClose = false;
    private float DoorPosY = 924.0f;
    private float DoorPosX = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
        Door_Up.position = new Vector3(DoorPosX, DoorPosY, 0);
        Door_Down.position = new Vector3(DoorPosX, -DoorPosY, 0);
        isClose = false;
    }

    // Update is called once per frame
    void Update()
    {
        
       if (Door_Down.position.y == 360)
       {
           isClose = true;
       }

       if (isClose == false)
       {
           Door_Up.position -= new Vector3(DoorPosX, MoveSpeed, 0);
           Door_Down.position += new Vector3(DoorPosX, MoveSpeed, 0);
       }
       else
       {
           Door_Up.position += new Vector3(DoorPosX, MoveSpeed, 0);
           Door_Down.position -= new Vector3(DoorPosX, MoveSpeed, 0);
       }
        
        
    }

    
}
