using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosUpdate : MonoBehaviour
{
    public GameObject shadow;
    public GameObject player;
    public PlayerMovement playerController;
    public float yOffset = 8.23f;
    private void Start()
    {
        shadow = GameObject.Find("shadow");
        player = GameObject.Find("player");
        playerController = GameObject.Find("parentOfPlayers").GetComponent<PlayerMovement>();


    }

    private void Update()
    {
        UpdateCamPos();
    }

    private void UpdateCamPos()
    {
        //transform.position = new Vector3((shadow.transform.position.x + player.transform.position.x) / 2, transform.position.y, transform.position.z);
        transform.position = new Vector3(playerController.curController.transform.position.x, playerController.curController.transform.position.y, transform.position.z);
    }
}
