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
