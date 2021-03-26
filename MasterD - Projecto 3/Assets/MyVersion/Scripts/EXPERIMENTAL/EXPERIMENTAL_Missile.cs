using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPERIMENTAL_Missile : MonoBehaviour
{
    //Rigidbody Reference
    private Rigidbody rb;

    //Missile GameObject Holder Reference
    private Transform missileParent;

    //Movement Speed
    [SerializeField] private float movementSpeed;

    void Start()
    {
        missileParent = GameObject.Find("Missiles").transform;
        rb = GetComponent<Rigidbody>();

        transform.parent = missileParent;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(0, 0, movementSpeed);
    }
}
