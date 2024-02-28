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
        }
    }
    // protected override void SetQuestStepState(string state){ }
}
