using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool controllingPlayer = true;
    public bool controllingShadow = false;


    // Start is called before the first frame update
    void Start()
    {
        controllingPlayer = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<PlayerMovement>().isInLight)
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
    }


}
