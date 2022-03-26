using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : Switch
{
    public bool hasCollision;
    public Collider2D collision;
    public float volume;
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
                }
                canInteract = false;
            }

        }
    }
}
