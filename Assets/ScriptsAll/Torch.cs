using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    //made for debugging purposes
    //public float offset;

    void Update()
    {

        //screen pos of object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        //screen pos of mouse
        Vector2 mouseOnScreen = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //angle between 2 points
        float angle = AngleBetweenMousePlayer(positionOnScreen, mouseOnScreen);

        //rot to mouse pos
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    float AngleBetweenMousePlayer(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg + 90;
    }
}
