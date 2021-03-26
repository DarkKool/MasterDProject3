using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveInformationAnimationScript : MonoBehaviour
{
    //UI Controller Reference
    private PersonalUIController uiController;

    private void Start()
    {
        //Get Reference
        uiController = GameObject.Find("UIController").GetComponent<PersonalUIController>();
    }

    private void WaveInformationEndAnimation()
    {
        uiController.WaveInformationEndAnimation();
    }
}
