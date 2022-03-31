using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightSequence : MonoBehaviour
{
    [SerializeField]
    private GameObject[] lightsInSequence;
    [SerializeField]
    private float timerLeway = 2f;
    [SerializeField]
    private float[] timingsInSequence;
    [SerializeField]
    private float sequenceTimer;

    public bool puzzleStart = false;

    private void Update()
    {
        if (puzzleStart)
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
}
