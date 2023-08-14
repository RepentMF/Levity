using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GravityHarness : MonoBehaviour
{
    public float initTimer;
    public float currentTimer;

    private AudioSource audio;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerController>().harnessNumber < 6)
            {
                collision.gameObject.GetComponent<PlayerController>().harnessNumber++;

                if (collision.gameObject.GetComponent<PlayerController>().harnessNumber > 1)
                {
                    collision.gameObject.GetComponent<PlayerController>().currentHarness = 
                        collision.gameObject.GetComponent<PlayerController>().harnessNumber - 1;
                }

                //audio.Play(0);
                collision.gameObject.GetComponent<PlayerController>().audio.clip = audio.clip;
                collision.gameObject.GetComponent<PlayerController>().audio.Play(0);
                Destroy(this.gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audio = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTimer >= 5)
        {
            currentTimer -= Time.deltaTime;
            this.gameObject.GetComponent<Light2D>().falloffIntensity += .06f * Time.deltaTime;
        }
        else if (currentTimer >= 0)
        {
            currentTimer -= Time.deltaTime;
            this.gameObject.GetComponent<Light2D>().falloffIntensity -= .06f * Time.deltaTime;
        }
        else if (currentTimer < 0)
        {
            currentTimer = initTimer;
        }
    }
}
