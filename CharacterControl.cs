using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] float xMovement, yMovement;

    Rigidbody2D playerRB;
    public SpriteRenderer submarine;
    public ParticleSystem leftBubble, rightBubble;
    public bool docked;
    public bool canMove;
    ParticleSystem.EmissionModule emissionModuleLeft, emissionModuleRight;

    void Start()
    {
        docked = true;
        canMove = false;
        playerRB = GetComponent<Rigidbody2D>();
        Physics.gravity = Physics.gravity * 0.8f;
        emissionModuleLeft = leftBubble.emission;
        emissionModuleRight = rightBubble.emission;

        emissionModuleLeft.enabled = false;
        emissionModuleRight.enabled = false;
    }

    void Update()
    {
        if (canMove && transform.position.y > -2)
        {
            playerRB.velocity = new Vector3(xMovement, -1f) * speed;
        }
        else if (canMove)
            MoveCharacter();
        else if (!docked )
            playerRB.velocity = new Vector3(xMovement, -0.5f) * speed;
        else
            playerRB.velocity = new Vector3(0,0);
        
        BubbleEmission();
    }


    public void MoveCharacter()
    {
        xMovement = Input.GetAxis("Horizontal");
        yMovement = Input.GetAxis("Vertical");

        if (yMovement == 0 && transform.position.y < 0)
            yMovement = -0.2f;

        if (xMovement != 0)
        {
            if (xMovement < 0)
            {
                submarine.flipX = false;

            }
            else if (xMovement > 0)
            {
                submarine.flipX = true;
            }
        }
        
        playerRB.velocity = new Vector3(xMovement, yMovement, 0) * speed;
    }

    void BubbleEmission()
    {
        if (xMovement != 0)
        {
            if (xMovement < 0)
            {
                emissionModuleRight.enabled = true;
                emissionModuleLeft.enabled = false;

            }
            else if (xMovement > 0)
            {
                emissionModuleRight.enabled = false;
                emissionModuleLeft.enabled = true;
            }
        }
        
        else if (xMovement != 0 || docked)
        {
            emissionModuleLeft.enabled = false;
            emissionModuleRight.enabled = false;
        }
    }
}
