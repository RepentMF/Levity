using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public bool gravityChanger;
    public bool isLevelDoor;
    public int ID;
    public int destinationID;

    // when a trigger enters this teleporter's collision box
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // if this teleporter is colliding with the player
        if (collider.gameObject.tag == "Player")
        {
            // if the player is not already teleporting
            // and if the player's active teleporter ID is not this teleporter's ID
            if (!collider.gameObject.GetComponent<PlayerController>().isTeleporting &&
                collider.gameObject.GetComponent<PlayerController>().activeTeleporter != ID)
            {
                // for every teleporter found in the scene
                // (IN GODOT, REWRITE SOLUTION TO NOT USE FOREACH, INSTEAD COMPARE DIRECTLY
                // AND FIND PARTNER TELEPORTER'S POSITION)
                foreach (Teleporter tele in FindObjectsOfType<Teleporter>())
                {
                    // if the teleporter in question's ID is the same as this teleporter's partner ID
                    if (tele.ID == destinationID)
                    {
                        // if this teleporter is a gravity changing teleporter
                        if (gravityChanger)
                        {
                            // change the player's gravity
                            GetComponentInChildren<GravityTrigger>().ChangeGravity(ID);
                        }
                        
                        // set the player's position to the teleporter in question's position, 
                        // active teleporter variable to this teleporter's partner ID, and the player
                        // to be teleporting
                        collider.gameObject.transform.position = tele.gameObject.transform.position;
                        collider.gameObject.GetComponent<PlayerController>().activeTeleporter = destinationID;
                        collider.gameObject.GetComponent<PlayerController>().isTeleporting = true;

                        // if this teleporter is a partner with a level entrance or exit
                        if (isLevelDoor)
                        {
                            // set the player's current number of harnesses to their total amount of 
                            // harnesses minus 1
                            collider.gameObject.GetComponent<PlayerController>().currentHarness =
                                collider.gameObject.GetComponent<PlayerController>().harnessNumber - 1;
                        }
                    }
                }
            }
        }
    }

    // when a trigger exits this teleporter's collision box
    private void OnTriggerExit2D(Collider2D collider)
    {
        // if this teleporter was colliding with the player
        if (collider.gameObject.tag == "Player")
        {
            // if the player's active teleporter ID is this teleporter's ID
            if (collider.gameObject.GetComponent<PlayerController>().activeTeleporter == ID)
            {
                collider.gameObject.GetComponent<PlayerController>().isTeleporting = false;
                collider.gameObject.GetComponent<PlayerController>().activeTeleporter = 0;
            }
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
