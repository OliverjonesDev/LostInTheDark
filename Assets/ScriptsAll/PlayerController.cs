using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool controllingPlayer = true;
    public bool controllingShadow = false;
    public bool playerHasTorch = false;
    public bool isPlayerTorchActive;
    public torchController torchController;
    //[SerializeField]
    //private GameObject TorchBatteryUI;
    [SerializeField]
    private GameObject playerTorch;
    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private PlayerPullBlock playerPullBlock;
    [SerializeField]
    private PlayerInteractions playerInteractions;
    void Start()
    {
        controllingPlayer = true;
        playerTorch = GameObject.Find("TorchLight");
        playerPullBlock =GetComponent<PlayerPullBlock>();
        playerMovement = GetComponent<PlayerMovement>();
        playerInteractions = GetComponent<PlayerInteractions>();
        torchController = playerTorch.transform.parent.GetComponent<torchController>();
    }

    void Update()
    {

        if (!playerMovement.isInLight && playerMovement.jumpCheck1.collider != null && playerPullBlock.blockPulling == false && !playerMovement.crouching)
        {
            if (Input.GetAxis("Shadow Switch") < 0)
            {
                {
                    controllingPlayer = true;
                    controllingShadow = false;

                }
            }
            else if (Input.GetAxis("Shadow Switch") > 0)
            {
                if (controllingPlayer == true)
                {
                    controllingPlayer = false;
                    Debug.Log("Switched");
                    controllingShadow = true;
                    torchController.torchOn = false;
                    isPlayerTorchActive = false;
                    playerTorch.SetActive(false);
                }
            }
        }
        if (controllingPlayer == true)
        {
            if (playerInteractions.playerPossibleInterations.Count == 0)
            {
                if (playerTorch != null)
                {
                    //TorchBatteryUI.SetActive(true);
                    isPlayerTorchActive = playerTorch.activeInHierarchy;
                    if (Input.GetButtonDown("Interact") && torchController.battery > 0 && !torchController.torchLastEmpty)
                    {
                        if (!isPlayerTorchActive)
                        {
                            playerTorch.SetActive(true);
                            torchController.torchOn = true;
                        }
                        if (isPlayerTorchActive)
                        {
                            playerTorch.SetActive(false);
                            torchController.torchOn = false;
                            //TorchBatteryUI.SetActive(false);
                        }
                    }
                }
            }
        }
        if (!playerHasTorch)
        {
            playerTorch.SetActive(false);
            torchController.torchOn = false;
            //TorchBatteryUI.SetActive(false);
        }
        else
        {
            //TorchBatteryUI.SetActive(true);
        }
        if (playerPullBlock.blockPulling)
        {
            torchController.torchOn = false;
        }
    }

}
