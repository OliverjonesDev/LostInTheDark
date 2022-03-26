using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Experimental.Rendering.Universal;

public class OldInteractable : MonoBehaviour
{
    public GameObject parentOfPlayer;
    private PlayerInteractions playerInteractionsScript;
    public bool collectable;
    public bool interactable;
    public bool moveable = true;
    public bool torch = false;
    public LayerMask playerLayerMask;
    public AudioSource audioSource;
    public enum InteractableType
    {
        typeSwitch,
        typeFix,
    }
    public InteractableType typeOfInteractable;
    [Header("||")]
    public bool moving;
    public bool moveOnce = false;
    public Transform newPos;
    public float animationSpeed = 2f;
    public float amplitude = .2f;
    private Vector2 transformOrg;
    private Light2D interactionLight;
    

    [Header("||")]
    public bool interactableFixed;
    public List<GameObject> requiredItemsToFix;
    public GameObject affectedPartner;
    private float input;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {

        parentOfPlayer = GameObject.Find("parentOfPlayers");
        transformOrg = transform.position;
        interactionLight = GetComponent<Light2D>();
        playerInteractionsScript = parentOfPlayer.transform.GetComponent<PlayerInteractions>();
        if (typeOfInteractable == InteractableType.typeFix)
        {
            moveable = true;
        }
        if (interactionLight != null)
        {
            interactionLight.enabled = false;
        }
        else
        {
            return;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 || collision.gameObject.layer == 6)
        {
            if (collectable)
            {
                parentOfPlayer.GetComponent<PlayerInteractions>().playerInventory.Add(gameObject);
                collectable = false;
                if (torch)
                {
                    parentOfPlayer.GetComponent<PlayerController>().playerHasTorch = true;
                    Debug.Log("Torch collected");
                    gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("Item collected" + gameObject.name);
                    gameObject.SetActive(false);
                }

            }
            if (interactable)
            {
                if (parentOfPlayer.GetComponent<PlayerController>().controllingPlayer)
                {
                    parentOfPlayer.GetComponent<PlayerInteractions>().playerPossibleInterations.Add(gameObject);
                }
                else
                {
                    parentOfPlayer.GetComponent<PlayerInteractions>().shadowPossibleInterations.Add(gameObject);
                }
                if (interactionLight != null)
                {
                    interactionLight.enabled = true;
                }
            }
        }
        if (typeOfInteractable ==InteractableType.typeFix)
        {
            if (moveable)
            {
                moveable = false;
            }
        }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (interactionLight != null)
        {
            interactionLight.enabled = false;
        }
        if (parentOfPlayer.GetComponent<PlayerController>().controllingPlayer)
        {
            parentOfPlayer.GetComponent<PlayerInteractions>().playerPossibleInterations.Remove(gameObject);
        }
        else
        {
            parentOfPlayer.GetComponent<PlayerInteractions>().shadowPossibleInterations.Remove(gameObject);
        }
        if (!moveable)
        {
            moveable = true;
        }

    }

    private void Update()
    {
        if (moveable)
        {
            if (moving)
            {
                if (moveOnce)
                {
                    transform.position = Vector2.Lerp(transform.position, newPos.position, animationSpeed * Time.deltaTime);
                }
                else
                {
                    transform.position = new Vector2(transform.position.x, transformOrg.y + (Mathf.Sin(input * animationSpeed) * amplitude));
                    input += Time.deltaTime;
                }
            }
        }
        if (interactableFixed == true && affectedPartner != null)
        {
            if (affectedPartner != null)
            {
                affectedPartner.gameObject.GetComponent<Interactable>();
            }
        }
        else
        {
            if (affectedPartner != null && typeOfInteractable == InteractableType.typeFix)
            {
                affectedPartner.gameObject.GetComponent<Interactable>();
            }
            else
            {
                return;
            }
            if (interactionLight != null)
            {
                if (interactionLight.enabled)
                {
                    interactionLight.intensity = (Mathf.Sin(Time.time * animationSpeed) + 1) / 2 + 0.3f;

                }
            }
            return;
        }
    }
    private void OnDrawGizmosSelected()
    {
        #if UNITY_EDITOR
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * amplitude);
        Gizmos.DrawRay(transform.position, Vector3.up * amplitude);
        #endif

    }

    public void Interaction()
    {
        //This lo
        if (interactable && typeOfInteractable == InteractableType.typeFix)
        {
            if (requiredItemsToFix.Count != 0)
            {
                for (var i = 0; i < requiredItemsToFix.Count; i++)
                {
                    for (var j = 0; j < playerInteractionsScript.playerInventory.Count; j++)
                    {
                        if (requiredItemsToFix[i] == (playerInteractionsScript.playerInventory[j]))
                        {
                            Debug.Log("Item interacted with: " + gameObject.name);
                            Debug.Log("Item removed: " + requiredItemsToFix[i].name);
                            interactable = false;
                            interactableFixed = true;
                            requiredItemsToFix.Remove(requiredItemsToFix[i]);
                            playerInteractionsScript.playerInventory.Remove(playerInteractionsScript.playerInventory[i]);
                            //gameObject.GetComponent<SpriteRenderer>().color = Color.black;
                            interactionLight.enabled = false;
                        }
                    }
                }
            }
        }
        if (interactable && typeOfInteractable == InteractableType.typeSwitch)
        {
            if (affectedPartner.gameObject.activeInHierarchy)
            {
                affectedPartner.gameObject.SetActive(false);
                Debug.Log("Disabled");
                PlaySound();
            }
            else if (!affectedPartner.gameObject.activeInHierarchy)
            {
                affectedPartner.gameObject.SetActive(true);
                Debug.Log("Enabled");
                PlaySound();
            }
        }
    }

    public void PlaySound()
    {
        audioSource.time = .34f;
        audioSource.Play();
    }
}
