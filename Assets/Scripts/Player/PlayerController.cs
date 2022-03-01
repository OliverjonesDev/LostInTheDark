using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool controllingPlayer = true;
    public bool controllingShadow = false;
    public bool isPlayerTorchActive;
    public GameObject playerTorch;

    void Start()
    {
        controllingPlayer = true;
        playerTorch = GameObject.Find("TorchLight");
    }

    void Update()
    {

        if (!GetComponent<PlayerMovement>().isInLight && GetComponent<PlayerMovement>().jumpCheck1.collider != null && GetComponent<PlayerPullBlock>().blockPulling == false)
        {
            if (Input.GetButtonDown("Shadow Switch"))
            {
                if (controllingPlayer == true)
                {
                    controllingPlayer = false;
                    Debug.Log("Switched");
                    controllingShadow = true;
                }
                else
                {
                    controllingPlayer = true;
                    controllingShadow = false;

                }
            }
        }
        if (controllingPlayer == true)
        {
            if (playerTorch != null)
            {
                isPlayerTorchActive = playerTorch.activeInHierarchy;
                if (Input.GetButton("Fire1") && playerTorch.transform.parent.GetComponent<torchController>().battery > 0 && !playerTorch.transform.parent.GetComponent<torchController>().torchLastEmpty)
                {
                    if (!isPlayerTorchActive)
                    {
                        playerTorch.SetActive(true);
                        playerTorch.transform.parent.GetComponent<torchController>().torchOn = true;
                    }
                }
                else
                {
                    playerTorch.SetActive(false);
                    playerTorch.transform.parent.GetComponent<torchController>().torchOn = false;
                }
            }
        }
    }


}
