using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Interactable : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip audioClip;
    public SpriteRenderer spriteRenderer;
    public float audioOffset;
    [SerializeField]
    public GameObject parentOfPlayer;
    [SerializeField]
    public Light2D interactionLight;


    private void Awake()
    {
        parentOfPlayer = GameObject.Find("parentOfPlayers");
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void PlayAudio()
    {
        audioSource.time = audioOffset;
        audioSource.Play();
    }

    public virtual void OnInteract(int CurController)
    {

        PlayAudio();
        Debug.Log("This Object was Interacted with" + gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
        Debug.Log("This is working");
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
        Debug.Log("This is working");
    }

}
