using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MustangMovement : MonoBehaviour
{
    public Transform Character;
    public Animator animator;
    public AudioSource carSound;
    public Image winImage;
    private float restartDelay = 5f;
    private float restartTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Character.transform.position.x >= 859)
        {
            transform.Translate(10 * Time.deltaTime * Vector2.right.normalized);
            animator.SetBool("IsRolling", true);
            if (!carSound.isPlaying)
            {
                carSound.Play();
                winImage.enabled = true;
                restartTimer += Time.deltaTime;

                // .. if it reaches the restart delay...
                if(restartTimer >= restartDelay)
                {
                    // .. then reload the currently loaded level.
                    Application.LoadLevel(Application.loadedLevel);
                }
            }
        }
        else
        {
            winImage.enabled = false;
        }
    }
}
