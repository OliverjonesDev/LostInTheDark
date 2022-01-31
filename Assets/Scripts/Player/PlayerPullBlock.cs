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
        DetectBlock();
    }

    void DetectBlock()
    {
        Debug.DrawRay(GetComponent<PlayerMovement>().curController.transform.position, maxDistanceToPull * (Vector2.right * GetComponent<PlayerMovement>().lastDirInput), Color.green);
        _raycastHit2D = Physics2D.Raycast(GetComponent<PlayerMovement>().curController.transform.position, maxDistanceToPull * (Vector2.right * GetComponent<PlayerMovement>().lastDirInput), maxDistanceToPull, blockMask);
        Debug.Log(_raycastHit2D.point.ToString());
        if (_raycastHit2D.point != Vector2.zero)
        {
            blockDetected = true;
        }
        else
        {
            blockDetected = false;
        }
        if (blockDetected && Input.GetButtonDown("Fire1"))
        {
            _raycastHit2D.transform.parent.transform = gameObject.GetComponent<PlayerMovement>().curController.transform;
            blockPulling = true;
            Debug.Log("Pulling");
        }
        else if (Input.GetButtonDown("Fire1") && blockPulling)
        {
            gameObject.GetComponent<PlayerMovement>().curController.transform.DetachChildren();
            blockPulling = false;
        }

    }
}
