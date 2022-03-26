using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    public GameObject torch;
    public override void OnInteract(int curController)
    {
        parentOfPlayer.GetComponent<PlayerInteractions>().playerInventory.Add(gameObject);
        if (torch)
        {
            parentOfPlayer.GetComponent<PlayerController>().playerHasTorch = true;
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
