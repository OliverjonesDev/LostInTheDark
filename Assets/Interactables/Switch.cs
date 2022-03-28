using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Switch : Interactable
{

    public Sprite onSprite;
    public Sprite offSprite;

    public bool switchOn = false;

    public bool isApartOfSequence;
    public SequencePuzzle sequence;
    public List<Light2D> enabled;
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
                for (int i = 0; i < enabled.Count; i++)
                {
                    enabled[i].enabled = false;
                }
                if (isApartOfSequence)
                {
                    sequence.currentSequence.Remove(gameObject);
                }
            }
            else
            {
                switchOn = true;
                spriteRenderer.sprite = onSprite;
                for (int i = 0; i < enabled.Count; i++)
                {
                    enabled[i].enabled = true;
                }
                if (isApartOfSequence)
                {
                    sequence.currentSequence.Add(gameObject);
                }
            }
        }
    }
}
