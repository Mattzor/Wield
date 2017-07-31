using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterController : MonoBehaviour {

	public Transform playerTransform;

    public float walkSpeed;
    public float runSpeed;

    Rigidbody monsterRigidbody;
    Animator anim;
	BoxCollider leftHandCollider;
	BoxCollider rightHandCollider;


    float time;
    int i;
    bool areaFree = true;

    float[] rotationAngles = {0f,90f,180f,270f};




	// Use this for initialization
	void Start () {
        monsterRigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        anim.SetBool("isIdle", true);
        time = Time.time;
        i = Random.Range(2, 5);
		leftHandCollider = Helper.FindInChildren (gameObject, "MonsterLArmPalm").GetComponent<BoxCollider>();
		rightHandCollider = Helper.FindInChildren (gameObject, "MonsterRArmPalm").GetComponent<BoxCollider> ();
		//leftHandCollider = transform.Find ("MonsterLArmPalm").GetComponent<BoxCollider>();
		//rightHandCollider = transform.Find ("MonsterRArmPalm").GetComponent<BoxCollider>();

    }
	
	// Update is called once per frame
	void FixedUpdate () {
		if(anim.GetBool("isAlive")){
			if(playerTransform != null){
				if (anim.GetBool ("playerInRange")) {
					Attack ();
				} else {
					ChasePlayer ();
				}
			}else{
				anim.SetBool ("aggro", false);
				anim.SetBool ("playerInRange", false);
		        if (areaFree)
		        {
		            Move();
		        }
		        else
		        {
		            TurnAround();
		        }
			}
		}
	}

    private void Move()
    {
       
        if (anim.GetBool("isIdle") && Time.time - time > i)
        {
            int rotationIndex = Random.Range(0, 3);
            transform.Rotate(new Vector3(0, rotationAngles[rotationIndex], 0));
            anim.SetBool("isWalking", true);
            anim.SetBool("isIdle", false);
            time = Time.time;
            i = Random.Range(2, 5);
        }
        else if (anim.GetBool("isWalking"))
        {
            if (Time.time - time > i)
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isIdle", true);
                time = Time.time;
                int i = Random.Range(2, 5);
            }
            else
            {
                Vector3 position = monsterRigidbody.position;
                Vector3 movement = transform.forward * walkSpeed * Time.deltaTime;
                monsterRigidbody.MovePosition(position + movement);
            }
        }
    }

	void ChasePlayer()
	{
		Vector3 playerPosition = playerTransform.position;
		transform.LookAt(playerTransform);
		Vector3 position = monsterRigidbody.position;
		Vector3 movement = transform.forward * runSpeed * Time.deltaTime;
		monsterRigidbody.MovePosition(position + movement);
	}

    private void Attack()
    {
		
    }

    private void TurnAround()
    {
        transform.Rotate(new Vector3(0, 90f, 0));
        areaFree = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Building" || other.tag == "Wall")
        {
			areaFree = false;
        } else if (other.tag == "Player")
        {
            anim.SetBool("playerInRange", true);
			leftHandCollider.enabled = true;
			rightHandCollider.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            anim.SetBool("playerInRange", false);
			leftHandCollider.enabled = false;
			rightHandCollider.enabled = false;
        }
    }



	public void PlayerGone(){
		anim.SetBool ("playerInRange", false);
		anim.SetBool ("aggro", false);
		anim.SetBool ("isIdle", true);
	}



}
