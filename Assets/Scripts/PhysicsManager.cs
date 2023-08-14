using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    public bool gravitySwitch;
    public float gravityAcceleration;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obst in GameObject.FindGameObjectsWithTag("Obstacle"))
        {
            obst.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Obstacle";
        }

        foreach (Teleporter tele in FindObjectsOfType<Teleporter>())
        {
            tele.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Movement Obstacle";
        }
        
        foreach (BouncyStar boun in FindObjectsOfType<BouncyStar>())
        {
            boun.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Movement Obstacle";
        }

        foreach (GravityTrigger grav in FindObjectsOfType<GravityTrigger>())
        {
            grav.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Gravity Trigger";
        }

        foreach (GravityHarness harn in FindObjectsOfType<GravityHarness>())
        {
            harn.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
        }

        foreach (GameObject haza in GameObject.FindGameObjectsWithTag("Hazard"))
        {
            haza.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Hazard";
        }
    }

    // Update is called once per frame
    void Update()
    {
        // probably move somewhere else, like a master controller
        if (gravitySwitch)
        {
            Physics2D.gravity = new Vector2 (0f, gravityAcceleration);
        }
        else
        {
            Physics2D.gravity = new Vector2 (0f, -gravityAcceleration);
        }
    }
}
