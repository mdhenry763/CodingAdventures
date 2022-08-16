using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    //Parameters - foe tuning, typically set in editor
    //Chache - e.g refernces for readability or speed
    //State - private instance (member) variables

    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem rocketMainThruster;
    [SerializeField] ParticleSystem rocketRightThrusters;
    [SerializeField] ParticleSystem rocketLeftThrusters;

    Rigidbody rb;
    AudioSource aS;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        aS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ThrustingUp();
        }
        else
        {
            aS.Stop();
            rocketMainThruster.Stop();
        }
    }
    void ThrustingUp()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!aS.isPlaying)
        {
            aS.PlayOneShot(mainEngine);
        }
        if (!rocketMainThruster.isPlaying)
        {
            rocketMainThruster.Play();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            LeftRotation();

        }
        else if (Input.GetKey(KeyCode.D))
        {
            RightRotation();
        }
        else
        {
            rocketLeftThrusters.Stop();
            rocketRightThrusters.Stop();
        }
    }

    void RightRotation()
    {
        ApplyRotation(-rotationThrust);
        if (!rocketLeftThrusters.isPlaying)
        {
            rocketLeftThrusters.Play();
        }
    }

    void LeftRotation()
    {
        ApplyRotation(rotationThrust);
        if (!rocketRightThrusters.isPlaying)
        {
            rocketRightThrusters.Play();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}


