using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPERIMENTAL2_ShipDisplay : MonoBehaviour
{
    private EXPERIMENTAL_GameMenuScreen gameMenu;

    //Start Game Animation Path
    private Transform waypoints;
    private int waypointIndex;

    //
    private bool isAnimating;
    private bool hasLerped;

    private float initalLerp;
    private float endLerp;

    private float timeLerpStarted;
    private float timeGoalLerpDuration;

    private bool transitioning;

    private void Start()
    {
        gameMenu = GameObject.Find("MenuManager").GetComponent<EXPERIMENTAL_GameMenuScreen>();

        waypoints = GameObject.Find("StartGameAnimationWaypoints").transform;
        waypointIndex = 0;

        isAnimating = false;
        hasLerped = false;
        timeGoalLerpDuration = 3f;

        transitioning = false;
    }

    private void Update()
    {
        //Basic Display
        if (!isAnimating)
        {        
            Rotate();
            Float();
        }
        else
        {
            StartGameProcess();
        }
    }

    //Basic Display
    private void Float()
    {
        transform.position = new Vector3(transform.position.x, 1f + 0.05f * Mathf.Sin(Time.time), transform.position.z);
    }

    private void Rotate()
    {
        transform.eulerAngles += new Vector3(0, Time.deltaTime * 10, 0);
    }

    //Define Values and Rotation Orientation
    public void DefineRotationValues()
    {
        if(transform.eulerAngles.y >= 215)
        {
            initalLerp = transform.eulerAngles.y;
            endLerp = waypoints.GetChild(waypointIndex).transform.eulerAngles.y + 360;
        }
        else
        {
            initalLerp = transform.eulerAngles.y;
            endLerp = waypoints.GetChild(waypointIndex).transform.eulerAngles.y;
        }

        timeLerpStarted = Time.time;
        isAnimating = true;
    }

    //Game Start Process
    private void StartGameProcess()
    {
        if (!hasLerped)
        {
            StartGameRotationAnimation();
        }
        else
        {
            if (waypointIndex < waypoints.childCount)
            {
                transform.position += (waypoints.GetChild(waypointIndex).transform.position - transform.position) * Time.deltaTime;

                if ((transform.position - waypoints.GetChild(waypointIndex).transform.position).magnitude <= 7f)
                {
                    waypointIndex++;
                }
            }
            else if(waypointIndex == waypoints.childCount && !transitioning)
            {
                gameMenu.Transition();
                transitioning = true;
            }
        }
    }

    //Game Start Animation
    private void StartGameRotationAnimation()
    {
        float timeSinceStarted = Time.time - timeLerpStarted;
        float percentangeComplete = timeSinceStarted / timeGoalLerpDuration;

        transform.eulerAngles = new Vector3(0, Mathf.Lerp(initalLerp, endLerp, percentangeComplete), 0);

        if (percentangeComplete >= 1)
        {
            hasLerped = true;
        }
    }
}
