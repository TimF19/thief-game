using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;
    public AudioSource runSound;
    public AudioSource jumpSound;
    public AudioSource hurtSound;
    public AudioSource deadSound;
    public AudioSource carHitSound;
    public AudioSource healthSound;
    public AudioSource shieldSound;
    public Image wastedImage;
    public float runSpeed = 40f;
    private float horizontalMove = 0f;
    private float restartDelay = 3f;
    private float restartTimer = 0f;
    private bool jump;
    private bool shield;
    private bool dead = false;
    private Vector3 distance;

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }
    
    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed ;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (transform.position.x > 600)
        {
            shield = false;
        }
        
        if (transform.position.x >= 850)
        {
            transform.Translate(10 * Time.deltaTime * Vector2.right.normalized);
        }
        
        if (runSpeed < 0)
        {
            animator.SetTrigger("Dead");
            dead = true;
            deadSound.Play();
        }

        if (dead)
        {
            wastedImage.enabled = true;
            restartTimer += Time.deltaTime;

            // .. if it reaches the restart delay...
            if(restartTimer >= restartDelay)
            {
                // .. then reload the currently loaded level.
                Application.LoadLevel(Application.loadedLevel);
            }
        }
        else
        {
            wastedImage.enabled = false;
        }
        
        if (transform.position.y < -0.75 && !runSound.isPlaying && horizontalMove != 0)
        {
            runSound.Play();
        }

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
            runSound.Stop();
            jumpSound.Play();
        }
        
        if (Input.GetButtonDown("Attack"))
        {
            animator.SetTrigger("Knife");
        }
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Health"))
        {
            runSpeed += 10;
            Destroy(other.gameObject);
            healthSound.Play();
        }
        
        if (other.CompareTag("Shield"))
        {
            shield = true;
            Destroy(other.gameObject);
            shieldSound.Play();
        }

        if (!shield)
        {
            if (other.CompareTag("Police"))
            {
                runSpeed -= 1;
                animator.SetTrigger("Hurt");
                hurtSound.Play();
            }

            if (other.CompareTag("Obstacle"))
            {
                runSpeed -= 2;
                animator.SetTrigger("Hurt");
                carHitSound.Play();
            }
        }

        if (other.CompareTag("Chief"))
        { 
            animator.SetTrigger("Dead");
            dead = true;
            deadSound.Play();
        }
    }
}
