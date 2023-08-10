using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacleManager : MonoBehaviour
{
    private PlayerController player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isDead)
        {
            foreach (ObstacleMover om in FindObjectsOfType<ObstacleMover>())
            {
                om.gameObject.transform.parent.position = om.initPosition;
                om.gameObject.transform.position = om.initPosition;
                om.obstacleSpeed = om.initSpeed;
                om.currentTimer = om.initTimer;
                om.playerAlert = false;
            }
        }
        
        this.gameObject.transform.position = player.gameObject.transform.localPosition;
    }
}
