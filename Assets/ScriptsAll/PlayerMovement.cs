using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerController playerController;
    private float playerOrgColBoundsY, shadowOrgColBoundsY;

    [Header("This script should be on the parent object of the player and the shadow")]
    public GameObject player;
    public GameObject shadow;
    public GameObject curController;
    [Header("Player Variables")]
    public Vector2 playerVelocity;
    private float movementSpeedOrg = 8;
    private float movementSpeed = 8;
    private float movementSpeedPulling = 4;
    private float climbSpeed = 4;
    public float jumpHeight = 6;
    public float lastDirInput;
    public float jumpPlayerCheckLength = 1.35f;
    public float jumpShadowCheckLength = 2f;
    [Header("Slope Variables")]
    public float maxSlope = 30;
    public float curSlope;
    public float slopeDetectDist = 5;
    public PhysicsMaterial2D slopeFriction;
    public PhysicsMaterial2D normalFriction;
    private Vector2 slopeNormal;
    public LayerMask jumpMask;
    public bool crouching;
    public bool isGrounded;
    public bool isOnLadder;
    [HideInInspector]
    public Rigidbody2D shadowRB2D, playerRB2D;
    public RaycastHit2D jumpCheck1;
    public RaycastHit2D crouchCheck;
    public bool walking;

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
        playerOrgColBoundsY = player.GetComponent<BoxCollider2D>().size.y;
        shadowOrgColBoundsY = shadow.GetComponent<BoxCollider2D>().size.y;
    }

    private void Update()
    {
        if (!isInLight && !playerInShadow)
        {
            if (maxSlope > curSlope)
            {
                ControllingPlayer();
                Jump();
                Crouching();
                OnLadder();
                curController.GetComponent<Rigidbody2D>().velocity = playerVelocity;
                curController.GetComponent<Rigidbody2D>().sharedMaterial = normalFriction;
            }
            Slope();
        }
    }

    void ControllingPlayer()
    {
        #region playerInput and player movement directional

        if (Input.GetAxis("Horizontal") < 0)
        {
            lastDirInput = -1;
            if (!GetComponent<PlayerPullBlock>().blockPulling && Input.GetAxisRaw("Horizontal") < 0)
            {
                curController.transform.localScale = new Vector3(-Mathf.Abs(curController.transform.localScale.x), curController.transform.localScale.y, curController.transform.localScale.z);
            }
            walking = true;
        }
        else if(Input.GetAxis("Horizontal") > 0)
        {
            lastDirInput = 1;
            if (!GetComponent<PlayerPullBlock>().blockPulling && Input.GetAxisRaw("Horizontal") > 0)
            {
                curController.transform.localScale = new Vector3(Mathf.Abs(curController.transform.localScale.x), curController.transform.localScale.y, curController.transform.localScale.z);
            }
            walking = true;

        }
        else
        {
            walking = false;
        }

        if (Input.GetButtonDown("Crouch") && crouchCheck.collider == null && isGrounded)
        {
            if (crouching)
            {
                crouching = false;
            }
            else
            {
                crouching = true;
            }
        }
        if (GetComponent<PlayerPullBlock>().blockPulling || crouching)
        {
            movementSpeed = movementSpeedPulling;
        }
        else
        {
            movementSpeed = movementSpeedOrg;
        }
        //Use get axis for ease in and ease out, if you use GetAxisRaw it is just the int flat value so 0,1,-1
        if (playerController.controllingPlayer)
        {
            curController = player;
            if (isGrounded)
            {
                playerVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * movementSpeed, playerRB2D.velocity.y);
            }
            else if (!isGrounded && lastDirInput != Input.GetAxisRaw("Horizontal"))
            {
                playerVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * movementSpeed / 2, playerRB2D.velocity.y);
            }

            else if (!isGrounded)
            {
                playerVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * movementSpeed, playerRB2D.velocity.y);
            }
            if (shadowRB2D.velocity != Vector2.zero)
            {
                shadowRB2D.velocity = new Vector2(0, shadow.GetComponent<Rigidbody2D>().velocity.y);
            }
        }
        else
        {
            curController = shadow;
            playerVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * movementSpeed, shadowRB2D.velocity.y);
            if (playerRB2D.velocity != Vector2.zero)
            { 
                playerRB2D.velocity = new Vector2(0, playerRB2D.velocity.y);
            }
        }
        #endregion
    }

    void Slope()
    {
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
        }
        if (curSlope > maxSlope)
        {
            playerVelocity.Set(2 * slopeNormal.x * -Input.GetAxis("Horizontal"), 2 * -slopeNormal.y * Input.GetAxis("Horizontal"));
            curController.GetComponent<Rigidbody2D>().sharedMaterial = slopeFriction;
        }
    }

    void OnLadder()
    {
        var verticalInput = Input.GetAxis("Vertical");
        Debug.Log(verticalInput);
        if (isOnLadder)
        {
            curController.GetComponent<Rigidbody2D>().gravityScale = 0;
            playerVelocity = new Vector3(playerVelocity.x, verticalInput * climbSpeed);
        }
        else
        {
            curController.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
    void Jump()
    {
        #region player jumping
        if (curController == player)
        {
            jumpCheck1 = Physics2D.Raycast(curController.transform.position, Vector2.down, jumpPlayerCheckLength);
            Debug.DrawRay(curController.transform.position, Vector2.down * jumpPlayerCheckLength);
            crouchCheck = Physics2D.Raycast(curController.transform.position, Vector2.up, jumpPlayerCheckLength);
            Debug.DrawRay(curController.transform.position, Vector2.up * jumpPlayerCheckLength, Color.red);

        }
        else
        {
            jumpCheck1 = Physics2D.Raycast(curController.transform.position, Vector2.down, jumpShadowCheckLength);
            Debug.DrawRay(curController.transform.position, Vector2.down * jumpShadowCheckLength);
            crouchCheck = Physics2D.Raycast(curController.transform.position, Vector2.up, 1);
            Debug.DrawRay(curController.transform.position, Vector2.up * 1, Color.red);
        }
        if (jumpCheck1.collider != null)
        {
            isGrounded = true;
            if (Input.GetButtonDown("Jump") && !GetComponent<PlayerPullBlock>().blockPulling)
            {
                if (crouching)
                {
                    playerVelocity = new Vector2(playerVelocity.x, jumpHeight / 1.25f);
                }
                else
                {
                    playerVelocity = new Vector2(playerVelocity.x, jumpHeight);
                }
                Debug.Log("jump");
            }
        }
        else
        {
            isGrounded = false;
            return;
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
    void Crouching()
    {
        if (crouching)
        {
            curController.GetComponent<BoxCollider2D>().offset = new Vector2(curController.GetComponent<BoxCollider2D>().offset.x, -0.46f);
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
}

