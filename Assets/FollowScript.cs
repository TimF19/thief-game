using System;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
    public Transform Character; // Target Object to follow
    private Vector3 direction;
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;
    private float horizontalMove = 0f;
    private bool jump;

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }
    
    // Update is called once per frame
    void Update()
    {
        horizontalMove = runSpeed;
        direction = Character.transform.position - transform.position;
        if (direction.x > 0)
        {
            horizontalMove = runSpeed ;
        }
        else
        {
            if (direction.x > -9)
            {
                horizontalMove = 0 - runSpeed;
            }
            else
            {
                horizontalMove = 0;
            }
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }
    
    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            controller.m_JumpForce = 400;
            jump = true;
            animator.SetBool("IsJumping", true);
        }
        if (other.CompareTag("HighObstacle"))
        {
            controller.m_JumpForce = 500;
            jump = true;
            animator.SetBool("IsJumping", true);
        }
        
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("Attack");
        }
    }
}

    