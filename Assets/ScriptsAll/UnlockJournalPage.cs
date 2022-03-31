using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockJournalPage : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private bool played;
    [SerializeField]
    private JournalUI journal;
    [SerializeField]
    private Sprite[] pagesToUnlock;
    [SerializeField]
    private SpriteRenderer speechBubble;
    [SerializeField]
    private GameObject showHelpText;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        journal = GameObject.Find("Journal").GetComponent<JournalUI>();
        speechBubble = GameObject.Find("SpeechBubble").GetComponent<SpriteRenderer>();
        speechBubble.enabled = false;
        if (showHelpText != null)
        {
            showHelpText.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!played)
        {
            if (collision.gameObject.layer == 6)
            {
                audioSource.Play();
                played = true;
                for (int i = 0; i < pagesToUnlock.Length; i++)
                {
                    journal.journalPages.Add(pagesToUnlock[i]);
                }

                StartCoroutine(ShowBubble());
                if (showHelpText != null)
                {
                    StartCoroutine(ShowHelpText());
                }
            }
        }
    }

    IEnumerator ShowBubble()
    {
        speechBubble.enabled = true;
        yield return new WaitForSeconds(2.5f);
        speechBubble.enabled = false;
    }

    IEnumerator ShowHelpText()
    {
        showHelpText.SetActive(true);
        yield return new WaitForSeconds(5f);
        showHelpText.SetActive(false);
    }

}
