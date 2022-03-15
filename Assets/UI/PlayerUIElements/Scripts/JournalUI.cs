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
    public Sprite[] journalPages;
    

    private void Start()
    {
        journalPage = GetComponent<Image>();
        gameObject.SetActive(false);
    }

    public void Back()
    {
        if (currentPage > 0)
        {
            currentPage--;
        }
    }

    public void Forth()
    {
        if (currentPage < journalPages.Length - 1)
        {
            currentPage++;
        }
    }

    private void Update()
    {
        journalPage.sprite = journalPages[currentPage];
    }
}
