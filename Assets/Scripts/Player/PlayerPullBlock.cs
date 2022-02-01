using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPullBlock : MonoBehaviour
{
    [Header("Pull Variables")]
    public LayerMask blockMask;
    public float maxDistanceToPull;
    public RaycastHit2D _raycastHit2D;

    public bool blockDetected = false;
    public bool blockPulling;
    private GameObject blockBeingPulled;
    private void FixedUpdate()
    {
        if (gameObject.GetComponent<PlayerMovement>().curController.GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            DetectBlock();
        }
    }

    void DetectBlock()
    {
        Debug.DrawRay(GetComponent<PlayerMovement>().curController.transform.position, maxDistanceToPull * (Vector2.right * GetComponent<PlayerMovement>().lastDirInput), Color.green);
        //Raycast starts
        _raycastHit2D = Physics2D.Raycast(GetComponent<PlayerMovement>().curController.transform.position,
            maxDistanceToPull * (Vector2.right * GetComponent<PlayerMovement>().lastDirInput), maxDistanceToPull, blockMask);
        //End
        /*
        if (_raycastHit2D.point != Vector2.zero)
        {
            blockDetected = true;
            _raycastHit2D.collider.GetComponent<Rigidbody2D>().simulated = true;
            blockBeingPulled = _raycastHit2D.collider.gameObject;
        }
        else
        {
<<<<<<< Updated upstream
            blockDetected = false;
        }
        if (blockDetected && Input.GetButton("Fire1") && !blockPulling)
        {
            _raycastHit2D.transform.parent = gameObject.GetComponent<PlayerMovement>().curController.transform;
            blockPulling = true;
        }
        if (blockDetected && Input.GetButton("Fire1") && !blockPulling)
        {
            _raycastHit2D.collider.transform.parent = gameObject.GetComponent<PlayerMovement>().curController.transform;
            blockPulling = true;
        }
        else if (Input.GetButton("Fire2") && blockPulling)
        {
            _raycastHit2D.collider.transform.parent = null;
            blockPulling = false;
=======
            blockBeingPulled.GetComponent<Rigidbody2D>().simulated = false;
            blockBeingPulled = null;
>>>>>>> Stashed changes
        }
        */

    }
}

/*
 *         if (interact)
        {
            if (!blockPulling)
            {
                _raycastHit2D.transform.parent = gameObject.GetComponent<PlayerMovement>().curController.transform;
                blockPulling = true;
                blockBeingPulled = _raycastHit2D.collider.transform.gameObject;
                Debug.Log(blockBeingPulled.name + " being pulled");
            }
            else
            {
                blockBeingPulled.transform.parent = null;
                blockPulling = false;
                Debug.Log(blockBeingPulled.name + " stopped being pulled");
            }
        }
*/
