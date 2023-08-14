using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afterimage : MonoBehaviour
{
    private SpriteRenderer sprite;

    void Awake()
    {
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
        sprite.sprite = FindObjectOfType<PlayerController>().gameObject.GetComponent<SpriteRenderer>().sprite;
        sprite.flipX = FindObjectOfType<PlayerController>().gameObject.GetComponent<SpriteRenderer>().flipX;
        sprite.flipY = FindObjectOfType<PlayerController>().gameObject.GetComponent<SpriteRenderer>().flipY;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Color color = sprite.color;
        color.a -= 1.3f * Time.deltaTime;
        sprite.color = color;

        if (color.a <= 0f)
        {
            Destroy(this.gameObject);
        }
    }
}
