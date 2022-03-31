using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    public GameObject torch;
    public PlayerInteractions playerInteractions;

    private void Start()
    {
        playerInteractions = parentOfPlayer.GetComponent<PlayerInteractions>();
    }
    public override void OnInteract(int curController)
    {
        playerInteractions.playerInventory.Add(gameObject);
        if (torch)
        {
            playerInteractions.GetComponent<PlayerController>().playerHasTorch = true;
            Debug.Log("Torch collected");
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Item collected" + gameObject.name);
            gameObject.SetActive(false);
        }
    }

}
