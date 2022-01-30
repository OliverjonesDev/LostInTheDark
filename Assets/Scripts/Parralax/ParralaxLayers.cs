using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxLayers : MonoBehaviour
{
    private float startPos, length;
    public float parralaxSpeed;
    public GameObject camera;

    public void Start()
    {
        startPos = transform.position.x;
        length = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
        camera = GameObject.Find("Camera");
    }

    private void FixedUpdate()
    {
        float curPos = (camera.transform.position.x * (1 - parralaxSpeed));
        float dist = camera.transform.position.x * parralaxSpeed;
        // 
        transform.position = new Vector2(startPos + dist,transform.position.y);
        //If the cur pos greater than the width of the object, reset the objects position to that edge on the right side pos x
        if (curPos > startPos + length)
        {
            startPos += length;
        }
        // if the cur pos lesser then the widht of the image, resetthe objects pos tht the edge on the left side pos x
        else if (curPos < startPos - length)
        {
            startPos -= length;
        }
    }
}
