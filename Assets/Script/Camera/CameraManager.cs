using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;

public class CameraManager : MonoBehaviour
{
    public CinemachineFreeLook thirdPerson;
    public CinemachineFreeLook closeUp;
    public CinemachineFreeLook NpcInteract;
    public CameraStates cameraState;
    private void Start()
    {
        cameraState = CameraStates.ThirdPerson;
    }
    public void OnEnable()
    {
        GameEventsManager.instance.cameraEvents.OnSwapCamera += SwapCamera;
    }
    public void OnDisable()
    {
        GameEventsManager.instance.cameraEvents.OnSwapCamera -= SwapCamera;
    }
    public void SwapCamera(CameraStates cameraState)
    {
        this.cameraState = cameraState;
        thirdPerson.Priority = 0;
        closeUp.Priority = 0;
        //NpcInteract.Priority = 0;

        switch (cameraState)
        {

            case CameraStates.ThirdPerson:
                {
                    thirdPerson.Priority = 10;
                }
                break;
            case CameraStates.CloseUp:
                {
                    closeUp.Priority = 10;
                }
                break;
            case CameraStates.Npc_Interact:
                {
                    closeUp.Priority = 10;
                }
                break;
            default:
                Debug.Log("Camera State is not recognized by switch statements. CameraStates is:" + cameraState);
                break;
        }
    }
}
