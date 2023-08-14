using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ObstacleAesthetic : MonoBehaviour
{
    private GameObject[] allObjects;
    public string search;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer desiredSprite = this.gameObject.GetComponent<SpriteRenderer>();
        allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        Debug.Log(this.gameObject.name + this.gameObject.transform.parent.name);

        foreach (GameObject obj in allObjects)
        {
            if (obj.name.Contains("Level") && obj.gameObject.GetComponentInChildren<Teleporter>() != null)
            {
                // obj.AddComponent<Animator>();
                // obj.gameObject.GetComponentInChildren<Animator>().runtimeAnimatorController = Resources.Load("Bouncy Star Gravity Trigger") as RuntimeAnimatorController;

                if (obj.gameObject.GetComponent<SpriteRenderer>() != null)
                {
                    obj.gameObject.GetComponent<SpriteRenderer>().sprite = desiredSprite.sprite;
                    obj.gameObject.GetComponent<SpriteRenderer>().color = desiredSprite.color;
                    // if (this.gameObject.GetComponent<Light2D>() != null)
                    // {
                    //     obj.gameObject.AddComponent<Light2D>();
                    //     obj.gameObject.GetComponent<Light2D>().color = Color.green;
                    //     obj.gameObject.GetComponent<Light2D>().intensity = this.gameObject.GetComponent<Light2D>().intensity;
                    //     obj.gameObject.GetComponent<Light2D>().falloffIntensity = .2f;
                    // }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
