using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionScenes : MonoBehaviour
{
    //Menu Manager Reference
    private MenuManager menuManager;

    //Game Controller Reference
    private PersonalUIController uiController;

    //Transition Image Reference
    private Image transitionImage;

    //Time when Transition Started
    private float timeLerpStarted;

    //Time duration of the Transition
    [SerializeField] private float timeGoalLerpDuration;

    //Flags to Start or End Transitions depending on who Called It
    private bool goTransitionMenuManagerWTD;
    private bool goTransitionMenuManagerDTW;
    private bool goTransitionUIControllerWTD;
    private bool goTransitionUIControllerDTW;

    // Start is called before the first frame update
    void Awake()
    {
        //Get Reference
        transitionImage = transform.GetChild(0).GetComponent<Image>();

        //Set Flags to False
        goTransitionMenuManagerWTD = false;
        goTransitionMenuManagerDTW = false;
        goTransitionUIControllerWTD = false;
        goTransitionUIControllerDTW = false;
    }
    // UgoTransitionGameControllerBTWpdate is called once per frame
    void Update()
    {
        if (goTransitionMenuManagerWTD)
        {
            WhiteToDarkTransition(menuManager);
        }
        else if (goTransitionMenuManagerDTW)
        {
            DarkToWhiteTransition(menuManager);
        }
        else if (goTransitionUIControllerWTD)
        {
            WhiteToDarkTransition(uiController);
        }
        else if (goTransitionUIControllerDTW)
        {
            DarkToWhiteTransition(uiController);
        }
    }

    public void SetTimeLerpStarted()
    {
        timeLerpStarted = Time.unscaledTime;
    }


    #region Menu Manager
    public void DarkToWhiteTransition(MenuManager menuManager)
    {
        this.menuManager = menuManager;

        goTransitionMenuManagerDTW = true;

        float timeSinceStarted = Time.unscaledTime - timeLerpStarted;
        float percentangeComplete = timeSinceStarted / timeGoalLerpDuration;

        transitionImage.color = new Color(0, 0, 0, Mathf.Lerp(1, 0, percentangeComplete));

        if (percentangeComplete >= 1)
        {
            goTransitionMenuManagerDTW = false;
            menuManager.EndInitialSceneTransition();
        }
    }

    public void WhiteToDarkTransition(MenuManager menuManager)
    {
        this.menuManager = menuManager;

        goTransitionMenuManagerWTD = true;

        float timeSinceStarted = Time.unscaledTime - timeLerpStarted;
        float percentangeComplete = timeSinceStarted / timeGoalLerpDuration;

        transitionImage.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, percentangeComplete));

        if (percentangeComplete >= 1)
        {
            goTransitionMenuManagerWTD = false;
            menuManager.EndFinalSceneTransition();
        }
    }
    #endregion

    #region UI Controller
    public void DarkToWhiteTransition(PersonalUIController uiController)
    {
        this.uiController = uiController;

        goTransitionUIControllerDTW = true;

        float timeSinceStarted = Time.unscaledTime - timeLerpStarted;
        float percentangeComplete = timeSinceStarted / timeGoalLerpDuration;

        transitionImage.color = new Color(0, 0, 0, Mathf.Lerp(1, 0, percentangeComplete));

        if (percentangeComplete >= 1)
        {
            goTransitionUIControllerDTW = false;
            uiController.EndInitialSceneTransition();
        }
    }

    public void WhiteToDarkTransition(PersonalUIController uiController)
    {
        this.uiController = uiController;

        goTransitionUIControllerWTD = true;

        float timeSinceStarted = Time.unscaledTime - timeLerpStarted;
        float percentangeComplete = timeSinceStarted / timeGoalLerpDuration;

        transitionImage.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, percentangeComplete));

        if (percentangeComplete >= 1)
        {
            goTransitionUIControllerWTD = false;
            uiController.EndFinalSceneTransition();
        }
    }
    #endregion
}
