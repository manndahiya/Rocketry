using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float ThrustValue = 20f;
    [SerializeField] float rotateValue =20f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        ProcessThrust();
        ProcessRotation();
        
    }


  
    void ProcessThrust()
    {

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * ThrustValue * Time.deltaTime);
          
        }

        
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotateValue);
        }
      else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotateValue);
        }
    }

    void ApplyRotation(float rot)
    {
        rb.freezeRotation = true; //to disbale unity physics of rotation so we manually rotate
        transform.Rotate(Vector3.forward * rot * Time.deltaTime);
        rb.freezeRotation = false; //unfreezing rotation so physics system can take over
    }
}
