using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;

	private RectTransform healthbarTransform;

	private Camera camera;
	private Space offsetPositionSpace = Space.Self;
    
	private Vector3 offsetPosition;
	private Vector3 offsetLookAtHeight;
	private Vector3 pos;

	private float camFOVmin = 24f;
	private float camFOVmax = 70f;
    


    // Use this for initialization
    void Start()
    {        
		camera = GetComponent<Camera> ();
        offsetPosition = transform.position - player.transform.position;  
		offsetLookAtHeight = new Vector3 (0f, 2f, 0);
		healthbarTransform = GetComponentInChildren<RectTransform> ();
		RenderSettings.skybox.color = Color.grey;
    }



    void LateUpdate()
    {
		if (player == null) {
			return;
		}
        if (Input.GetMouseButton(1))
        {            
			if (transform.rotation.x < 90f && transform.rotation.x > -5f) {
				offsetPosition -= Input.GetAxis ("Mouse Y") * new Vector3 (0, 1, 1);
			}
            Cursor.visible = false;
        }
        else { Cursor.visible = true; }

		if(Input.GetAxisRaw("Mouse ScrollWheel") > 0 && camera.fieldOfView > camFOVmin)
        {            
            camera.fieldOfView-=2;
        }
		if (Input.GetAxisRaw("Mouse ScrollWheel") < 0 && camera.fieldOfView < camFOVmax)
        {          
            camera.fieldOfView+=2;
        }
        if (offsetPositionSpace == Space.Self)
        {
            Vector3 targetPos = player.transform.TransformPoint(offsetPosition);
			Vector3 velocity = Vector3.zero;
			transform.position = Vector3.SmoothDamp (transform.position, targetPos, ref velocity , 0.1f);
        }
        else
        {
            transform.position = player.transform.position + offsetPosition;
        }
        
		pos = player.transform.position + offsetLookAtHeight;
		player.transform.position = pos;
		transform.LookAt(player.transform);
		//Vector3 targetDir = player.transform.position - transform.position;
		//transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (targetDir), 0.9f);
		pos = pos - offsetLookAtHeight;
		player.transform.position = pos;
	       
    }

	private void updateHealthbarPos(float x, float y){

		Vector3 pos = healthbarTransform.position;
		pos = pos + new Vector3 (x, y, 0f);
		healthbarTransform.position = pos;

	}
}
