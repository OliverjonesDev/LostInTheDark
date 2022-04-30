using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [Header("Player Animations")]
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private SpriteRenderer playerRenderer;
    [SerializeField]
    private SpriteRenderer speechBubble;
    [SerializeField]
    private Sprite playerIdleAnim, playerWalkAnim , playerCrouchAnim, playerPushAnim, playerOnLadder;
    [Header("Shadow Animations")]
    [SerializeField]
    private GameObject shadow;
    [SerializeField]
    private SpriteRenderer shadowRenderer;
    [SerializeField]
    private Sprite shadowIdleAnim , shadowWalkAnim, shadowCrouchAnim;
    [SerializeField]
    private GameObject playerWalkTorchSprites;
    [SerializeField]
    private PlayerMovement referenceToPlayerMovement;
    [SerializeField]
    private PlayerPullBlock referenceToPlayerPullBlock;
    [SerializeField]
    private PlayerController referenceToPlayerController;


    private void Start()
    {
        player = GameObject.Find("player");
        shadow = GameObject.Find("shadow");
        playerRenderer = player.GetComponent<SpriteRenderer>();
        shadowRenderer = shadow.GetComponent<SpriteRenderer>();
        referenceToPlayerMovement = GetComponent<PlayerMovement>();
        referenceToPlayerPullBlock = GetComponent<PlayerPullBlock>();
        referenceToPlayerController = GetComponent<PlayerController>();
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
                if (referenceToPlayerController.playerHasTorch == true)
                {
                    playerWalkTorchSprites.SetActive(true);
                }
                else
                {
                    playerWalkTorchSprites.SetActive(false);
                }
            }
            else if (referenceToPlayerMovement.crouching)
            {
                playerRenderer.sprite = playerCrouchAnim;
                if (referenceToPlayerController.playerHasTorch == true)
                {
                    playerWalkTorchSprites.SetActive(true);
                }
                else
                {
                    playerWalkTorchSprites.SetActive(false);
                }
            }
            else if (referenceToPlayerPullBlock.blockPulling)
            {
                playerRenderer.sprite = playerPushAnim;
                if (referenceToPlayerController.playerHasTorch == true)
                {
                    playerWalkTorchSprites.SetActive(false);
                }
            }
            else if (referenceToPlayerMovement.isOnLadder)
            {
                playerRenderer.sprite = playerOnLadder;
            }
            else
            {
                playerRenderer.sprite = playerWalkAnim;
            }
        }
    }
    void ShadowAnimations()
    {
        if (referenceToPlayerMovement.curController == shadow)
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
