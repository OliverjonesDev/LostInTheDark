using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ShadowBlockPlayer : MonoBehaviour
{
    public bool blockPlayer;
    public bool playerInShadow;
    public GameObject player;
    [Header("What light to brighten the shadow")]
    public GameObject isShadowLitUp;
    private PlayerMovement playerMovement;
    public float lightIntensityShadow = 1;

    private void Start()
    {
        player = GameObject.Find("player");
        playerMovement = GameObject.Find("parentOfPlayers").GetComponent<PlayerMovement>();
    }
    //We can use triggers for shadows since they are not dynamic light the lighting.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //This controlls if the light which is meant to light up and unblock the player from walking through is enabled.
        //This is incase there is no light assigned to the variable, this allows the code to still use shadows as walls for the player

        if (blockPlayer)
        {
            if (isShadowLitUp != null)
            {
                if (!isShadowLitUp.activeInHierarchy)
                {
                    if (collision.gameObject.layer == 6)
                    {
                        GetComponent<BoxCollider2D>().enabled = true;
                        player.gameObject.transform.parent.GetComponent<PlayerMovement>().playerInShadow = true;
                        Debug.Log("Entered");
                        if (collision.transform.position.x > transform.position.x)
                        {
                            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(10, 0), ForceMode2D.Impulse);
                            Debug.Log("Left");

                        }
                        else
                        {
                            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-10, 0), ForceMode2D.Impulse);
                        }
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
                    if (collision.transform.position.x > transform.position.x)
                    {
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(10, 0), ForceMode2D.Impulse);
                        Debug.Log("Left");

                    }
                    else
                    {
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-10, 0), ForceMode2D.Impulse);
                    }
                }
            }
        }
        if (collision.gameObject.layer == 10)
        {
            GetComponent<Light2D>().intensity = lightIntensityShadow;
        }
        if (collision.gameObject.layer == 6 && !blockPlayer)
        {
            GetComponent<Light2D>().intensity = lightIntensityShadow / 4;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StartCoroutine(Delay());
        if (collision.gameObject.layer == 10)
        {
            GetComponent<Light2D>().intensity = 0f;
        }
        if (collision.gameObject.layer == 6)
        {
            GetComponent<Light2D>().intensity = 0;
        }
    }

    private void Update()
    {
        if (playerMovement.playerInShadow == true)
        {
            StartCoroutine(MovePlayerBackFromShadow());
        }
        else
        {
            return;
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
