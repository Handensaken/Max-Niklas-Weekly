using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> questMap;
    private void Awake(){
        questMap = CreateQuestMap();
    }
    private void OnEnable(){
        GameEventsManager.instance.questEvents.onStartQuest += StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest += FinishQuest;
        GameEventsManager.instance.questEvents.onQuestStepStateChange += QuestStepStateChange;
    }
    private void OnDisable(){
        GameEventsManager.instance.questEvents.onStartQuest -= StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest -= FinishQuest;
        GameEventsManager.instance.questEvents.onQuestStepStateChange -= QuestStepStateChange;
    }
    private void Start(){
        foreach (Quest quest in questMap.Values)
        {
            GameEventsManager.instance.questEvents.QuestStateChange(quest);
        }
    }
    private void ChangeQuestState(string id, QuestState state){
        Quest quest = GetQuestById(id);
        quest.state = state;
        GameEventsManager.instance.questEvents.QuestStateChange(quest);
    }
    private bool CheckRequirementsMet(Quest quest){
        bool meetsRequirements = true;
        foreach (QuestInfoSO prerequisiteQuestInfo in quest.info.questPrerequisites)
        {
            if (GetQuestById(prerequisiteQuestInfo.id).state != QuestState.FINISHED){
                meetsRequirements = false;
            }
        }
        return meetsRequirements;
    }
    private void Update(){
        foreach (Quest quest in questMap.Values)
        {
            if (quest.state == QuestState.REQUIREMENTS_NOT_MET && CheckRequirementsMet(quest)){
                ChangeQuestState(quest.info.id, QuestState.CAN_START);
            }
        }
    }
    private void StartQuest(string id){
        Quest quest = GetQuestById(id);
        quest.InstantiateCurrentQuestStep(this.transform);
        ChangeQuestState(quest.info.id, QuestState.IN_PROGRESS);
    }
    private void AdvanceQuest(string id){
        Quest quest = GetQuestById(id);

        quest.MoveToNextStep();

        if (quest.CurrentStepExists()){
            quest.InstantiateCurrentQuestStep(this.transform);
        }
        else {
            
            ChangeQuestState(quest.info.id, QuestState.CAN_FINISH);
        }
    }
    private void FinishQuest(string id){
        Quest quest = GetQuestById(id);
        ClaimRewards(quest);
        ChangeQuestState(quest.info.id, QuestState.FINISHED);
    }
    private void ClaimRewards(Quest quest){
        //could get gold or xp like, GameEventsManager.instance.goldEnvents.GoldGained(quest.info.goldReward);
    }
    private void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState){
        Quest quest = GetQuestById(id);
        quest.StoreQuestStepState(questStepState, stepIndex);
        ChangeQuestState(id, quest.state);
    }
    private Dictionary<String, Quest> CreateQuestMap()
    {
        
        QuestInfoSO[] allQuest = Resources.LoadAll<QuestInfoSO>("Quests");

        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach (QuestInfoSO questInfo in allQuest)
        {
            if (idToQuestMap.ContainsKey(questInfo.id))
            {
                Debug.LogWarning("Duplicate ID found when creating quest map" + questInfo.id);
            }
            idToQuestMap.Add(questInfo.id, new Quest(questInfo));
        }
        return idToQuestMap;
    }
    private Quest GetQuestById(string id){
        Quest quest = questMap[id];
        if (quest == null){
            Debug.LogError("ID not found in the quest map: " + id);
        }
        return quest;
    }
    private void OnApplicationQuit(){
        foreach (Quest quest in questMap.Values)
        {
            QuestData questData = quest.GetQuestData();
            Debug.Log(quest.info.id);
            Debug.Log("State =" + questData.state);
            Debug.Log("index =" + questData.questStepIndex);
            foreach (QuestStepState stepState in questData.questStepStates)
            {
                Debug.Log("Step state = " + stepState.state);
            }
        }
    }
}
