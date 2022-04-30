using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("This script should be on the parent object of the player and the shadow")]
    private PlayerController playerController;
    private float playerOrgColBoundsY, shadowOrgColBoundsY;
    public GameObject player, shadow, curController;
    private Rigidbody2D curConRB2D, shadowRB2D, playerRB2D;
    [Header("Player Variables")]
    [SerializeField]
    [Range(0, 10)]
    private float decceleration = 10;
    [SerializeField]
    [Range(0, 10)]
    private float acceleration = 10;
    [SerializeField]
    [Range(0, 1)]
    private float frictionAmount = 1;
    private float slopeAcceleration = .5f;
    private float accelerationOrg;
    private float velPower = .9f;
    [SerializeField]
    private float movementSpeed = 8;
    [SerializeField]
    private float climbSpeed = 4;
    [SerializeField]
    private float jumpHeight = 4.5f;
    [SerializeField]
    private float jumpPlayerCheckLength = 1.35f;
    [SerializeField]
    private float jumpShadowCheckLength = 2;
    [Header("Slope Variables")]
    [SerializeField]
    private float maxSlope = 30;
    [SerializeField]
    private float curSlope;
    public float slopeDetectDist = 3;

    [SerializeField]
    private LayerMask jumpMask;
    //These are being accessed by other scripts
    public bool crouching, walking, jumping, isGrounded, isOnLadder;
    public float lastDirInput;
    [HideInInspector]
    private RaycastHit2D jumpCheck, crouchCheck;

    [Header("States - Shadow")]
    public bool isInLight;
    [Header("States - Player")]
    public bool playerInShadow;

    [SerializeField]
    private PlayerPullBlock playerPullBlock;

    //inputs
    private float xInput;
    private float yInput;
    private void Awake()
    {
        Physics2D.queriesHitTriggers = false;
        playerController = GetComponent<PlayerController>();
        player = GameObject.Find("player");
        shadow = GameObject.Find("shadow");
        playerRB2D = player.GetComponent<Rigidbody2D>();
        shadowRB2D = shadow.GetComponent<Rigidbody2D>();
        curController = player;
        curConRB2D = curController.GetComponent<Rigidbody2D>();
        playerOrgColBoundsY = player.GetComponent<BoxCollider2D>().size.y;
        shadowOrgColBoundsY = shadow.GetComponent<BoxCollider2D>().size.y;
        playerPullBlock = GetComponent<PlayerPullBlock>();
        accelerationOrg = acceleration;
    }
    //Physics happen in here
    private void FixedUpdate()
    {
        if (!playerInShadow)
        {
            MainMovement();
        }

    }
    //Non physics updates here
    private void Update()
    {
        curConRB2D = curController.GetComponent<Rigidbody2D>();
        Inputs();
        Scale();
        SetCurrentCharacter();
        StateChecks();
        Crouching();
        Slope();
        OnLadder();
    }
    private void Inputs()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
        if (Input.GetAxis("Crouch") > 0 && !crouchCheck.transform)
        {
            crouching = false;
        }
        else if (Input.GetAxis("Crouch") < 0 && !isOnLadder && isGrounded)
        {
            crouching = true;
        }
        if (Input.GetButton("Jump") && isGrounded && !isOnLadder)
        {
            jumping = true;
            Debug.Log("Jumping");
        }
        else
        {
            jumping = false;
        }
    }
    private void Scale()
    {
        if (xInput < 0)
        {
            lastDirInput = -1;
            if (!playerPullBlock.blockPulling && xInput < 0)
            {
                curController.transform.localScale = new Vector3(-Mathf.Abs(curController.transform.localScale.x), curController.transform.localScale.y, curController.transform.localScale.z);
            }
            walking = true;
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            lastDirInput = 1;
            if (!playerPullBlock.blockPulling && xInput > 0)
            {
                curController.transform.localScale = new Vector3(Mathf.Abs(curController.transform.localScale.x), curController.transform.localScale.y, curController.transform.localScale.z);
            }
            walking = true;

        }
        else
        {
            walking = false;
        }
    }
    private void SetCurrentCharacter()
    {
        if (playerController.controllingPlayer)
        {
            shadowRB2D.velocity = new Vector2(0, shadowRB2D.velocity.y);
            curController = player;
        }
        else
        {
            playerRB2D.velocity = new Vector2(0, playerRB2D.velocity.y);
            curController = shadow;
        }
        return;
    }
    private void MainMovement()
    {
        #region Walk
        //the directon we are moving in and if we are crouching 
        float targetSpeed = crouching || playerPullBlock.blockBeingPulled ? xInput * movementSpeed * .5f : xInput * movementSpeed;
        //our current speed - what we want our speed to be
        float speedDifference = targetSpeed - curConRB2D.velocity.x;
        //if not at target speed and if we are in neg or pos
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01) ? acceleration : decceleration;
        //total movement speed, speedDiff * accelrate, to the power of our velocityPower
        //higher velocity power -> higher speed increase, then times by 1 or -1 for our direction
        float movement = Mathf.Pow(Mathf.Abs(speedDifference) * accelRate, velPower) * Mathf.Sign(speedDifference);
        curConRB2D.AddForce(Vector2.right * movement);

        #endregion

        #region jump
        //add jump force
        if (jumping && isGrounded)
        {
            curConRB2D.velocity = new Vector2(curConRB2D.velocity.x, jumpHeight);
            curConRB2D.sharedMaterial.friction = 0;
        }
        #endregion

        #region friction
        //apply this friction when no input to slow down by X amount
        if (isGrounded && xInput == 0)
        {
            float amount = Mathf.Min(Mathf.Abs(curConRB2D.velocity.x), Mathf.Abs(frictionAmount));
            amount *= Mathf.Sign(curConRB2D.velocity.x);

            curConRB2D.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }

        if (!isGrounded)
        {
            curConRB2D.sharedMaterial.friction = 0;
        }
        #endregion

    }
    private void Crouching()
    {
        if (!isOnLadder)
        {
            if (crouching)
            {
                curController.GetComponent<BoxCollider2D>().offset = new Vector2(curController.GetComponent<BoxCollider2D>().offset.x, -0.42f);
                curController.GetComponent<BoxCollider2D>().size = new Vector2(curController.GetComponent<BoxCollider2D>().size.x, shadowOrgColBoundsY / 2);
            }
            else
            {
                if (curController == shadow)
                {
                    curController.GetComponent<BoxCollider2D>().offset = new Vector2(curController.GetComponent<BoxCollider2D>().offset.x, -0.19f);
                    curController.GetComponent<BoxCollider2D>().size = new Vector2(curController.GetComponent<BoxCollider2D>().size.x, shadowOrgColBoundsY);
                }
                else if (curController == player)
                {
                    curController.GetComponent<BoxCollider2D>().offset = new Vector2(curController.GetComponent<BoxCollider2D>().offset.x, -0.01f);
                    curController.GetComponent<BoxCollider2D>().size = new Vector2(curController.GetComponent<BoxCollider2D>().size.x, playerOrgColBoundsY);
                }
            }
        }
        else
        {
            crouching = false;
        }

    }
    private void StateChecks()
    {
        Physics2D.queriesHitTriggers = false;
        if (curController == shadow)
        {
            jumpCheck = Physics2D.Raycast(curController.transform.position, Vector2.down, jumpShadowCheckLength, layerMask: ~jumpMask);
        }
        else
        {
            jumpCheck = Physics2D.Raycast(curController.transform.position, Vector2.down, jumpPlayerCheckLength, layerMask: ~jumpMask);
        }
        isGrounded = jumpCheck.transform;
        Debug.DrawRay(curController.transform.position, Vector2.down * jumpPlayerCheckLength);
        crouchCheck = Physics2D.Raycast(curController.transform.position, Vector2.up, jumpPlayerCheckLength);
        Debug.DrawRay(curController.transform.position, Vector2.up * jumpPlayerCheckLength, Color.red);
    }
    private void Slope()
    {
        Vector2 slopeNormal;
        Debug.DrawRay(curController.transform.position, Vector2.down * slopeDetectDist);
        RaycastHit2D curSlopeData = Physics2D.Raycast(curController.transform.position, Vector2.down, slopeDetectDist);
        if (curSlopeData)
        {
            slopeNormal = Vector2.Perpendicular(curSlopeData.normal).normalized;
            curSlope = Vector2.Angle(Vector2.down, curSlopeData.normal) - 180;
            curSlope = Mathf.Abs(curSlope);
        }
        else
        {
            curSlope = 0;
            slopeNormal = Vector2.zero;
        }
        acceleration = curSlope > maxSlope ? slopeAcceleration : accelerationOrg;
    }
    private void OnLadder()
    {
        if (isOnLadder)
        {
            curController.GetComponent<Rigidbody2D>().gravityScale = 0;
            curConRB2D.velocity = new Vector3(curConRB2D.velocity.x, yInput * climbSpeed);
        }
        else
        {
            curController.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
}

