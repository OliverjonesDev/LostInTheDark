using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [Header("Player Animations")]
    public GameObject player;
    public SpriteRenderer playerRenderer;
    public SpriteRenderer speechBubble;
    public Sprite playerIdleAnim;
    public Sprite playerWalkAnim;
    public Sprite playerCrouchAnim;
    public Sprite playerPushAnim;
    [Header("Shadow Animations")]
    public GameObject shadow;
    public SpriteRenderer shadowRenderer;
    public Sprite shadowIdleAnim;
    public Sprite shadowWalkAnim;
    public Sprite shadowCrouchAnim;

    public PlayerMovement referenceToPlayerMovement;
    public PlayerPullBlock referenceToPlayerPullBlock;


    private void Start()
    {
        player = GameObject.Find("player");
        shadow = GameObject.Find("shadow");
        playerRenderer = player.GetComponent<SpriteRenderer>();
        shadowRenderer = shadow.GetComponent<SpriteRenderer>();
        referenceToPlayerMovement = GetComponent<PlayerMovement>();
        referenceToPlayerPullBlock = GetComponent<PlayerPullBlock>();
        speechBubble = GameObject.Find("SpeechBubble").GetComponent<SpriteRenderer>();
        speechBubble.enabled = false;
    }
    private void Update()
    {
        PlayerAnimations();
        ShadowAnimations();
    }

    void PlayerAnimations()
    {
        if (referenceToPlayerMovement.curController == player)
        {
            if (referenceToPlayerMovement.walking && !referenceToPlayerMovement.crouching && !referenceToPlayerPullBlock.blockPulling)
            {
                playerRenderer.sprite = playerWalkAnim;
            }
            else if (referenceToPlayerMovement.crouching)
            {
                playerRenderer.sprite = playerCrouchAnim;
            }
            else if (referenceToPlayerPullBlock.blockPulling)
            {
                playerRenderer.sprite = playerPushAnim;
            }
            else
            {
                playerRenderer.sprite = playerIdleAnim;
            }
        }
    }
    void ShadowAnimations()
    {
        if (GetComponent<PlayerMovement>().curController == shadow)
        {
            if (referenceToPlayerMovement.walking && !referenceToPlayerMovement.crouching)
            {
                shadowRenderer.sprite = shadowWalkAnim;
            }
            else if (referenceToPlayerMovement.crouching)
            {
                shadowRenderer.sprite = shadowCrouchAnim;
            }
            else
            {
                shadowRenderer.sprite = shadowIdleAnim;
            }
        }
    }

}
