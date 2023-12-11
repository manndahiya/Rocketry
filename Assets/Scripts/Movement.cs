using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float ThrustValue = 20f;
    [SerializeField] float rotateValue =20f;
   
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem BoosterPart;
    [SerializeField] ParticleSystem LeftThrustPart;
    [SerializeField] ParticleSystem RightThrustPart;



    AudioSource audioSource;

    bool isAlive = true;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        ProcessThrust();
        ProcessRotation();
        
    }


  
    void ProcessThrust()
    {

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
        {
            StartThrusting();
        }

        else
        {
            StopThrusting();
        }

    }

    private void StopThrusting()
    {
        audioSource.Stop();
        BoosterPart.Stop();
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * ThrustValue * Time.deltaTime);
        if (!BoosterPart.isPlaying)
        {
            BoosterPart.Play();
        }
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            LeftRotate();
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            RightRotate();
        }
        else
        {
            StopRotate();
        }
    }

    private void StopRotate()
    {
        LeftThrustPart.Stop();
        RightThrustPart.Stop();
    }

    private void RightRotate()
    {
        ApplyRotation(-rotateValue);
        if (!LeftThrustPart.isPlaying)
        {
            LeftThrustPart.Play();
        }
    }

    private void LeftRotate()
    {
        ApplyRotation(rotateValue);
        if (!RightThrustPart.isPlaying)
        {
            RightThrustPart.Play();
        }
    }


    void ApplyRotation(float rot)
    {
        rb.freezeRotation = true; //to disbale unity physics of rotation so we manually rotate
        transform.Rotate(Vector3.forward * rot * Time.deltaTime);
        rb.freezeRotation = false; //unfreezing rotation so physics system can take over
    }
}
