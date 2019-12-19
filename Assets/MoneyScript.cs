using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyScript : MonoBehaviour
{
    public AudioSource moneySound;
    public int value;
    public Renderer rend;  // renderer variable
    public PolygonCollider2D poly;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MoneyText.money += value;
            moneySound.Play();
            rend = GetComponent<SpriteRenderer>(); // gets sprite renderer

            rend.enabled = false; // sets to false if hit.

            poly = GetComponent<PolygonCollider2D>();

            poly.enabled = false;
            Debug.Log("Money");
        }
    }
}
