using System;
using UnityEngine;
using UnityEngine.Serialization;

public class HelicopterMovement : MonoBehaviour
{
    public Transform Character; // Target Object to follow
    private Vector3 direction;
    public AudioSource sound;
    public CharacterController2D controller;
    public float speed = 40f;
    private float horizontalMove = 0f;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = speed;
        direction = Character.transform.position - transform.position;
        if (direction.x > 0)
        {
            horizontalMove = speed ;
        }
        else
        {
            horizontalMove = 0;
        }

        if (direction.x < 10 && direction.x > -10)
        {
            if (!sound.isPlaying)
            {
                Debug.Log("Helico");
                sound.Play();
            }
        }
        else
        {
            sound.Stop();
        }
    }
    
    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, false);
    }
}