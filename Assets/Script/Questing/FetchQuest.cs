using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]


public class FetchQuest : QuestStep
{
    private bool playerClose = false;
    private void Start()
    {
        string status = "Have not fetched yes!";
        ChangeState("", status);

    }
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
            string status = "Have fetched!";
            ChangeState("", status);
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
