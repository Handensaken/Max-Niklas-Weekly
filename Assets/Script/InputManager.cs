using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
public void ToggleQuestUI(InputAction.CallbackContext context){
    if (context.started){
        GameEventsManager.instance.inputEvents.QuestLogTogglePressed();
    }
}
  public void SubmitPressed(InputAction.CallbackContext context){
    if (context.started){
        GameEventsManager.instance.inputEvents.SubmitPressed();
    }
  }
  public void Test(){
    Debug.Log("pepe");
  }
}
