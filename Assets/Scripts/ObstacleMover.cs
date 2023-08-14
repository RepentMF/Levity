using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public bool hasTimer;
    public bool playerAlert;
    public string direction;
    public float initTimer;
    public float currentTimer;

    public Vector2 obstacleSpeed;
    public Vector2 initSpeed;
    public Vector3 initPosition;
    public Rigidbody2D body;

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerAlert = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerAlert = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        body = this.gameObject.GetComponentInParent<Rigidbody2D>();

        if (initTimer > 0)
        {
            hasTimer = true;
            currentTimer = initTimer;
        }

        
        initSpeed = obstacleSpeed;
        initPosition = this.gameObject.transform.parent.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAlert)
        {
            if (hasTimer)
            {
                if (currentTimer >= 0f)
                {
                    currentTimer -= Time.deltaTime;
                }
                else
                {
                    currentTimer = initTimer;
                    obstacleSpeed *= -1;
                }
            }
            
            switch (direction)
            {
                case "x":
                    body.gameObject.transform.position += new Vector3(obstacleSpeed.x * Time.deltaTime * 150, 0, 0);
                    break;
                case "y":
                    body.gameObject.transform.position += new Vector3(0, obstacleSpeed.y * Time.deltaTime * 150, 0);
                    break;
                case "both":
                    body.gameObject.transform.position += new Vector3(obstacleSpeed.x * Time.deltaTime * 150, obstacleSpeed.y * Time.deltaTime * 150, 0);
                    break;
            }
        }
        else
        {
            this.gameObject.transform.parent.position = initPosition;
            this.gameObject.transform.position = initPosition;
            obstacleSpeed = initSpeed;
            currentTimer = initTimer;
        }
    }
}
