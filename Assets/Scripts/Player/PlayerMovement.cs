using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerController playerController;
    [Header("This script should be on the parent object of the player and the shadow")]
    public GameObject player;
    public GameObject shadow;
    public GameObject curController;
    [Header("Player Variables")]
    public Vector2 playerVelocity;
    private float movementSpeed = 8;
    public float jumpHeight = 6;
    public float lastDirInput;
    public float jumpCheckLength = 0.65f;
    public LayerMask jumpMask;
    [HideInInspector]
    public Rigidbody2D shadowRB2D, playerRB2D;
    public RaycastHit2D jumpCheck1;

    [Header("States - Shadow")]
    public bool isInLight;

    [Header("States - Player")]
    public bool playerInShadow;
    public void Start()
    {
        playerController = GetComponent<PlayerController>();
        player = GameObject.Find("player");
        shadow = GameObject.Find("shadow");
        shadowRB2D = shadow.GetComponent<Rigidbody2D>();
        playerRB2D = player.GetComponent<Rigidbody2D>();
        curController = player;
    }

    private void Update()
    {
        if (!isInLight && !playerInShadow)
        {
            ControllingPlayer();
            Jump();
            curController.GetComponent<Rigidbody2D>().velocity = playerVelocity;
        }
    }
    void ControllingPlayer()
    {
        #region playerInput and player movement directional
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            lastDirInput = -1;
            if (!GetComponent<PlayerPullBlock>().blockPulling && Input.GetAxisRaw("Horizontal") < 0)
            {
                curController.transform.localScale = new Vector3(-Mathf.Abs(curController.transform.localScale.x), curController.transform.localScale.y, curController.transform.localScale.z);
            }
        }
        else if(Input.GetAxisRaw("Horizontal") > 0)
        {
            lastDirInput = 1;
            if (!GetComponent<PlayerPullBlock>().blockPulling && Input.GetAxisRaw("Horizontal") > 0)
            {
                curController.transform.localScale = new Vector3(Mathf.Abs(curController.transform.localScale.x), curController.transform.localScale.y, curController.transform.localScale.z);
            }


        }
        if (GetComponent<PlayerPullBlock>().blockPulling)
        {
            movementSpeed = 4;
        }
        else
        {
            movementSpeed = 8;
        }
        Debug.Log(curController.transform.localScale.x);
        //Use get axis for ease in and ease out, if you use GetAxisRaw it is just the int flat value so 0,1,-1
        if (playerController.controllingPlayer)
        {
            curController = player;
            playerVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * movementSpeed, playerRB2D.velocity.y);
            if (shadowRB2D.velocity != Vector2.zero)
            {
                shadowRB2D.velocity = new Vector2(0, shadow.GetComponent<Rigidbody2D>().velocity.y);
            }
            shadowRB2D.simulated = false;
            playerRB2D.simulated = true;
        }
        else
        {
            curController = shadow;
            playerVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * movementSpeed, shadowRB2D.velocity.y);
            if (playerRB2D.velocity != Vector2.zero)
            {
                playerRB2D.velocity = new Vector2(0, playerRB2D.velocity.y);
            }
            shadowRB2D.simulated = true;
            playerRB2D.simulated = false;
        }
        #endregion
    }
    void Jump()
    {
        #region player jumping
        jumpCheck1 = Physics2D.Raycast(curController.transform.position, Vector2.down, jumpCheckLength, layerMask: jumpMask);
        Debug.Log(jumpCheck1.collider);
        Debug.DrawRay(curController.transform.position, Vector2.down * jumpCheckLength);
        if (Input.GetButtonDown("Jump") && !GetComponent<PlayerPullBlock>().blockPulling)
        {
            if (jumpCheck1.collider != null)
            {
                playerVelocity= new Vector2(playerVelocity.x ,jumpHeight);
                Debug.Log("jump");
            }
            else
            {
                return;
            }
        }
        if (jumpCheck1.collider != null)
        {
            if (jumpCheck1.collider.gameObject.layer == 12)
            {
                curController.transform.parent = jumpCheck1.collider.transform;
            }
            else
            {
                curController.transform.parent = playerController.transform;
            }
        }
        #endregion
    }
}

