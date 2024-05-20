using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class Quest
{
    public QuestInfoSO info;
    public QuestState state;
    private int currentQuestStepIndex;
    private QuestStepState[] questStepStates;

    public Quest(QuestInfoSO questInfo)
    {
        this.info = questInfo;
        this.state = QuestState.REQUIREMENTS_NOT_MET;
        this.currentQuestStepIndex = 0;
        this.questStepStates = new QuestStepState[info.questStepPrefabs.Length];
        for (int i = 0; i < questStepStates.Length; i++)
        {
            questStepStates[i] = new QuestStepState();
        }
    }
    public void MoveToNextStep()
    {
        currentQuestStepIndex++;
    }
    public bool CurrentStepExists()
    {
        return (currentQuestStepIndex < info.questStepPrefabs.Length);
    }
    public void InstantiateCurrentQuestStep(Transform parentTransfom)
    {
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();
        if (questStepPrefab != null)
        {
            QuestStep questStep = Object.Instantiate<GameObject>(questStepPrefab, parentTransfom).GetComponent<QuestStep>();
            questStep.InitializeQuestStep(info.id, currentQuestStepIndex);
        }
    }
    private GameObject GetCurrentQuestStepPrefab()
    {
        GameObject questStepPrefab = null;
        if (CurrentStepExists())
        {
            questStepPrefab = info.questStepPrefabs[currentQuestStepIndex];
        }
        else
        {
            Debug.LogWarning("Tried to get quest step prefab, but stepIndex was out of range indication that " + "theres no current step: questid="
            + info.id + ", stepIndex=" + currentQuestStepIndex);
        }
        return questStepPrefab;
    }
    public void StoreQuestStepState(QuestStepState questStepState, int stepIndex)
    {
        if (stepIndex < questStepStates.Length)
        {
            questStepStates[stepIndex].state = questStepState.state;
            questStepStates[stepIndex].status = questStepState.status;
        }
        else
        {
            Debug.LogWarning("Tried to acess quest step data, but stepindex was out of range: QuestID=" + info.id + ", Step Index=" + stepIndex);
        }
    }
    public QuestData GetQuestData()
    {
        return new QuestData(state, currentQuestStepIndex, questStepStates);
    }
    public string GetFullStatusText()
    {
        string fullStatus = "";

        if (state == QuestState.REQUIREMENTS_NOT_MET)
        {
            fullStatus = "Requirements are not yet met to start this quest.";
        }
        else if (state == QuestState.CAN_START)
        {
            fullStatus = "This quest can be started!";
        }
        else
        {
            for (int i = 0; i < currentQuestStepIndex; i++)
            {
                fullStatus = "<s>" + questStepStates[i].status + "</s>\n";
            }
            if (CurrentStepExists())
            {
                fullStatus += questStepStates[currentQuestStepIndex].status;
            }
            if (state == QuestState.CAN_FINISH)
            {
                fullStatus = "The quest can be turned in!";
            }
            else if (state == QuestState.FINISHED)
            {
                fullStatus = "The quest has been completed";
            }
        }

        return fullStatus;
    }
}
