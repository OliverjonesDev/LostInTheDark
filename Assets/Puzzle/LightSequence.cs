using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightSequence : MonoBehaviour
{
    public GameObject[] lightsInSequence;
    public float timerLeway = 2f;
    public float[] timingsInSequence;
    public float sequenceTimer;

    private void Update()
    {
        sequenceTimer += Time.deltaTime;
        for (int i = 0; i < lightsInSequence.Length; i++)
        {
            if (sequenceTimer > timingsInSequence[i])
            {
                lightsInSequence[i].GetComponent<Light2D>().enabled = false;
                lightsInSequence[i].GetComponent<LightingPlayerDetect>().enabled = false;
            }
            if (i == lightsInSequence.Length - 1)
            {
                if (sequenceTimer > timingsInSequence[i] + timerLeway)
                {
                    sequenceTimer = 0;
                    for (int j = 0; j < lightsInSequence.Length; j++)
                    {
                        lightsInSequence[j].GetComponent<Light2D>().enabled = true;
                        lightsInSequence[j].GetComponent<LightingPlayerDetect>().enabled = true;
                    }

                }
            }

        }

    }
}
