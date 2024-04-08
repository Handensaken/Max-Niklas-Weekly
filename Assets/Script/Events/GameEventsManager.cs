using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }
    public QuestEvents questEvents;
    public InputEvents inputEvents;
    public PlayerEvents playerEvents;
    public CameraEvents cameraEvents;

    private void Awake()
    {
        instance = this;
        questEvents = new QuestEvents();
        inputEvents = new InputEvents();
        playerEvents = new PlayerEvents();
        cameraEvents = new CameraEvents();
    }
    
}
