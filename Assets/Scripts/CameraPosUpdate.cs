using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosUpdate : MonoBehaviour
{
    public GameObject shadow;
    public GameObject player;

    private void Start()
    {
        shadow = GameObject.Find("shadow");
        player = GameObject.Find("player");



    }

    private void Update()
    {
        UpdateCamPos();
    }

    private void UpdateCamPos()
    {
        transform.position = new Vector3((shadow.transform.position.x + player.transform.position.x) / 2, transform.position.y, transform.position.z);
    }
}
