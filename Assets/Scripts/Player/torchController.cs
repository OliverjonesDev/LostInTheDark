using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class torchController : MonoBehaviour
{
    public Light2D torchLight;
    public bool torchOn;
    public float flickerInterval, flickerTimeMin, flickerTimeMax, torchLightIntensityMin, torchLightIntensityMax;
    public float battery = 100, batteryDecreaseRatePS;
    private float timer = 0;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //This is turned on by the player controller script. The torch interaction may be moved into this script later to increase
        //encapsulation and reduce spaghetti code. there is alot right now which I need to rewrite but this is a mockup.

        if (torchOn)
        {
            if (timer > flickerInterval)
            {
                timer = 0;
                flickerInterval = Random.Range(flickerTimeMin, flickerTimeMax);
                torchLight.intensity = Random.Range(torchLightIntensityMin, torchLightIntensityMax);
            }
                battery -= Time.deltaTime * batteryDecreaseRatePS;
        }
        else
        {
            //If battery is not at 100 start the coroutine to charge the torch fully so player can use it.
            //Using coroutines as its cleaner than doing a for loop with a set timer, just use
            //wait for seconds in the coroutine return for a delay;
            if (battery <= 100)
            {
                StartCoroutine(RechargeTorch());
            }
            else
            {
                StopAllCoroutines();
            }
        }

    }

    IEnumerator RechargeTorch()
    {
        yield return new WaitForSeconds(4f);
        battery = 100;
        Debug.Log("Torch recharged");

    }
}
