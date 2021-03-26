using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EXPERIMENTAL_CameraManager : MonoBehaviour
{
    private EXPERIMENTAL_GameMenuScreen gameMenu;

    private CinemachineBrain brain;

    private CinemachineVirtualCamera camera2D;
    private CinemachineVirtualCamera camera3D;

    private int cameraPriorityLow;
    private int cameraPriorityHigh;

    public CinemachineVirtualCamera currentActiveCamera;

    public bool transitionFlag;
    private void Start()
    {
        gameMenu = GameObject.Find("MenuManager").GetComponent<EXPERIMENTAL_GameMenuScreen>();

        brain = transform.GetChild(0).GetComponent<CinemachineBrain>();
        camera2D = transform.GetChild(1).GetComponent<CinemachineVirtualCamera>();
        camera3D = transform.GetChild(2).GetComponent<CinemachineVirtualCamera>();

        cameraPriorityLow = 1;
        cameraPriorityHigh = 2;

        camera2D.Priority = cameraPriorityHigh;
        camera3D.Priority = cameraPriorityLow;

        currentActiveCamera = camera2D;

        transitionFlag = false;
}

    private void LateUpdate()
    {
        if (transitionFlag)
        {
            Transitioning();
        }
    }

    public void ChangeActiveCamera()
    {
        if((CinemachineVirtualCamera) brain.ActiveVirtualCamera == camera2D)
        {
            currentActiveCamera = camera3D;
            
            camera3D.Priority = cameraPriorityHigh;
            camera2D.Priority = cameraPriorityLow;
        }
        else
        {
            currentActiveCamera = camera2D;

            camera2D.Priority = cameraPriorityHigh;
            camera3D.Priority = cameraPriorityLow;
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
        }
    }

    public void EndTransition()
    {
        gameMenu.ShowMenu();
    }
}
