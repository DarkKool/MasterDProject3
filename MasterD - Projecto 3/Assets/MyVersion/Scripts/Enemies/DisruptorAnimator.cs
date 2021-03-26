using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisruptorAnimator : MonoBehaviour
{
    //Animations Dictionary
    private Dictionary<int, Vector3> animations;

    //Choosen Movement
    private Vector3 initialPosition;
    private Vector3 finalPosition;

    //Timers
    private float initialTime;
    private float durationTime;

    //Chosen Animation Slot
    private int movementDirection;

    //Position Limits
    private float topVerticalLimit;
    private float botVerticalLimit;
    private float horizontalLimit;

    //Stopped Flag
    private bool stopped;

    //Time Stopped Timer
    private float timeStopped;

    //Battle Start Flag
    private bool battleStart;

    //Movement Flag
    private bool canMove;

    //Allow Movement Flag
    private bool allowMovement;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize Dictionary
        animations = new Dictionary<int, Vector3>()
        {
            [0] = new Vector3(0, 1, 1),
            [1] = new Vector3(-1, 1, 1),
            [2] = new Vector3(-1, 0, 1),
            [3] = new Vector3(-1, -0.5f, 1),
            [4] = new Vector3(0, -0.5f, 1),
            [5] = new Vector3(1, -0.5f, 1),
            [6] = new Vector3(1, 0, 1),
            [7] = new Vector3(1, 1, 1)
        };

        //Set Limits
        topVerticalLimit = 20f;
        botVerticalLimit = 10f;
        horizontalLimit = 25f;

        //Set Stopped
        stopped = false;

        //Set Battle Start
        battleStart = false;

        //Set Movement Flag
        canMove = false;

        //Set Allow Movement Flag
        allowMovement = false;

        //Set Complete Movement Timer
        durationTime = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (battleStart)
        {
            if (canMove && !stopped)
            {
                movementDirection = Random.Range(0, 8);

                switch (movementDirection)
                {
                    case 0:
                        if (transform.position.y + topVerticalLimit <= topVerticalLimit)
                        {
                            allowMovement = true;
                        }
                        break;
                    case 1:
                        if (transform.position.x - horizontalLimit >= -horizontalLimit && transform.position.y + topVerticalLimit <= topVerticalLimit)
                        {
                            allowMovement = true;
                        }
                        break;
                    case 2:
                        if (transform.position.x - horizontalLimit >= horizontalLimit)
                        {
                            allowMovement = true;
                        }
                        break;
                    case 3:
                        if (transform.position.x - horizontalLimit >= horizontalLimit && transform.position.y - botVerticalLimit >= -botVerticalLimit)
                        {
                            allowMovement = true;
                        }
                        break;
                    case 4:
                        if (transform.position.y - botVerticalLimit >= -botVerticalLimit)
                        {
                            allowMovement = true;
                        }
                        break;
                    case 5:
                        if (transform.position.x + horizontalLimit <= horizontalLimit && transform.position.y - botVerticalLimit >= -botVerticalLimit)
                        {
                            allowMovement = true;
                        }
                        break;
                    case 6:
                        if (transform.position.x + horizontalLimit <= horizontalLimit)
                        {
                            allowMovement = true;
                        }
                        break;
                    case 7:
                        if (transform.position.x + horizontalLimit <= horizontalLimit && transform.position.y + topVerticalLimit <= topVerticalLimit)
                        {
                            allowMovement = true;
                        }
                        break;
                }

                if (allowMovement)
                {
                    initialPosition = transform.position;
                    finalPosition = transform.position + Vector3.Scale(new Vector3(horizontalLimit, topVerticalLimit, 0), animations[movementDirection]);

                    initialTime = Time.time;

                    canMove = false;
                    allowMovement = false;
                }
            }
            else if (!canMove && !stopped)
            {
                float timeSinceStarted = Time.time - initialTime;
                float percentangeComplete = timeSinceStarted / durationTime;

                transform.position = Vector3.Lerp(initialPosition, finalPosition, percentangeComplete);

                if (percentangeComplete >= 1)
                {
                    timeStopped = durationTime + 1f;
                    stopped = true;
                }
            }
            else if (stopped && !canMove)
            {
                timeStopped -= Time.deltaTime;
                if (timeStopped <= 0)
                {
                    stopped = false;
                    canMove = true;
                }
            }

        }
    }

    public void StartBattle()
    {
        GetComponent<EnemyDisruptor>().StartBattle();

        battleStart = true;

        canMove = true;
    }
}
