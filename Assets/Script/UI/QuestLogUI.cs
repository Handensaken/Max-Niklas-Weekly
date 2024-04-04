using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class QuestLogUI : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private GameObject contentParent;
    [SerializeField] private QuestLogScrollingList scrollingList;
    [SerializeField] private TextMeshProUGUI questDisplayNameText;
    [SerializeField] private TextMeshProUGUI questStatusText;
    [SerializeField] private TextMeshProUGUI goldRewardsText;
    [SerializeField] private TextMeshProUGUI experianceRewardsText;
    [SerializeField] private TextMeshProUGUI levelRequirementsText;
    [SerializeField] private TextMeshProUGUI questRequirementsText;

    private Button firstSelectedButton;

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onQuestLogTogglePressed += QuestLogTogglePressed;
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }
    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onQuestLogTogglePressed -= QuestLogTogglePressed;
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }
    public void QuestLogTogglePressed(){
        if (contentParent.activeInHierarchy){
            HideUI();
        }
        else{
            ShowUI();
        }
    }
    private void HideUI(){
        contentParent.SetActive(false);
        
        EventSystem.current.SetSelectedGameObject(null);
    }
    private void ShowUI(){
        contentParent.SetActive(true);

        if (firstSelectedButton != null){
            firstSelectedButton.Select();
        }
    }

    private void QuestStateChange(Quest quest)
    {
        QuestLogButton questLogButton = scrollingList.CreateButtonIfNotExists(quest, () =>
        {
            SetQuestLogInfo(quest);
        });
        if (firstSelectedButton == questLogButton.button)
        {
            firstSelectedButton = questLogButton.button;
            firstSelectedButton.Select();
        }

        questLogButton.SetState(quest.state);
    }

    private void SetQuestLogInfo(Quest quest)
    {
        questDisplayNameText.text = quest.info.displayName;

        levelRequirementsText.text = "Level " + quest.info.levelRequirement;
        questRequirementsText.text = "";

        foreach (QuestInfoSO prerequisiteQuestInfo in quest.info.questPrerequisites)
        {
            questRequirementsText.text += prerequisiteQuestInfo.displayName;
        }

        goldRewardsText.text = quest.info.goldReward + " GOLD";
        experianceRewardsText.text = quest.info.experienceReward + " XP";
    }
}
