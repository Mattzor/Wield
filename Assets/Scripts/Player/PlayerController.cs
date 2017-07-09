﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float turnSpeed = 10f;
    public float walkSpeed = 2f;
    public float runSpeed = 10f;

    public bool isAlive = true;
    public bool isAttacking = false;
    public bool running = true;


    Animation anim;
    Rigidbody playerRigidbody;
    bool forward = true;
    bool runningMode = true;

    bool moving;
    float moveSpeed;

    // All sounds for the player and the index for the specific sounds
    AudioSource[] sounds;
    private int swordSFX = 0;
    private int stepsRunning = 1;



    // Use this for initialization
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animation>();
        sounds = GetComponents<AudioSource>();
        moveSpeed = runSpeed;
      
        anim["strafeLeft"].speed = 1.7f;
        anim["strafeRight"].speed = 1.7f;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (isAlive)
        {
            Move();
            Attack();
            PlaySounds();
        }
        else
        {
            Die();
        }
        //Animating(h, v);
    }

    void Move()
    {
        Vector3 movement;
        Vector3 position = playerRigidbody.position;
        moving = false;
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            ChangeMoveSpeed();
        }
        /* Move forward */
        if (Input.GetKey(KeyCode.W) && !isAttacking)
        {
            if (running)
            {
                movement = transform.forward * moveSpeed * Time.deltaTime;
                playerRigidbody.MovePosition(position + movement);
                anim.CrossFade("run");
            }
            else
            {
                movement = transform.forward * moveSpeed * Time.deltaTime;
                playerRigidbody.MovePosition(position + movement);
                if (!forward)
                {
                    ChangeWalkAnimationDirection(true);
                    forward = true;
                }
                anim.CrossFade("walk");
            }
            moving = true;

        }
        /* Move backwards */
        else if (Input.GetKey(KeyCode.S) && !isAttacking)
        {
            movement = -transform.forward * walkSpeed * Time.deltaTime;
            playerRigidbody.MovePosition(position + movement);
            if (forward)
            {
                ChangeWalkAnimationDirection(false);
                forward = false;
            }
            anim.CrossFade("walk");
            moving = true;
        }
        /* Strafe left */
        else if (Input.GetKey(KeyCode.A) && Input.GetMouseButton(1) && !isAttacking)
        {
            movement = -transform.right * moveSpeed * Time.deltaTime;
            playerRigidbody.MovePosition(position + movement);
            anim.Play("strafeLeft");
            moving = true;

        }
        /* Strafe right */
        else if (Input.GetKey(KeyCode.D) && Input.GetMouseButton(1) && !isAttacking)
        {
            movement = transform.right * moveSpeed * Time.deltaTime;
            playerRigidbody.MovePosition(position + movement);
            anim.Play("strafeRight");
            moving = true;

        }
        else
        {
            moving = false;
        }
        /* Turning left/right */
        float x = 0;

        if (Input.GetAxis("Horizontal") != 0)
        {
            x = Input.GetAxis("Horizontal") * 2;
        }
        if (Input.GetMouseButton(1))
        {
            x = Input.GetAxis("Mouse X") * turnSpeed * 1;
        }
        transform.Rotate(0, x, 0);
        if (!moving)
        {
            anim.CrossFade("idle");
        }
        else
        {
            isAttacking = false;
        }
    }

    void Attack()
    {
        if (Input.GetKey(KeyCode.E))
        {
            //anim.Play("swordStrike2");
            anim["swordStrike2"].speed = 2f;
            anim.CrossFade("swordStrike2");
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
    }

    public void PlaySounds()
    {
        if (isAttacking)
        {
            if (!sounds[swordSFX].isPlaying)
            {
                sounds[swordSFX].PlayDelayed(0.175f);
            }
        }
        else
        {
            sounds[swordSFX].Stop();
        }
        if (moving)
        {
            if (!sounds[stepsRunning].isPlaying)
            {
                sounds[stepsRunning].PlayDelayed(0.025f);
            }
        }
        else
        {
            sounds[stepsRunning].Stop();
        }
    }

    public void Die()
    {
        isAlive = false;
        anim.Play("die2");        
        Destroy(gameObject, 3);
    }

    void Animate()
    {

    }


    // Update is called once per frame
    void Update()
    {

    }

    void ChangeMoveSpeed()
    {
        if (!running)
        {
            moveSpeed = runSpeed;
            anim["strafeLeft"].speed = 1.7f;
            anim["strafeRight"].speed = 1.7f;
            running = true;
        }
        else
        {
            moveSpeed = walkSpeed;
            anim["strafeLeft"].speed = 1f;
            anim["strafeRight"].speed = 1f;
            running = false;
        }
    }

    void ChangeWalkAnimationDirection(bool forward)
    {
        if (forward)
        {
            anim["walk"].speed = 1;
            anim["walk"].time = 0;
        }
        else
        {
            anim["walk"].speed = -1;
            anim["walk"].time = anim["walk"].length;
        }
    }
     

}