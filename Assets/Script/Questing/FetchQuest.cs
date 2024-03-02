using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]


public class FetchQuest : QuestStep
{
    private bool playerClose = false;
    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.OnSubmitPressed += SubmitPressed;
    }
    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.OnSubmitPressed -= SubmitPressed;
    }
    private void SubmitPressed()
    {
        if (playerClose)
        {
            FinishQuestStep();
        }
    }
    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            playerClose = true;
        }
    }
    private void OnTriggerExit(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            playerClose = false;
        }
    }
    // protected override void SetQuestStepState(string state){ }
}
