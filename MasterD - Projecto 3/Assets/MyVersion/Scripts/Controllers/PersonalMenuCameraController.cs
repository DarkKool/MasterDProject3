using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PersonalMenuCameraController : MonoBehaviour
{
    //Menu Manager Reference
    private MenuManager menuManager;

    //Camera Brain Reference
    private CinemachineBrain brain;

    //Camera Menu's References
    private CinemachineVirtualCamera personalMenuCamera;
    private CinemachineVirtualCamera selectShipMenuCamera;
    private CinemachineVirtualCamera historyMenuCamera;
    private CinemachineVirtualCamera optionsMenuCamera;

    //Current Active Camera
    private CinemachineVirtualCamera currentActiveCamera;

    //Camera's Priorities Values
    private int cameraHighPriority;
    private int cameraLowPriority;

    //Transition Between Cameras Flag
    private bool transitionFlag;

    private void Start()
    {
        //Get References
        menuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();

        brain = transform.GetChild(0).GetComponent<CinemachineBrain>();

        personalMenuCamera = transform.GetChild(1).GetComponent<CinemachineVirtualCamera>();
        selectShipMenuCamera = transform.GetChild(2).GetComponent<CinemachineVirtualCamera>();
        historyMenuCamera = transform.GetChild(3).GetComponent<CinemachineVirtualCamera>();
        optionsMenuCamera = transform.GetChild(4).GetComponent<CinemachineVirtualCamera>();

        //Set Camera's Priorities Values
        cameraHighPriority = 2;
        cameraLowPriority = 1;

        //Set Camera's Priorities
        personalMenuCamera.Priority = cameraHighPriority;
        selectShipMenuCamera.Priority = cameraLowPriority;
        historyMenuCamera.Priority = cameraLowPriority;
        optionsMenuCamera.Priority = cameraLowPriority;

        //Set Current Active Camera
        currentActiveCamera = personalMenuCamera;

        //Set Transition Flag
        transitionFlag = false;
    }

    private void LateUpdate()
    {
        if (transitionFlag)
        {
            Transitioning();
        }
    }

    public void ChangeActiveCamera(MenuManager.Menu menu)
    {
        if(menu == MenuManager.Menu.Main)
        {
            currentActiveCamera = personalMenuCamera;

            personalMenuCamera.Priority = cameraHighPriority;
            selectShipMenuCamera.Priority = cameraLowPriority;
            historyMenuCamera.Priority = cameraLowPriority;
            optionsMenuCamera.Priority = cameraLowPriority;
        }
        else if(menu == MenuManager.Menu.Select)
        {
            currentActiveCamera = selectShipMenuCamera;

            selectShipMenuCamera.Priority = cameraHighPriority;
            personalMenuCamera.Priority = cameraLowPriority;
            historyMenuCamera.Priority = cameraLowPriority;
            optionsMenuCamera.Priority = cameraLowPriority;
        }
        else if (menu == MenuManager.Menu.History)
        {
            currentActiveCamera = historyMenuCamera;

            historyMenuCamera.Priority = cameraHighPriority;
            personalMenuCamera.Priority = cameraLowPriority;
            selectShipMenuCamera.Priority = cameraLowPriority;
            optionsMenuCamera.Priority = cameraLowPriority;
        }
        else if (menu == MenuManager.Menu.Options)
        {
            currentActiveCamera = optionsMenuCamera;

            optionsMenuCamera.Priority = cameraHighPriority;
            personalMenuCamera.Priority = cameraLowPriority;
            selectShipMenuCamera.Priority = cameraLowPriority;
            historyMenuCamera.Priority = cameraLowPriority;
        }

        StartTransition();
    }

    public void StartTransition()
    {
        transitionFlag = true;
    }

    public void Transitioning()
    {
        if (currentActiveCamera == (CinemachineVirtualCamera)brain.ActiveVirtualCamera && !brain.IsBlending)
        {
            EndTransition();
            transitionFlag = false;
        }
    }

    public void EndTransition()
    {
        menuManager.ShowMenu();
    }

}
