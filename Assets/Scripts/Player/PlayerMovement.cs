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
    public float breakForce = 8;
    public float movementSpeed;
    public float jumpHeight = 6;
    public float fallRate = 3;
    public float quickJumpRate = 3;
    public float lastDirInput;

    [Header("States - Shadow")]
    public bool isInLight;
    public void Start()
    {
        playerController = GetComponent<PlayerController>();
        player = GameObject.Find("player");
        shadow = GameObject.Find("shadow");
    }

    private void FixedUpdate()
    {
        curController.GetComponent<Rigidbody2D>().velocity = playerVelocity;
    }
    private void Update()
    {
        if (!isInLight)
        {
            ControllingPlayer();
        }

    }
    void ControllingPlayer()
    {
        #region playerInput and player movement directional
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            lastDirInput = Input.GetAxisRaw("Horizontal");
        }

        //Use get axis for ease in and ease out, if you use GetAxisRaw it is just the int flat value so 0,1,-1
        if (playerController.controllingPlayer)
        {
            curController = player;
            playerVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * movementSpeed, player.GetComponent<Rigidbody2D>().velocity.y);
            if (shadow.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
            {
                shadow.GetComponent<Rigidbody2D>().velocity = new Vector2(0, shadow.GetComponent<Rigidbody2D>().velocity.y);
            }
        }
        else
        {
            curController = shadow;
            playerVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * movementSpeed, shadow.GetComponent<Rigidbody2D>().velocity.y);
            if (player.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
            {
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, player.GetComponent<Rigidbody2D>().velocity.y);
            }
        }
        #endregion

        #region player jumping
        if (Input.GetButton("Jump"))
        {
            Debug.Log("jump");
            if (playerController.controllingPlayer && player.GetComponent<Rigidbody2D>().velocity.y == 0)
            {
                player.GetComponent<Rigidbody2D>().velocity = jumpHeight * Vector2.up;
            }
            else if (playerController.controllingShadow && shadow.GetComponent<Rigidbody2D>().velocity.y == 0)
            {
                shadow.GetComponent<Rigidbody2D>().velocity = jumpHeight * Vector2.up;
            }

        }
        #endregion
    }
}
