using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightingPlayerDetect : MonoBehaviour
{
    public Light2D _light2D;
    public GameObject player;
    public float playerAngle;
    [Header("Thresh hold is for the angle that the player can be seen from")]
    [Header("This should be play tested to find the optimal threshhold for each light")]
    [Header("X is the lower bound, Y is the Upper bound")]
    public Vector2 threshhold;
    private Vector2 dir;
    public RaycastHit2D _raycastHit;
    public float facingAngle;
    public bool inLightAngle;
    public bool closeToLight;

    private void Start()
    {
        _light2D = GetComponent<Light2D>();
    }

    public void Update()
    {
        dir = player.transform.position - transform.position;
        playerAngle = Mathf.Atan2(dir.y, dir.x) / Mathf.PI * 180;
        facingAngle = Mathf.Atan2(transform.position.y, transform.position.x) / Mathf.PI * 180;
        if (playerAngle < 0)
        {
            playerAngle += 360;
        }
        Debug.DrawRay(transform.position, dir, Color.green);
        if (playerAngle > threshhold.x && playerAngle < threshhold.y)
        {
            inLightAngle = true;
        }
        else
        {
            inLightAngle = false;
        }
        if (Vector3.Distance(transform.position, player.transform.position) < _light2D.pointLightOuterRadius)
        {
            closeToLight = true;
        }
        else
        {
            closeToLight = false;
        }
        if (inLightAngle && closeToLight)
        {
            _raycastHit = Physics2D.Raycast(transform.position, dir, Mathf.Infinity);
            if (_raycastHit.transform.gameObject.layer == 6)
            {
                Debug.Log("Player not in shadow");
            }
        }

    }
}
