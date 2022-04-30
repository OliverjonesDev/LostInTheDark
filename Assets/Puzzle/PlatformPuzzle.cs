using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlatformPuzzle : MonoBehaviour
{
    public float amountOfPlatforms;
    public float triggeredPlatforms;
    public Light2D lightToAffect;

    public void updatePlatforms()
    {
        if (triggeredPlatforms == (amountOfPlatforms / 2))
        {
            lightToAffect.intensity = .5f;
        }
        else
        {
            lightToAffect.intensity = 1;
        }
        if (triggeredPlatforms == amountOfPlatforms)
        {
            lightToAffect.enabled = false;
            lightToAffect.GetComponent<LightingPlayerDetect>().enabled = false;
        }
        else
        {
            lightToAffect.enabled = true;
            lightToAffect.GetComponent<LightingPlayerDetect>().enabled = true;
        }
    }

}
