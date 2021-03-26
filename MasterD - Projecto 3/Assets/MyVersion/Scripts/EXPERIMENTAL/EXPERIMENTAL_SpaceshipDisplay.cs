using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPERIMENTAL_SpaceshipDisplay : MonoBehaviour
{
    private Rigidbody rb;

    private Transform waypoints;
    private int waypointIndex;

    private float previousMousePosition;
    private float currentMousePosition;

    private float deltaMousePosition;

    private float angularRotationSpeed;

    //Test
    private bool isAnimating;
    private bool hasLerped;

    private float initalLerp;
    private float endLerp;

    private float timeLerpStarted;
    private float timeGoalLerpDuration;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        waypoints = GameObject.Find("StartGameAnimationWaypoints").transform;
        waypointIndex = 0;

        //Test
        isAnimating = false;
        hasLerped = false;
        timeGoalLerpDuration = 3f;
    }

    private void Update()
    {
        //Sinusoidal Movement
        //transform.position = new Vector3(transform.position.x, 0.555f + 0.05f * Mathf.Sin(Time.time), transform.position.z);



        if (Input.GetMouseButtonDown(0))
        {
            //Get Initial Mouse Position
            previousMousePosition = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            //Get Updated Mouse Position
            currentMousePosition = Input.mousePosition.x;

            //Difference Between Current Mouse Position and Previous Mouse Position
            deltaMousePosition = currentMousePosition - previousMousePosition;

            //Update Current Mouse Position to Previous
            previousMousePosition = currentMousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //Force Difference Between Both Moments to be Zero.
            deltaMousePosition = 0;
        }

        //TESTS
        if (isAnimating)
        {
            StartGameAnimation();
        }
    }

    private void FixedUpdate()
    {
        //Adding Speed to the Rigidbody Rotation
        angularRotationSpeed += deltaMousePosition * Time.deltaTime / 2;

        //Limiting Maximum and Minimum Rigidbody Rotation Values
        angularRotationSpeed = Mathf.Clamp(angularRotationSpeed, -4, 4);

        //Apply Rotation Speed to Rigidbody
        rb.angularVelocity = new Vector3(0, angularRotationSpeed, 0);

        //Decrease, Increase and Force Rotation Speed towards 0
        if (angularRotationSpeed > 0)
        {
            angularRotationSpeed -= Time.deltaTime;
            
            if(angularRotationSpeed < 0.2f)
            {
                angularRotationSpeed = 0;
            }
        }
        else if(angularRotationSpeed < 0)
        {
            angularRotationSpeed += Time.deltaTime;

            if (angularRotationSpeed > -0.2f)
            {
                angularRotationSpeed = 0;
            }
        }
    }

    public void StartGame()
    {
        initalLerp = transform.eulerAngles.y;
        endLerp = waypoints.GetChild(waypointIndex).transform.eulerAngles.y;
        timeLerpStarted = Time.time;
        isAnimating = true;
    }

    public void StartGameAnimation()
    {
        if (!hasLerped)
        {
            StartingAnimation();
        }
        else
        {
            if(waypointIndex < waypoints.childCount)
            {
                transform.position += (waypoints.GetChild(waypointIndex).transform.position - transform.position) * Time.deltaTime;

                if ((transform.position - waypoints.GetChild(waypointIndex).transform.position).magnitude <= 7f)
                {
                    waypointIndex++;
                }
            }
        }

        /*if(waypointIndex < waypoints.childCount)
        {
            //------Movement----------------------------------------------------------------------------------------------------------------
            transform.position += (waypoints.GetChild(waypointIndex).transform.position - transform.position) * Time.deltaTime;

            if(transform.position == waypoints.GetChild(waypointIndex).transform.position)
            {
                waypointIndex++;
            }
            //------Movement----------------------------------------------------------------------------------------------------------------

            //------Rotation----------------------------------------------------------------------------------------------------------------
            

            //------Rotation----------------------------------------------------------------------------------------------------------------
        }

        //transform.LookAt();*/
    }

    private void StartingAnimation()
    {
        float timeSinceStarted = Time.time - timeLerpStarted;
        float percentangeComplete = timeSinceStarted / timeGoalLerpDuration;

        transform.eulerAngles = new Vector3(0, Mathf.Lerp(initalLerp, endLerp, percentangeComplete), 0);

        if(percentangeComplete >= 1)
        {
            hasLerped = true;
        }
    }
}
