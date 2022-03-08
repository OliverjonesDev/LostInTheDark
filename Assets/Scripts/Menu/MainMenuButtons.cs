using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{

    public GameObject optionsMenu;
    public GameObject levelsMenu;
    public GameObject menu;
    public void Play()
    {
        if (!levelsMenu.activeInHierarchy)
        {
        
            levelsMenu.SetActive(true);
        }
        else
        {
            levelsMenu.SetActive(false);
        }

    }

    public void Options()
    {
        if (!optionsMenu.activeInHierarchy)
        {

            optionsMenu.SetActive(true);
        }
        else
        {
            optionsMenu.SetActive(false);
        }

    }

    public void Quit()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Continue()
    {
        menu.SetActive(false);
    }
    public void LevelOne()
    {
        SceneManager.LoadScene("Level01");
    }
    public void LevelTwo()
    {
        SceneManager.LoadScene("Level02");
    }
    public void LevelThree()
    {
        SceneManager.LoadScene("Level03");
    }
    public void LevelFour()
    {
        SceneManager.LoadScene("Level04");
    }

    private void Update()
    {
        if (menu != null)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                if (!menu.activeSelf)
                {
                    menu.SetActive(true);
                }
                else
                {
                    menu.SetActive(false);
                }
            }
        }
    }
}
