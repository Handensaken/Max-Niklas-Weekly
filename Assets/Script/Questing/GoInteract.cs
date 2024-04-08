using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class GoInteract : QuestStep
{
    private void Start()
    {
        string status = "Have not interacted yet!";
        ChangeState("", status);

    }
    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            FinishQuestStep();
            UpdateState();
        }
    }
    // protected override void SetQuestStepState(string state){ }
    protected void UpdateState()
    {
        string status = "Have interacted!";
        ChangeState("", status);
    }
}
