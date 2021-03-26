using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDisplay : MonoBehaviour
{
    //Menu Manager Reference
    private MenuManager menuManager;

    //Start Game Animation Path
    private Transform waypoints;
    private int waypointIndex;

    //Game Start Flag
    private bool isAnimating;

    //Start Game Rotation Animation ended
    private bool hasLerped;

    //Initial Euler Angles and Final Euler Angles on Start Game Rotation Animation
    private float initalLerp;
    private float endLerp;

    //Time the Start Game Rotation Animation Started and Duration
    private float timeLerpStarted;
    private float timeGoalLerpDuration;

    //Take Off Animation End Flag
    private bool transition;

    private void Start()
    {
        //Get References
        menuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();

        waypoints = GameObject.Find("StartGameAnimationWaypoints").transform;
        waypointIndex = 0;

        isAnimating = false;
        hasLerped = false;
        timeGoalLerpDuration = 3f;

        transition = false;
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
    private void Rotate()
    {
        transform.position = new Vector3(transform.position.x, 1f + 0.05f * Mathf.Sin(Time.time), transform.position.z);
    }

    private void Float()
    {
        transform.eulerAngles += new Vector3(0, Time.deltaTime * 10, 0);
    }

    //Define Time Values and Lerp Rotation Orientation
    public void DefineRotationValues()
    {
        if (transform.eulerAngles.y >= 215)
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
            else if (waypointIndex == waypoints.childCount && !transition)
            {
                menuManager.StartFinalSceneTransition();
                transition = true;
            }
        }
    }


}
