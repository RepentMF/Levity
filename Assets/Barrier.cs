using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public int direction;

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            switch (direction)
            {
                case 1:
                    if (collider.transform.position.y > this.gameObject.transform.position.y)
                    {
                        collider.transform.position = new Vector3 (collider.transform.position.x, 
                            this.gameObject.GetComponent<BoxCollider2D>().bounds.min.y,
                            collider.transform.position.z);
                    }
                    break;
                case 2:
                    if (collider.transform.position.x > this.gameObject.transform.position.x)
                    {
                        collider.transform.position = new Vector3 (this.gameObject.GetComponent<BoxCollider2D>().bounds.min.x,
                            collider.transform.position.y, collider.transform.position.z);
                    }
                    break;
                case 3:
                    if (collider.transform.position.y < this.gameObject.transform.position.y)
                    {
                        collider.transform.position = new Vector3 (collider.transform.position.x, 
                            this.gameObject.GetComponent<BoxCollider2D>().bounds.max.y,
                            collider.transform.position.z);
                    }
                    break;
                case 4:
                    if (collider.transform.position.x < this.gameObject.transform.position.x)
                    {
                        collider.transform.position = new Vector3 (this.gameObject.GetComponent<BoxCollider2D>().bounds.max.x,
                            collider.transform.position.y, collider.transform.position.z);
                    }
                    break;
            }
        }
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
