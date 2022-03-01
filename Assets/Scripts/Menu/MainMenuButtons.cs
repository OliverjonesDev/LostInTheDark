using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{

    public GameObject optionsMenu;
    public GameObject levelsMenu;
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

    public void LevelOne()
    {

    }
    public void LevelTwo()
    {

    }
    public void LevelThree()
    {

    }
    public void LevelFour()
    {

    }
}
