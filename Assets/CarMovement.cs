using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public Transform Character;
    public float speed = 5f;
    private Vector3 direction;
    public AudioSource carSound;

    // Update is called once per frame
    void Update()
    {
        direction = Character.transform.position - transform.position;
        if (direction.x > -20 && direction.x < 20)
        {
            float move = speed * Time.deltaTime;
            transform.Translate(move * Vector2.left.normalized);
            carSound.Play();
        }
    }
}
