using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockJournalPage : MonoBehaviour
{

    AudioSource audioSource;
    bool played;

    public JournalUI journal;

    public Sprite[] pagesToUnlock;
    public SpriteRenderer speechBubble;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        journal = GameObject.Find("Journal").GetComponent<JournalUI>();
        speechBubble = GameObject.Find("SpeechBubble").GetComponent<SpriteRenderer>();
        speechBubble.enabled = false;
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
            }
        }
    }

    IEnumerator ShowBubble()
    {
        speechBubble.enabled = true;
        yield return new WaitForSeconds(2.5f);
        speechBubble.enabled = false;
    }

}
