using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Interactable
{

    public Sprite onSprite;
    public Sprite offSprite;

    public bool switchOn = false;

    public bool isApartOfSequence;
    public SequencePuzzle sequence;
    public bool canInteract =true;
    public override void OnInteract(int curController)
    {
        if (canInteract)
        {
            PlayAudio();
            if (switchOn)
            {
                switchOn = false;
                spriteRenderer.sprite = offSprite;
                if (isApartOfSequence)
                {
                    sequence.currentSequence.Remove(gameObject);
                }
            }
            else
            {
                switchOn = true;
                spriteRenderer.sprite = onSprite;
                if (isApartOfSequence)
                {
                    sequence.currentSequence.Add(gameObject);
                }
            }
        }
    }
}
