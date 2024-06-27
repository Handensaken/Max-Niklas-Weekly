using System.Net.Http.Headers;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using System.Net;
using Yarn.Unity;
using Yarn;
using Unity.VisualScripting;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(SphereCollider))]
public class QuestPoint : MonoBehaviour
{
    [Header("Quest")]
    [SerializeField] private QuestInfoSO questInfoForPoint;
    [Header("Config")]
    [SerializeField] private bool startPoint = true;
    [SerializeField] private bool finishPoint = true;
    private DialogueRunner dialogueRunner; //utility object that servers lines of dialogue
    public String questDialogueName;
    private bool playerIsNear = false;
    private string questID;
    private QuestState currentQuestState;
    private QuestIcon questIcon;
    private void Awake()
    {
        questID = questInfoForPoint.id;
        questIcon = GetComponentInChildren<QuestIcon>();
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
    }
    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
        GameEventsManager.instance.inputEvents.OnSubmitPressed += SubmitPressed;
    }
    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
        GameEventsManager.instance.inputEvents.OnSubmitPressed -= SubmitPressed;
    }
    private void SubmitPressed()
    {
        if (!playerIsNear)
        {
            return;
        }
        if (currentQuestState.Equals(QuestState.CAN_START) && startPoint)
        {
            GameEventsManager.instance.questEvents.StartQuest(questID);
            Debug.Log("Started " + questID);
            if (questDialogueName != null)
            {
                dialogueRunner.StartDialogue(questDialogueName);
            }
        }
        else if (currentQuestState.Equals(QuestState.CAN_FINISH) && finishPoint)
        {
            GameEventsManager.instance.questEvents.FinishQuest(questID);
            Debug.Log("Finished ");
        }
    }
    private void QuestStateChange(Quest quest)
    {
        if (quest.info.id.Equals(questID))
        {
            currentQuestState = quest.state;
            questIcon.SetState(currentQuestState, startPoint, finishPoint);
        }
    }
    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }
    private void OnTriggerExit(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }
}
