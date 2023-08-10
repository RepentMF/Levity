using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTrigger : MonoBehaviour
{
    public void ChangeGravity(int ID)
    {
        if (FindObjectOfType<PhysicsManager>().gravitySwitch)
        {
            FindObjectOfType<PhysicsManager>().gravitySwitch = false;
        Debug.Log(FindObjectOfType<PhysicsManager>().gravitySwitch + " " + ID);
        }
        else
        {
            FindObjectOfType<PhysicsManager>().gravitySwitch = true;
        Debug.Log(FindObjectOfType<PhysicsManager>().gravitySwitch + " " + ID);
        }
        FindObjectOfType<PlayerController>().gameObject.GetComponent<EdgeCollider2D>().offset *= -1;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
