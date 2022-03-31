using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour 
{
    public List<GameObject> playerInventory;
    public List<GameObject> playerPossibleInterations;
    public List<GameObject> shadowPossibleInterations;
    private PlayerController playerController;
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void LateUpdate()
    {
        if (playerController.controllingPlayer)
        {
            if (playerPossibleInterations.Count > 0)
            {
                if (playerController.isPlayerTorchActive)
                {
                    playerController.torchController.torchOn = false;
                    playerController.torchController.torchLight.gameObject.SetActive(false);
                    playerController.isPlayerTorchActive = false;
                    Debug.Log("Torch Off");
                }
                for (var i = 0; i < playerPossibleInterations.Count; i++)
                {
                    if (Input.GetButtonDown("Interact"))
                    {
                        var curController = 1;
                        playerPossibleInterations[i].GetComponent<Interactable>().OnInteract(curController) ;
                        Debug.Log("Interation Called");
                    }
                }
            }
            else
            {
                return;

            }
        }
        if (GetComponent<PlayerController>().controllingShadow)
        {
            if (shadowPossibleInterations.Count > 0)
            {
                for (var i = 0; i < shadowPossibleInterations.Count; i++)
                {
                    if (Input.GetButtonDown("Interact"))
                    {
                        var curController = 2;
                        shadowPossibleInterations[i].GetComponent<Interactable>().OnInteract(curController);
                        Debug.Log("Interation Called");
                    }
                }
            }
            else
            {
                return;

            }
        }
    }
}
