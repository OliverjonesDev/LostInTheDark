using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencePuzzle : MonoBehaviour
{
    [SerializeField]
    private GameObject[] correctSequence;
    [SerializeField]
    private List<GameObject> allSwitchsInSequence;
    public List<GameObject> currentSequence;
    [SerializeField]
    private List<GameObject> objectives;
    [SerializeField]
    private bool sequenceCompleted = false;

    public LightSequence lightSequenceLinked;

    private void Awake()
    {
       foreach (Transform child in transform)
       {
            allSwitchsInSequence.Add(child.gameObject);
       }
    }

    private void Start()
    {
        for (var i = 0; i < objectives.Count; i++)
        {
            objectives[i].SetActive(false);
        }
    }

    private void Update()
    {
        //If sequence is completed
        if (currentSequence.Count == correctSequence.Length)
        {
            //Check each index of current sequence to see if correct
            for (var i = 0; i < currentSequence.Count; i++)
            {
                //if correct enable lights
                if (currentSequence[i] == correctSequence[i])
                {
                    CompleteSequence();
                }
                //if not correct, reset current sequence
                else
                {
                    for (var j = 0; j < allSwitchsInSequence.Count; j++)
                    {
                        allSwitchsInSequence[j].GetComponent<Switch>().spriteRenderer.sprite = allSwitchsInSequence[j].GetComponent<Switch>().offSprite;
                        allSwitchsInSequence[j].GetComponent<Switch>().switchOn = false;
                        allSwitchsInSequence[j].GetComponent<Switch>().canInteract = true;
                    }
                    currentSequence.Clear();
                    sequenceCompleted = false;
                    for (var k = 0; k < objectives.Count; k++)
                    {
                        objectives[k].SetActive(false);
                    }
                }
            }
        }
    }
    void CompleteSequence()
    {
        Debug.Log("Sequence Completed");
        sequenceCompleted = true; 
        for (var j = 0; j < allSwitchsInSequence.Count; j++)
        {
            allSwitchsInSequence[j].GetComponent<Switch>().spriteRenderer.sprite = allSwitchsInSequence[j].GetComponent<Switch>().onSprite;
            allSwitchsInSequence[j].GetComponent<Switch>().switchOn = true;
            allSwitchsInSequence[j].GetComponent<Switch>().canInteract = false;
        }
        for (var i = 0; i < objectives.Count; i++)
        {
            objectives[i].SetActive(true);
        }
        if (lightSequenceLinked != null)
        {
            lightSequenceLinked.puzzleStart = true;
        }
    }
}
