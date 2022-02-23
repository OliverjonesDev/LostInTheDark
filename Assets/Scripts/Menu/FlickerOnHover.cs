using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.EventSystems;

public class FlickerOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool hovered = false;
    public float timer, flickerInterval;
    public float flickerTimeMin = .04f;
    public float flickerTimeMax = .16f;
    public UnityEngine.UI.Button button;
    public float buttonAlphaMin = .4f;
    public float buttonAlphaMax = 1f;

    private void Start()
    {
        button = GetComponent<UnityEngine.UI.Button>();
        hovered = false;
    }
    public void Update()
    {
        ColorBlock color = button.colors;
        if (hovered)
        {
            timer += Time.deltaTime;
            if (timer > flickerInterval)
            {
                float random = Random.Range(buttonAlphaMin, buttonAlphaMax);
                timer = 0;
                flickerInterval = Random.Range(flickerTimeMin, flickerTimeMax);
                color.normalColor = new Color(random, random, random, 1);
                color.pressedColor = new Color(random, random, random, 1);
                color.highlightedColor = new Color(random, random, random, 1);
                color.selectedColor = new Color(random, random, random, 1);
                button.colors= color;
            }
        }
        else
        {
            timer = 0;
            color.normalColor = new Color(.1f, .1f, .1f, 1);
            color.pressedColor = new Color(.1f, .1f, .1f, 1);
            color.highlightedColor = new Color(.1f, .1f, .1f, 1);
            color.selectedColor = new Color(.1f, .1f, .1f, 1);
            button.colors = color;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovered= true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        hovered = false;
    }

}
