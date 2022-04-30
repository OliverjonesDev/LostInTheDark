using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlates : Interactable
{
    public PlatformPuzzle platformMaster;
    public bool platformOn;
    public Collider2D entityOnPlatform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!platformOn)
        {
            PlayAudio();
            platformMaster.updatePlatforms();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!platformOn)
        {
            entityOnPlatform = collision.GetComponent<Collider2D>();
            platformOn = true;
            platformMaster.triggeredPlatforms++;
            platformMaster.updatePlatforms();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (entityOnPlatform == collision)
        {
            PlayAudio();
            platformOn = false;
            platformMaster.triggeredPlatforms--;
            entityOnPlatform = null;
            platformMaster.updatePlatforms();

        }
    }
}
