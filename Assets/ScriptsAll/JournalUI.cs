using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalUI : MonoBehaviour
{
    [Header("Set the current page to the page you would like the player to see first and then they can affect this later whilst in game.")]
    [SerializeField]
    private int currentPage = 0;
    private Image journalPage;
    public List<Sprite> journalPages;
    [Header("Make these list elements you wish to lock")]

    private AudioSource audioSource;

    private void Start()
    {
        journalPage = GetComponent<Image>();
        gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    public void Back()
    {
        if (currentPage > 0)
        {
            currentPage--;
            playSound();
        }
    }

    public void Forth()
    {
        if (journalPages.Count >= currentPage+1)
        {
            if (currentPage < journalPages.Count - 1)
            {
                currentPage++;
                playSound();
            }
        }

    }

    private void Update()
    {
        journalPage.sprite = journalPages[currentPage];
        if (Input.GetButtonDown("RotateTorch"))
        {
            if (Input.GetAxisRaw("RotateTorch") < 0)
            {
                Forth();
            }
            else if (Input.GetAxisRaw("RotateTorch") > 0)
            {
                Back();
            }
        }
    }

    private void playSound()
    {
        audioSource.time = 2.2f;
        audioSource.Play();
    }
}
