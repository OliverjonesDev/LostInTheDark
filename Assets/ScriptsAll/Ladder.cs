using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public PlayerMovement playerController;

    private void Awake()
    {
        playerController = GameObject.Find("parentOfPlayers").GetComponent<PlayerMovement>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerController.isOnLadder = true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerController.isOnLadder = false;
    }
}
