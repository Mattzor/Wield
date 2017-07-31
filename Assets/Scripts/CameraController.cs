using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;

    private Vector3 offsetPosition;
    private Space offsetPositionSpace = Space.Self;
    private bool lookAt = true;

    // Use this for initialization
    void Start()
    {        
        offsetPosition = transform.position - player.transform.position;  
    }



    void LateUpdate()
    {
		if (player == null) {
			return;
		}
        if (Input.GetMouseButton(1))
        {
            //transform.position = transform.position - new Vector3(0, t, t);
            //offsetPosition -= Input.GetAxisRaw("Mouse ScrollWheel") * new Vector3(0, 2, 2); 
            offsetPosition -= Input.GetAxis("Mouse Y") * new Vector3(0,1,1);
            Cursor.visible = false;
        }
        else { Cursor.visible = true; }

        if(Input.GetAxisRaw("Mouse ScrollWheel") > 0)
        {
            //offsetPosition += Input.GetAxisRaw("Mouse ScrollWheel") * 5 * new Vector3(0, transform.forward.y, transform.forward.z);
            GetComponent<Camera>().fieldOfView-=2;
        }
        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            //offsetPosition += Input.GetAxisRaw("Mouse ScrollWheel") * 5 * new Vector3(0, transform.forward.y, transform.forward.z);
            GetComponent<Camera>().fieldOfView+=2;
        }
        if (offsetPositionSpace == Space.Self)
        {
            transform.position = player.transform.TransformPoint(offsetPosition);
        }
        else
        {
            transform.position = player.transform.position + offsetPosition;
        }
        
        if (lookAt)
        {
            transform.LookAt(player.transform);
        }
        else
        {
            transform.rotation = player.transform.rotation;
        }

       
    }
}
