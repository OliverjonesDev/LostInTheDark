using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    private int totalCharactersInTrigger;
    public GameObject moveToNextSceneButton;
   private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 || collision.gameObject.layer == 10)
        {
            totalCharactersInTrigger++;
            if (totalCharactersInTrigger == 2)
            {
                moveToNextSceneButton.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 || collision.gameObject.layer == 10)
        {
            if (totalCharactersInTrigger > 0)
            {
                totalCharactersInTrigger--;
                moveToNextSceneButton.SetActive(false);
            }
        }
    }
}
