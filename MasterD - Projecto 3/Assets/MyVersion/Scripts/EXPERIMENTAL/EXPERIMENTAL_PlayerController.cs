using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EXPERIMENTAL_PlayerController : MonoBehaviour
{
    //--------[2G] 2D Gameplay-------------------------------
        //RigidBody Constraint
    RigidbodyConstraints constraints2D;
    
        //Direction
    private float direction2D;

        //Speed
    [SerializeField] private float movementSpeed;

    //-------------------------------------------------------

    //--------[3G] 3D Gameplay-------------------------------
        //RigidBody constraint
    RigidbodyConstraints constraints3D;

        //Direction
    private float xDirection;
    private float yDirection;

    //-------------------------------------------------------

    //--------[OG] Overall Gameplay--------------------------
        //Rigidbody Reference
    private Rigidbody rb;

        //Gameplay View Toggle
    private bool view2D;

        //Cannon Shooting Location Reference
    private Transform leftCannon;
    private Transform rightCannon;

        //Missile Prefab
    private GameObject missile;

        //Shooting Timer
    [SerializeField] private float shootTime;

        //Shooting Time Tracker
    private float shootTimeTrack;

    //-------------------------------------------------------
    private void Start()
    {
        //[OG]
        rb = GetComponent<Rigidbody>();
        view2D = true;
        leftCannon = transform.GetChild(0);
        rightCannon = transform.GetChild(1);
        missile = (GameObject) Resources.Load("MissileConceptModel");

        //[2D]
        constraints2D = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
        rb.constraints = constraints2D;

        //[3D]
        constraints3D = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
    }

    private void Update()
    {
        Debug.Log(view2D);

        if (view2D)
        {
            //[2G]
            rb.constraints = constraints2D;
            direction2D = Input.GetAxisRaw("Horizontal");
        }
        else
        {
            //[3G]
            rb.constraints = constraints3D;
            xDirection = Input.GetAxisRaw("Horizontal");
            yDirection = Input.GetAxisRaw("Vertical");
        }
        
        //[OG]
        Shoot();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeDimensionGameplay(!view2D);
        }
    }

    private void FixedUpdate()
    {
        if (view2D)
        {
            //[2G]
            rb.velocity = new Vector3(direction2D, 0, 0) * movementSpeed;
        }
        else
        {
            //[3G]
            rb.velocity = new Vector3(xDirection, yDirection, 0).normalized * movementSpeed;
        }
    }

    private void Shoot()
    {
        //[OG]
        if(shootTimeTrack >= shootTime)
        {
            Instantiate(missile, leftCannon);
            Instantiate(missile, rightCannon);
            shootTimeTrack = 0;
        }
        else
        {
            shootTimeTrack += Time.deltaTime;
        }
    }

    public void ChangeDimensionGameplay(bool is2D)
    {
        view2D = is2D;

        if (view2D)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
            rb.constraints = constraints2D;
        }
        else
        {
            rb.constraints = constraints3D;
        }
    }
}
