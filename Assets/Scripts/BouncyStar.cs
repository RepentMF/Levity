using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyStar : MonoBehaviour
{
    public bool gravityChanger;
    public float bounceValue;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.collider.GetType() == typeof(EdgeCollider2D))
        {
            float tempSpeed;
            if (FindObjectOfType<PhysicsManager>().gravitySwitch)
            {
                tempSpeed = -bounceValue;
            }
            else
            {
                tempSpeed = bounceValue;
            }

            Rigidbody2D body = collision.gameObject.GetComponent<Rigidbody2D>();
            body.velocity = new Vector2 (body.velocity.x, tempSpeed);
        }

        if (collision.gameObject.tag == "Player" && collision.collider.GetType() == typeof(EdgeCollider2D) &&
            gravityChanger)
        {
            GetComponentInChildren<GravityTrigger>().ChangeGravity(1);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponentInChildren<GravityTrigger>())
        {
            gravityChanger = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
