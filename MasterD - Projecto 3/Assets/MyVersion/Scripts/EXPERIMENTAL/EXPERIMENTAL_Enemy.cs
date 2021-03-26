using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPERIMENTAL_Enemy : MonoBehaviour
{
    //Rigidbody Reference
    private Rigidbody rb;

    //Movement Speed
    [SerializeField] private float movementSpeed;

    //Make sure it's only hit once
    private bool isHit;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isHit = false;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(0, 0, -movementSpeed);
    }
    
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet" && !isHit)
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            isHit = true;
        }
    }
}
