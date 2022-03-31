using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    //made for debugging purposes
    //public float offset;
    private float angle = 65;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private int scale;

    private void Start()
    {
        player = GameObject.Find("player");
    }
    void FixedUpdate()
    {
        if (player.transform.localScale.x > 0)
        {
            scale = -1;
        }
        else
        {
            scale = 1;
        }
        if (Input.GetButton("RotateTorch"))
        {
            if (Input.GetAxisRaw("RotateTorch") < 0)
            {
                angle -= 5;
            }
            else if (Input.GetAxisRaw("RotateTorch") > 0)
            {
                angle += 5;
            }
        }
        /*
        //screen pos of object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        //screen pos of mouse
        Vector2 mouseOnScreen = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //angle between 2 points
        float angle = AngleBetweenMousePlayer(positionOnScreen, mouseOnScreen);

        //rot to mouse pos
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        */
    }

    float AngleBetweenMousePlayer(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg + 90;
    }
}
