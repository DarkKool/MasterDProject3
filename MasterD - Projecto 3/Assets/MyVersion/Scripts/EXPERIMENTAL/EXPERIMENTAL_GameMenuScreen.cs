using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPERIMENTAL_GameMenuScreen : MonoBehaviour
{
    //Types of Menus
    public enum Menu
    {
        Main,
        Select
    }

    public Menu currentMenu;

    //Camera Manager Reference
    private EXPERIMENTAL_CameraManager cameraManager;

    //Canvas's Reference
    private GameObject mainCanvas;
    private GameObject selectCanvas;
    private GameObject transitionCanvas;

    //MOST LIKELY TO CHANGE: PlayerModels
    public EXPERIMENTAL2_ShipDisplay ship;

    void Start()
    {
        currentMenu = Menu.Main;

        cameraManager = GameObject.Find("Cameras").GetComponent<EXPERIMENTAL_CameraManager>();

        mainCanvas = GameObject.Find("CanvasMainMenu");
        selectCanvas = GameObject.Find("CanvasSelectShipMenu");
        transitionCanvas = GameObject.Find("CanvasSceneTransition");

        mainCanvas.SetActive(true);
        selectCanvas.SetActive(false);
    }

    public void StartGame()
    {
        HideAll();
        ship.DefineRotationValues();
    }

    public void SelectMenu()
    {
        cameraManager.ChangeActiveCamera();
        UpdateMenu();
        HideMenu();
    }

    public void BackToMainMenu()
    {
        cameraManager.ChangeActiveCamera();
        UpdateMenu();
        HideMenu();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void HideMenu()
    {
        if(currentMenu == Menu.Main)
        {
            selectCanvas.SetActive(false);
        }
        else if (currentMenu == Menu.Select)
        {
            mainCanvas.SetActive(false);
        }
    }

    public void ShowMenu()
    {
        if (currentMenu == Menu.Main)
        {
            mainCanvas.SetActive(true);
        }
        else if (currentMenu == Menu.Select)
        {
            selectCanvas.SetActive(true);
        }
    }

    public void UpdateMenu()
    {
        if (currentMenu == Menu.Main)
        {
            currentMenu = Menu.Select;
        }
        else if (currentMenu == Menu.Select)
        {
            currentMenu = Menu.Main;
        }
    }

    public void HideAll()
    {
        mainCanvas.SetActive(false);
        selectCanvas.SetActive(false);
    }

    public void Transition()
    {
        transitionCanvas.GetComponent<EXPERIMENTAL2_TransitionSceneCanvas>().FlagTransition();
    }
}
