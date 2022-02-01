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
    private void Update()
    {
        if (gameObject.GetComponent<PlayerMovement>().curController.GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            DetectBlock(); 
        }
    }

    void DetectBlock()
    {
        Debug.DrawRay(GetComponent<PlayerMovement>().curController.transform.position, maxDistanceToPull * (Vector2.right * GetComponent<PlayerMovement>().lastDirInput), Color.green);
        //Raycast start
        _raycastHit2D = Physics2D.Raycast(GetComponent<PlayerMovement>().curController.transform.position,
            maxDistanceToPull * (Vector2.right * GetComponent<PlayerMovement>().lastDirInput), maxDistanceToPull, blockMask);
        //End
        Debug.Log(_raycastHit2D.point.ToString());
        if (_raycastHit2D.point != Vector2.zero)
        {
            blockDetected = true;
        }
        else
        {
            blockDetected = false;
        }
        if (blockDetected && Input.GetButton("Fire1") && !blockPulling)
        {
            _raycastHit2D.transform.parent = gameObject.GetComponent<PlayerMovement>().curController.transform;
            blockPulling = true;
        }
        else if (Input.GetButton("Fire2") && blockPulling)
        {
            _raycastHit2D.collider.transform.parent = null;
            blockPulling = false;
        }

    }
}
