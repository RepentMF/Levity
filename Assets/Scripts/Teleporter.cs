using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public bool gravityChanger;
    public bool isLevelDoor;
    public int ID;
    public int destinationID;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (!collider.gameObject.GetComponent<PlayerController>().isTeleporting &&
                collider.gameObject.GetComponent<PlayerController>().activeTeleporter != ID)
            {
                foreach (Teleporter tele in FindObjectsOfType<Teleporter>())
                {
                    if (tele.ID == destinationID)
                    {
                        if (gravityChanger)
                        {
                            GetComponentInChildren<GravityTrigger>().ChangeGravity(ID);
                        }

                        collider.gameObject.transform.position = tele.gameObject.transform.position;
                        collider.gameObject.GetComponent<PlayerController>().activeTeleporter = destinationID;
                        collider.gameObject.GetComponent<PlayerController>().isTeleporting = true;
                        if (isLevelDoor)
                        {
                            collider.gameObject.GetComponent<PlayerController>().currentHarness =
                                collider.gameObject.GetComponent<PlayerController>().harnessNumber - 1;
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
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
