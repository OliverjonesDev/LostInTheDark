using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightingPlayerDetect : MonoBehaviour
{
    public Light2D _light2D;
    public PlayerMovement playerMovement;
    public PlayerController playerController;
    public GameObject player;
    public GameObject playerShadow;
    public float playerAngle;
    [Header("Thresh hold is for the angle that the player can be seen from")]
    [Header("This should be play tested to find the optimal threshhold for each light")]
    [Header("X is the lower bound, Y is the Upper bound")]
    public Vector2 threshhold;
    private Vector2 dir;
    public RaycastHit2D _raycastHit;
    public LayerMask layerMask;
    public float facingAngle;
    public bool inLightAngle;
    public bool closeToLight;
    public bool shadoowInLight;

    private void Start()
    {
        _light2D = GetComponent<Light2D>();
        playerController = GameObject.Find("parentOfPlayers").GetComponent<PlayerController>();
        playerMovement = GameObject.Find("parentOfPlayers").GetComponent<PlayerMovement>();
        player = GameObject.Find("player");
        playerShadow = GameObject.Find("shadow");
    }

    public void Update()
    {
        dir = playerShadow.transform.position - transform.position;
        playerAngle = Mathf.Atan2(dir.y, dir.x) / Mathf.PI * 180;
        facingAngle = Mathf.Atan2(transform.position.y, transform.position.x) / Mathf.PI * 180;
        if (playerAngle < 0)
        {
            playerAngle += 360;
        }
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
            Debug.DrawRay(transform.position, dir, Color.green);
            _raycastHit = Physics2D.Raycast(transform.position, dir, Mathf.Infinity, layerMask:~layerMask);
            Debug.DrawLine(transform.position, _raycastHit.point, Color.red);
            if (_raycastHit.transform.gameObject.layer == 10)
            {
                Debug.Log("Shadow in light");
                playerMovement.isInLight = true;
                StartCoroutine(MovebackFromLight());
            }
            else
            {
                playerMovement.isInLight = false;
            }
        }
        else
        {
            playerMovement.isInLight = false;
        }

    }

    IEnumerator MovebackFromLight()
    {
        if (playerMovement.shadow.gameObject.transform.position.x < gameObject.transform.position.x)
        {
            yield return new WaitForSeconds(.075f);
            playerMovement.shadowRB2D.velocity = new Vector2(-4, playerMovement.playerVelocity.y);
        }
        if (playerMovement.shadowRB2D.gameObject.transform.position.x > gameObject.transform.position.x)
        {
            yield return new WaitForSeconds(.075f);
            playerMovement.shadowRB2D.velocity = new Vector2(4, playerMovement.playerVelocity.y);
        }
    }
}
