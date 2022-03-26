using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour 
{
    public List<GameObject> playerInventory;
    public List<GameObject> playerPossibleInterations;
    public List<GameObject> shadowPossibleInterations;

    private void Update()
    {
        if (GetComponent<PlayerController>().controllingPlayer)
        {
            if (playerPossibleInterations.Count > 0)
            {
                for (var i = 0; i < playerPossibleInterations.Count; i++)
                {
                    if (Input.GetButtonDown("Fire2"))
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
                    if (Input.GetButtonDown("Fire2"))
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
