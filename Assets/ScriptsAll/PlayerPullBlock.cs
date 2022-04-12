using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPullBlock : MonoBehaviour
{
    [Header("Pull Variables")]
    public LayerMask blockMask;
    public float maxDistanceToPull;
    public RaycastHit2D _raycastHit2D;
    public bool interact;

    public bool blockDetected = false;
    public bool blockPulling;
    public GameObject blockBeingPulled;

    private float setYValue;

    private void Update()
    {
        DetectBlock();
    }

    void DetectBlock()
    {
        Debug.DrawRay(GetComponent<PlayerMovement>().curController.transform.position, maxDistanceToPull * (Vector2.right * GetComponent<PlayerMovement>().lastDirInput), Color.green);
        //Raycast starts
        _raycastHit2D = Physics2D.Raycast(GetComponent<PlayerMovement>().curController.transform.position,
        maxDistanceToPull * (Vector2.right * GetComponent<PlayerMovement>().lastDirInput), maxDistanceToPull, blockMask);
        if (_raycastHit2D.point != Vector2.zero)
        {
            blockDetected = true;
        }
        else
        {
            blockDetected = false;
        }
        if (Input.GetButton("Interact") && GetComponent<PlayerMovement>().isGrounded)
        {
            interact = true;
        }
        else
        {
            interact = false;
        }

        if (GetComponent<PlayerMovement>().curController.transform.parent == transform)
        {
            if (interact)
            {
                if (blockDetected)
                {
                    if (!blockPulling)
                    {
                        setYValue = _raycastHit2D.transform.position.y;
                        _raycastHit2D.transform.parent = gameObject.GetComponent<PlayerMovement>().curController.transform;
                        blockPulling = true;
                        blockBeingPulled = _raycastHit2D.collider.transform.gameObject;
                        Debug.Log(blockBeingPulled.name + " being pulled");
                    }
                }
            }
            else
            {
                if (blockPulling)
                {
                    blockBeingPulled.transform.parent = null;
                    blockPulling = false;
                    Debug.Log(blockBeingPulled.name + " stopped being pulled");
                    blockBeingPulled.transform.position = new Vector3(blockBeingPulled.transform.position.x, setYValue);
                    blockBeingPulled = null;
                }
            }
        }
        else if (blockPulling)
        {
            blockBeingPulled.transform.parent = null;
            blockPulling = false;
            Debug.Log(blockBeingPulled.name + " stopped being pulled");
            blockBeingPulled.transform.position = new Vector3(blockBeingPulled.transform.position.x, setYValue);
            blockBeingPulled = null;
        }
    }
}

