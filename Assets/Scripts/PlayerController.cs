using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool onGround;
    public bool isTeleporting;
    public bool isDead;
    public bool respawnGravity;
    public int respawnPoint;
    public int activeTeleporter;
    public float gravityAcceleration;
    public float horiSpeed;
    public float vertiSpeed;

    public Rigidbody2D rigidbody2D;
    public EdgeCollider2D edgecollider2D;

    public void OnJump()
    {
        if (onGround)
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
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hazard")
        {
            isDead = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle" && collision.otherCollider.GetType() == typeof(EdgeCollider2D))
        {
            onGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            onGround = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Respawn")
        {
            respawnPoint = collider.gameObject.GetComponent<RespawnPoint>().respawnID;
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
                
                isTeleporting = false;
                isDead = false;
            }
        }
    }

    private void Animate()
    {
        if (rigidbody2D.velocity.x == 0)
        {

        }
        else if (Mathf.Sign(rigidbody2D.velocity.x) == 1)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (Mathf.Sign(rigidbody2D.velocity.x) == -1)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (FindObjectOfType<PhysicsManager>().gravitySwitch)
        {
            gameObject.GetComponent<SpriteRenderer>().flipY = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipY = false;
        }
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
    void Update()
    {
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

        if (isDead)
        {
            Respawn();
        }

        Animate();
    }
}