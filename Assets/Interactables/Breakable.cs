using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Breakable : Switch
{
    public bool hasCollision;
    public float volume;
    public Collider2D collision;
    public Collider2D triggerVolume;
    public override void OnInteract(int curController)
    {
        if (canInteract)
        {
            if (curController == 1)
            {
                audioSource.volume = volume;
                PlayAudio();
                spriteRenderer.sprite = offSprite;
                if (hasCollision)
                {
                    collision.enabled = false;
                    if (GetComponent<ShadowCaster2D>())
                    {
                        GetComponent<ShadowCaster2D>().enabled = false;
                    }
                    if (breakOnUse)
                    {
                        triggerVolume.enabled = false;
                    }
                }
                canInteract = false;
            }

        }
    }

}
