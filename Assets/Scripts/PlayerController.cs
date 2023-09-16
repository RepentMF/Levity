using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using System;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public bool blinking;
    public bool canJump;
    public bool playerFocus;
    public bool hasWalljumped;
    public bool onWall;
    public bool onGround;
    //public bool isTeleporting;
    public bool isDead;
    public bool respawnGravity;
    public int harnessNumber;
    public int currentHarness;
    public int respawnPoint;
    //public int activeTeleporter;
    public float afterimageTimer;
    public float initAfterimageTimer;
    public float initTimer;
    public float blinkingTimer;
    public float gravityAcceleration;
    public float horiSpeed;
    public float vertiSpeed;

    public Rigidbody2D rigidbody2D;
    public EdgeCollider2D edgecollider2D;
    public Light2D playerLight;
    public Sprite look;
    public Sprite blink;
    public Afterimage afterimage;
    public AudioSource audio;
    public GameObject winningText;
    public GameObject losingText;
    public GameObject almostText;
    public GameObject startingText;
    public Camera main;

    public void OnJump()
    {
        if (onGround && canJump)
        {
            float tempSpeed;

            if (FindObjectOfType<PhysicsManager>().gravitySwitch)
            {
                tempSpeed = -vertiSpeed;
            }
            else
            {
                tempSpeed = vertiSpeed;
            }

            rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, tempSpeed);
            audio.clip = GameObject.FindWithTag("Jump").GetComponent<AudioSource>().clip;
            audio.Play(0);
        }
        else if (onWall && !hasWalljumped)
        {
            float tempSpeed;

            if (FindObjectOfType<PhysicsManager>().gravitySwitch)
            {
                tempSpeed = -vertiSpeed;
            }
            else
            {
                tempSpeed = vertiSpeed;
            }

            hasWalljumped = true;
            rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, tempSpeed);
            audio.clip = GameObject.FindWithTag("Jump").GetComponent<AudioSource>().clip;
            audio.Play(0);
        }
    }

    public void OnActivateHarness()
    {
        if (harnessNumber != 4 && respawnPoint != 20)
        {
            if (harnessNumber > 1 && currentHarness > 0)
            {
                FindObjectOfType<GravityTrigger>().ChangeGravity(2);
                currentHarness--;
            }
        }
        
    }
    
    public void OnChangeCamera()
    {
        if (playerFocus)
        {
            playerFocus = false;
        }
        else
        {
            playerFocus = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hazard" && harnessNumber != 4)
        {
            isDead = true;
            
            audio.clip = GameObject.FindWithTag("Electrocute").GetComponent<AudioSource>().clip;
            audio.Play(0);
        }
        
        if (collision.gameObject.GetComponent<BouncyStar>() != null && collision.gameObject.GetComponentInChildren<GravityTrigger>() == null)
        {
            audio.clip = GameObject.FindWithTag("Bounce").GetComponent<AudioSource>().clip;
            audio.Play(0);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle" && collision.otherCollider.GetType() == typeof(EdgeCollider2D)
            && harnessNumber > 0)
        {
            onGround = true;
            hasWalljumped = false;
        }

        if (collision.gameObject.tag == "Wall")
        {
            onWall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            onGround = false;
        }

        if (collision.gameObject.tag == "Wall")
        {
            onWall = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Respawn" && respawnPoint != 20)
        {
            respawnPoint = collider.gameObject.GetComponent<RespawnPoint>().respawnID;
        }

        if (collider.gameObject.GetComponent<Teleporter>() != null && collider.gameObject.GetComponentInChildren<GravityTrigger>() == null)
        {
            audio.clip = GameObject.FindWithTag("Teleport").GetComponent<AudioSource>().clip;
            audio.Play(0);
        }
    }

    private void Respawn()
    {
        foreach (RespawnPoint respawn in FindObjectsOfType<RespawnPoint>())
        {
            if (respawn.respawnID == respawnPoint)
            {
                this.gameObject.transform.position = respawn.gameObject.transform.position;
                rigidbody2D.velocity = new Vector2 (0f, 0f);
                this.respawnGravity = respawn.gameObject.GetComponent<RespawnPoint>().respawnGravity;

                if (this.respawnGravity != FindObjectOfType<PhysicsManager>().gravitySwitch)
                {
                    FindObjectOfType<GravityTrigger>().ChangeGravity(2);
                }
                
                if (harnessNumber > 1)
                {
                    currentHarness = harnessNumber - 1;
                }

                isTeleporting = false;
                isDead = false;
            }
        }
    }

    private void AnimateBlinking()
    {
        if (rigidbody2D.velocity.x == 0 && onGround)
        {
            System.Random rd = new System.Random();
            int random1 = rd.Next(0, 1000);

            if (random1 > 995 && !blinking)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = blink;
                blinking = true;
            }
            else if (random1 <= 995 && !blinking)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = look;
            }
            else if (blinking)
            {
                blinkingTimer -= Time.deltaTime;

                if (blinkingTimer <= 0f)
                {
                    blinkingTimer = initTimer;
                    blinking = false;
                }
            }
        }
        else if (rigidbody2D.velocity.x != 0 || !onGround)
        {
            blinkingTimer = initTimer;
            blinking = false;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = look;
        }
    }

    private void AnimateDirection()
    {
        if (rigidbody2D.velocity.x == 0)
        {
           
        }
        else if (Mathf.Sign(rigidbody2D.velocity.x) == 1)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (Mathf.Sign(rigidbody2D.velocity.x) == -1)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (FindObjectOfType<PhysicsManager>().gravitySwitch)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipY = true;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipY = false;
        }
    }

    private void AnimateGlowColor()
    {
        if ((harnessNumber == 0) || (harnessNumber == 1 && !onGround) ||
            (currentHarness == 0 && !onGround))
        {
            playerLight.intensity = 0f;
            playerLight.falloffIntensity = 1f;
        }
        else if ((harnessNumber == 1 && onGround) || (currentHarness == 0))
        {
            playerLight.color = Color.magenta;
            playerLight.intensity = 30f;
            playerLight.falloffIntensity = .75f;
        }
        else if (currentHarness == 1)
        {
            playerLight.color = Color.green;
            playerLight.intensity = 40f;
            playerLight.falloffIntensity = .60f;
        }
        else if (currentHarness >= 2 && harnessNumber != 4)
        {
            playerLight.color = Color.blue;
            playerLight.intensity = 50f;
            playerLight.falloffIntensity = .45f;
        }
        else if (harnessNumber == 4)
        {
            playerLight.color = Color.white;
            playerLight.intensity = 70f;
            playerLight.falloffIntensity = .35f;
        }
    }

    private void AnimateAfterimages()
    {
        if (harnessNumber >= 1 && (rigidbody2D.velocity.x != 0 || rigidbody2D.velocity.y != 0))
        {
            afterimageTimer -= Time.deltaTime;

            if (afterimageTimer <= 0f)
            {
                Instantiate(afterimage, this.gameObject.transform.position, this.gameObject.transform.rotation);
                afterimageTimer = initAfterimageTimer;
            }
        }
        else if (rigidbody2D.velocity == Vector2.zero)
        {
            afterimageTimer = initAfterimageTimer;
        }
    }

    private void Animate()
    {
        AnimateBlinking();
        AnimateDirection();
        AnimateGlowColor();
        AnimateAfterimages();
    }

    // Start is called before the first frame update
    void Start()
    {
        horiSpeed = 4.5f;
        vertiSpeed = 6f;
        gravityAcceleration = 9.8f;

        Respawn();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // can only walk
        if (harnessNumber == 0)
        {
            canJump = false;
            hasWalljumped = true;

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                rigidbody2D.velocity = new Vector2 (-horiSpeed, rigidbody2D.velocity.y);
                startingText.gameObject.SetActive(false);
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                rigidbody2D.velocity = new Vector2 (horiSpeed, rigidbody2D.velocity.y);
                startingText.gameObject.SetActive(false);
            }
            else
            {
                rigidbody2D.velocity = new Vector2 (0f, rigidbody2D.velocity.y);
            }
        }
        // can fly
        else if (harnessNumber == 4)
        {
            canJump = false;
            hasWalljumped = true;
            rigidbody2D.gravityScale = 0f;

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                rigidbody2D.velocity = new Vector2 (-horiSpeed, rigidbody2D.velocity.y);
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                rigidbody2D.velocity = new Vector2 (horiSpeed, rigidbody2D.velocity.y);
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, horiSpeed);
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, -horiSpeed);
            }
            
            if (rigidbody2D.velocity.x > 0f && rigidbody2D.velocity.y > 0f)
            {
                rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x - Time.deltaTime, rigidbody2D.velocity.y - Time.deltaTime);
            }
            else if (rigidbody2D.velocity.x > 0f && rigidbody2D.velocity.y < 0f)
            {
                rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x - Time.deltaTime, rigidbody2D.velocity.y + Time.deltaTime);
            }
            else if (rigidbody2D.velocity.x < 0f && rigidbody2D.velocity.y > 0f)
            {
                rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x + Time.deltaTime, rigidbody2D.velocity.y - Time.deltaTime);
            }
            else if (rigidbody2D.velocity.x < 0f && rigidbody2D.velocity.y < 0f)
            {
                rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x + Time.deltaTime, rigidbody2D.velocity.y + Time.deltaTime);
            }
            else if (Mathf.Round(rigidbody2D.velocity.x) == 0f && Mathf.Round(rigidbody2D.velocity.y) == 0f)
            {
                rigidbody2D.velocity = Vector2.zero;
            }
        }
        // can walk, gravity trigger, and jump
        else if (harnessNumber < 4)
        {
            if (!hasWalljumped)
            {
                canJump = true;
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                rigidbody2D.velocity = new Vector2 (-horiSpeed, rigidbody2D.velocity.y);
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                rigidbody2D.velocity = new Vector2 (horiSpeed, rigidbody2D.velocity.y);
            }
            else
            {
                rigidbody2D.velocity = new Vector2 (0f, rigidbody2D.velocity.y);
            }
        }

        if (harnessNumber < 4 && respawnPoint == 20)
        {
            canJump = false;
            rigidbody2D.gravityScale = 0.0001f;
            losingText.gameObject.SetActive(true);
        }
        else if (harnessNumber == 4 && respawnPoint == 20)
        {
            almostText.gameObject.SetActive(true);
            respawnPoint = 20;
        }
        
        if (harnessNumber == 5 && respawnPoint == 20)
        {
            losingText.gameObject.SetActive(false);
            almostText.gameObject.SetActive(false);
            winningText.gameObject.SetActive(true);
        }

        if (isDead)
        {
            Respawn();
        }

        // redirect camera focus
        if (playerFocus)
        {
            main.transform.parent = this.gameObject.transform;
            main.transform.position = new Vector3 (this.gameObject.transform.position.x, this.gameObject.transform.position.y, -1);
            main.transform.rotation = Quaternion.identity;
            main.orthographicSize = 5.6f;

            foreach (Transform child in main.gameObject.transform)
            {
                if (child.gameObject.name == "Space")
                {
                    child.gameObject.GetComponent<SpaceParallax>().enabled = true;
                    child.gameObject.transform.localScale = new Vector3(2.5f,2.5f,1f);
                }
            }
        }
        else
        {
            

            foreach (GameObject level in GameObject.FindGameObjectsWithTag("Level"))
            {
                if (respawnPoint == 20)
                {
                    playerFocus = true;
                    break;
                }
                else if (level.name.Contains("Level " + (respawnPoint) + " Folder"))
                {
                    foreach (Transform child in level.gameObject.transform)
                    {
                        if (respawnPoint == 17)
                        {
                            if (child.gameObject.name == "Floor 1")
                            {
                                main.transform.parent = child.gameObject.transform;
                                main.transform.position = new Vector3 (child.transform.GetChild(2).gameObject.transform.position.x, 
                                    child.transform.GetChild(2).gameObject.transform.position.y - 12f, -1);
                                main.orthographicSize = 20f;
                            }
                        }
                        else
                        {
                            if (child.gameObject.name == "Ceiling")
                            {
                                main.transform.parent = child.gameObject.transform;
                                main.transform.position = new Vector3 (child.transform.GetChild(0).gameObject.transform.position.x, 
                                    child.transform.GetChild(0).gameObject.transform.position.y, -1);
                                main.orthographicSize = 9f; 
                            }
                        }
                    }
                }
            }

            foreach (Transform child in main.gameObject.transform)
            {
                if (respawnPoint == 20)
                {
                    playerFocus = true;
                    break;
                }
                else if (child.gameObject.name == "Space")
                {
                    if (respawnPoint == 17)
                    {
                        child.gameObject.GetComponent<SpaceParallax>().enabled = false;
                        child.gameObject.transform.localPosition = new Vector3(0f, 0f, 1f);
                        child.gameObject.transform.localScale = new Vector3(7f, 6f, 1.48440003f);
                    }
                    else
                    {
                        child.gameObject.GetComponent<SpaceParallax>().enabled = false;
                        child.gameObject.transform.localPosition = new Vector3(0f, 0f, 1f);
                        child.gameObject.transform.localScale = new Vector3(2.7f, 2.35f, 1.48440003f);
                    }
                    
                }
            }
        }

        Animate();
    }
}