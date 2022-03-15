using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class torchController : MonoBehaviour
{
    public Light2D torchLight;
    public bool torchOn;
    public bool torchLastEmpty;
    public float flickerInterval, flickerTimeMin, flickerTimeMax, torchLightIntensityMin, torchLightIntensityMax;
    public float battery = 100, batteryDecreaseRatePS, batteryIncreaseRatePS;
    private float timer = 0;
    public Image[] batteryIndicators;
    void Update()
    {
        timer += Time.deltaTime;
        //This is turned on by the player controller script. The torch interaction may be moved into this script later to increase
        //encapsulation and reduce spaghetti code. there is alot right now which I need to rewrite but this is a mockup.

        //If the player runs out of battery, the torch is put on a cooldown timer and has to recharge to 100 percent to be able to used again
        //this is promqote good resource management from the player. This can also have an animation of the player character hitting the torch or checking
        //the batteries in the torch to make sure its working.

        if (battery < 5)
        {
            torchLastEmpty = true;
        }
        if (torchLastEmpty)
        {
            battery += batteryIncreaseRatePS * Time.deltaTime;
            if (battery >= 100)
            {
                torchLastEmpty = false;
            }
        }
        else
        {
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
                //Using this means its an instant recharge but I can control how much is recharged per second
                //This kind of system with out a delay can be seen in newer games that promote better gameplay flow
                //with quicker recharge stamina systems.
                if (battery < 100)
                {
                    battery += batteryIncreaseRatePS * Time.deltaTime;
                }
                else
                {
                    battery = 100;
                }
            }
        }
        BatteryIndicator();



        //If battery is not at 100 start the coroutine to charge the torch fully so player can use it.
        //Using coroutines as its cleaner than doing a for loop with a set timer, just use
        //wait for seconds in the coroutine return for a delay;
        /*
        if (battery <= 100)
        {
            StartCoroutine(RechargeTorch());
        }
        else
        {
            StopAllCoroutines();
        }
        */

    }
    /*
    IEnumerator RechargeTorch()
    {
        yield return new WaitForSeconds(4f);
        battery = 100;
        Debug.Log("Torch recharged");

    }
    */

    private void BatteryIndicator()
    {
        //This gets the length of the battery indicator array
        //and sets the respective opacity depending on what its mapped value (i * (100 / (e.g. 2)))
        //this means that if it is the second in the array and that its current value is less then 75, it will effect the member 2 in the arrays
        //opacity
        for (int i = 0; i < batteryIndicators.Length; i++)
        {
            //As there are 
            batteryIndicators[i].color = new Color(batteryIndicators[i].color.r, batteryIndicators[i].color.g, batteryIndicators[i].color.b, Map(i * (100 / (batteryIndicators.Length + 1)), 100, 0, 1, battery));
        }

    }

    //writing map function from p5js in unity - can use to calculate the opacity of the batteries
    //maps a set of values to different values e.g. 50 - 100 to 0, 1;

    public float Map(float inmin, float inmax, float outmin, float outmax, float curValue)
    {
        return (curValue - inmin) * (outmax - outmin) / (inmax - inmin) + outmin;
    }
}
