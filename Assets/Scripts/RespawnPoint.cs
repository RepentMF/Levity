using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RespawnPoint : MonoBehaviour
{
    public bool exploded;
    public bool respawnGravity;
    public int respawnID;

    public AudioSource audio;

    private void Explode()
    {
        // play muffled explosion sound effect 
        audio.Play();
        // explode the level *smile*
        foreach (Transform obj in transform.parent)
        {
            System.Random rd = new System.Random();
            int random1 = rd.Next(-7, 7);
            int random2 = rd.Next(-7, 7);

            if (obj.gameObject.GetComponent<Rigidbody2D>())
            {
                if (obj.gameObject.GetComponent<Rigidbody2D>().bodyType != RigidbodyType2D.Dynamic)
                {
                    obj.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                }

                obj.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
                obj.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(random1 * Time.deltaTime, random2 * Time.deltaTime, 0f), ForceMode2D.Impulse);
                exploded = true;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!exploded)
        {
            audio.volume = 0f;
            audio.pitch = 0f;
            if (FindObjectOfType<PlayerController>().respawnPoint == 20)
            {
                Explode();
            }
            else if (FindObjectOfType<PlayerController>().respawnPoint > respawnID + 1 && 
                    FindObjectOfType<PlayerController>().respawnPoint >= 16)
            {
                Explode();
            }
            else if (FindObjectOfType<PlayerController>().respawnPoint > respawnID + 3)
            {
                Explode();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!exploded)
        {
            if (FindObjectOfType<PlayerController>().respawnPoint == 20)
            {
                audio.volume = .35f;
                audio.pitch = .35f;
                Explode();
            }
            else if (FindObjectOfType<PlayerController>().respawnPoint > respawnID + 1 && 
                    FindObjectOfType<PlayerController>().respawnPoint >= 16)
            {
                audio.volume = .3f;
                audio.pitch = .2f;
                Explode();
            }
            else if (FindObjectOfType<PlayerController>().respawnPoint > respawnID + 3)
            {
                audio.volume = .15f;
                audio.pitch = .1f;
                Explode();
            }
        }
    }
}
