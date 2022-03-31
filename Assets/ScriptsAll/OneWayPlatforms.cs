using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatforms : MonoBehaviour
{
    [SerializeField]
    private GameObject platform;
    [SerializeField]
    private Collider2D curCollider;
    [SerializeField]
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        curCollider = GetComponent<PlayerMovement>().curController.GetComponent<Collider2D>();

        if (Input.GetAxis("Vertical") < 0)
        {
            if (platform != null)
            {
                StartCoroutine(DisableCollider());
            }
        }

        RaycastHit2D hit = Physics2D.Raycast(playerMovement.curController.transform.position, Vector2.down, playerMovement.slopeDetectDist);
        if (hit)
        {
            if (hit.transform.CompareTag("OneWayPlatform"))
            {
                platform = hit.transform.gameObject;
            }
            else
            {
                platform = null;
            }
        }
    }

    IEnumerator DisableCollider()
    {
        Collider2D platformCollider = platform.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(curCollider, platformCollider);
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreCollision(curCollider, platformCollider, false);
    }
}
