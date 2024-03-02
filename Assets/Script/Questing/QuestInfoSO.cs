using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfoSO", menuName = "ScriptableObjects/QuestInfoSO", order = 1)]
public class QuestInfoSO : ScriptableObject
{
    [field: SerializeField] public string id { get; private set; }

    [Header("General")]
    public string displayName;
    [Header("Requirements")]
    public int levelRequirement;
    public QuestInfoSO[] questPrerequisites;
    [Header("Steps")]
    public GameObject[] questStepPrefabs;
    [Header("Rewards")]
    public int goldReward;
    public int experienceReward;



    //Name of scriptable objects have to be differant, otherwise they will have the same ID
    //Could swap what is ID if it becomes a problem
    private void OnValidate()
    {
#if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
