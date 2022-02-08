using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBlockPlayer : MonoBehaviour
{
    public bool playerInShadow;
    public GameObject player;
    [Header("What light to brighten the shadow")]
    public GameObject isShadowLitUp;

    private void Start()
    {
        player = GameObject.Find("player");
    }
    //We can use triggers for shadows since they are not dynamic light the lighting.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //This controlls if the light which is meant to light up and unblock the player from walking through is enabled.
        //This is incase there is no light assigned to the variable, this allows the code to still use shadows as walls for the player

        if (isShadowLitUp != null)
        {
            if (!isShadowLitUp.activeInHierarchy)
            {
                if (collision.gameObject.layer == 6)
                {
                    GetComponent<BoxCollider2D>().enabled = true;
                    player.gameObject.transform.parent.GetComponent<PlayerMovement>().playerInShadow = true;
                    Debug.Log("Entered");
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(10, 0), ForceMode2D.Impulse);
                }
            }
            else
            {
                GetComponent<BoxCollider2D>().enabled = false;
            }
        }
        else
        {
            if (collision.gameObject.layer == 6)
            {
                GetComponent<BoxCollider2D>().enabled = true;
                player.gameObject.transform.parent.GetComponent<PlayerMovement>().playerInShadow = true;
                Debug.Log("Entered");
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(10, 0), ForceMode2D.Impulse);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StartCoroutine(Delay());
    }

    private void Update()
    {
        if (player.gameObject.transform.parent.GetComponent<PlayerMovement>().playerInShadow == true)
        {
            StartCoroutine(MovePlayerBackFromShadow());
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(.3f);
        player.gameObject.transform.parent.GetComponent<PlayerMovement>().playerInShadow = false;
    }
    IEnumerator MovePlayerBackFromShadow()
    {
        if (player.gameObject.transform.position.x > gameObject.transform.parent.transform.position.x)
        {
            yield return new WaitForSeconds(.1f);
            player.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.5f, 0), ForceMode2D.Force);
        }
        if (player.gameObject.transform.position.x < gameObject.transform.parent.transform.position.x)
        {
            yield return new WaitForSeconds(.1f);
            player.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-0.5f, 0), ForceMode2D.Force);
        }
    }
}
