using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class GoInteract : QuestStep
{
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
        string state = "You have interacted!";
        ChangeState(state);
    }
}
