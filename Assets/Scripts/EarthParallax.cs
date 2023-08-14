using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EarthParallax : MonoBehaviour
{
    public float earthRotation;
    public float hypotenuse;
    public float ratio;
    public float parallax;
    public float parallaxXValue;
    public float parallaxYValue;
    public Vector3 playerPosition;

    public Light2D earthLight;
    private PlayerController player;
    private Color c;

    IEnumerator FadeToRed()
    {
        float j = 0f;
        
        for (float i = 1f; i >= 0f; i -= 0.0001f, j += 0.0001f)
        {
            c.r = j;
            c.g = i;
            c.b = i;

            earthLight.color = c;
            yield return new WaitForSeconds(.05f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponentInParent<PlayerController>();
        c = earthLight.color;
        
        StartCoroutine("FadeToRed");
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = player.gameObject.transform.position;

        hypotenuse = (Mathf.Sqrt(Mathf.Pow(playerPosition.x, 2) + Mathf.Pow(playerPosition.y, 2)));
        ratio = hypotenuse / 854.4f;
        parallax = (.2f - (ratio / 10f));

        if (playerPosition.x < 0f && playerPosition.y < 0f)
        {
            parallax = .2f;
            earthLight.falloffIntensity = 0f;
        }
        else if (ratio > 1)
        {
            parallax = .1f;
            earthLight.falloffIntensity = .5f;
        }
        
        this.gameObject.transform.localScale = new Vector3(parallax, parallax, 1f);
        this.gameObject.transform.Rotate(new Vector3(0f, 0f, 1f) * Time.deltaTime);
        earthLight.falloffIntensity = (ratio) / 2;

        if (earthLight.color == Color.red)
        {
            FindObjectOfType<MovingObstacleManager>().GetComponent<AudioSource>().loop = false;
        }
    }
}
