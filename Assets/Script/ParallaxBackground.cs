using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour//背景跟随相机移动
{

    private GameObject cam;

    [SerializeField] private float paralaxEffect;

    private float xPosition;
    private float length;
    
    void Start()
    {
        cam = GameObject.Find("Main Camera");

        length = GetComponent<SpriteRenderer>().bounds.size.x;
        xPosition = transform.position.x;

    }

    
    void Update()
    {
        float distanceMoved = cam.transform.position.x * (1 - paralaxEffect);
        float distanceToMove = cam.transform.position.x * paralaxEffect;   

        transform.position = new Vector2(xPosition + distanceToMove, transform.position.y);


        if (distanceMoved > xPosition + length)
        {
            xPosition += length;
        }
        else if (distanceMoved < xPosition - length)
        {
            xPosition -= length;
        }
        
    }
}
