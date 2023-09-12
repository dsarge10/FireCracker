using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 200f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
    [SerializeField] ParticleSystem boosterParticles;

    //CACHE - references for readability or speed
    Rigidbody rb;
    AudioSource audioSource;

    //STATE - private instance (member) variables

    // Start is called before the first frame update
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
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrust();
        }
        //add the else to the outside of the first if statement to prevent the sound from rapping over other sound.
        else
        {
            StopThrust();
        }
    }

    void ProcessRotation() 
    {
        if(Input.GetKey(KeyCode.A))
        {
            TurnLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            TurnRight();
        }
        else 
        {
            StopTurning();
        }
    }
    
    void StartThrust() 
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            //if there is no audio playing, play the rockets sound. This prevents sound ontop of other sound.
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            if (!boosterParticles.isPlaying)
            {
            boosterParticles.Play();
            }
    }

    void StopThrust()
    {
            audioSource.Stop();
            boosterParticles.Stop();
    }

    void TurnLeft() 
    {
        //Passing rotationThrust in to allow for a positive or negative thrust. "forward or -forward".
            ApplyRotation(rotationThrust);
            if (!leftThrusterParticles.isPlaying)
            {
            leftThrusterParticles.Play();
            }
    }

    void TurnRight()
    {
        //Passing rotationThrust in to allow for a positive or negative thrust. "forward or -forward".
            ApplyRotation(-rotationThrust);
             if (!rightThrusterParticles.isPlaying)
            {
            rightThrusterParticles.Play();
            }
    }

    void StopTurning()
    {
            leftThrusterParticles.Stop();
            rightThrusterParticles.Stop();
    }

    public void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation to allow manual rotation.
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //unfreezing rotation so the physics system can take over.
    }
}
