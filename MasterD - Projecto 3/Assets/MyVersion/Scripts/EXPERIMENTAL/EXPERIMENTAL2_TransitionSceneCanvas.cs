using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPERIMENTAL2_TransitionSceneCanvas : MonoBehaviour
{
    //Black Screen Image Reference
    private Image transitionImage;

    //Scene Transition Flag
    private bool isTransitioning;

    //Time when Transition Started
    private float timeLerpStarted;

    //Darkening Image Duration
    private float timeGoalLerpDuration;

    void Start()
    {
        transitionImage = transform.GetChild(0).GetComponent<Image>();

        timeGoalLerpDuration = 1f;
    }

    void Update()
    {
        if (isTransitioning)
        {
            Transition();
        }
    }

    public void FlagTransition()
    {
        isTransitioning = true;
        timeLerpStarted = Time.time;
    }

    private void Transition()
    {
        float timeSinceStarted = Time.time - timeLerpStarted;
        float percentangeComplete = timeSinceStarted / timeGoalLerpDuration;

        transitionImage.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, percentangeComplete));

        if(percentangeComplete >= 1)
        {
            //Change Scene
        }
    }
}
