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
            
            FindObjectOfType<PlayerController>().audio.clip = GameObject.FindWithTag("GravityDown").GetComponent<AudioSource>().clip;
            FindObjectOfType<PlayerController>().audio.Play(0);
        }
        else
        {
            FindObjectOfType<PhysicsManager>().gravitySwitch = true;
            
            FindObjectOfType<PlayerController>().audio.clip = GameObject.FindWithTag("GravityUp").GetComponent<AudioSource>().clip;
            FindObjectOfType<PlayerController>().audio.Play(0);
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
