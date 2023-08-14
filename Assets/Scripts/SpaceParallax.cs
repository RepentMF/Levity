using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceParallax : MonoBehaviour
{
    public float parallaxXValue;
    public float parallaxYValue;
    public Vector3 playerPosition;
    public Vector3 maxLarge;
    public Vector3 maxSmall;

    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = player.gameObject.transform.position;

        parallaxXValue = -1 * ((maxSmall.x * playerPosition.x / maxLarge.x) - (maxSmall.x / 2));
        parallaxYValue = -1f * ((maxSmall.y * playerPosition.y / maxLarge.y) - (maxSmall.y / 2));

        if (playerPosition.x < 0f)
        {
            parallaxXValue = maxSmall.x / 2;
        }
        else if (playerPosition.x > maxLarge.x)
        {
            parallaxXValue = -1 * maxSmall.x / 2;
        }

        if (playerPosition.y < 0f)
        {
            parallaxYValue = maxSmall.y / 2;
        }
        else if (playerPosition.y > maxLarge.y)
        {
            parallaxYValue = -1 * maxSmall.y / 2;
        }
        
        this.gameObject.transform.localPosition = new Vector3(parallaxXValue, parallaxYValue, 1f);
    }
}