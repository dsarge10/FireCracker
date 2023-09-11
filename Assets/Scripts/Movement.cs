using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 10f;
    [SerializeField] AudioClip mainEngine;

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
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            //if there is no audio playing, play the rockets sound. This prevents sound ontop of other sound.
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
        }
        //add the else to the outside of the first if statement to prevent the sound from rapping over other sound.
        else
        {
            audioSource.Stop();
        }
    }

    void ProcessRotation() 
    {
        if(Input.GetKey(KeyCode.A))
        {
            //Passing rotationThrust in to allow for a positive or negative thrust. "forward or -forward".
            ApplyRotation(rotationThrust);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            //Passing rotationThrust in to allow for a positive or negative thrust. "forward or -forward".
            ApplyRotation(-rotationThrust);
        }
    }

    public void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation to allow manual rotation.
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //unfreezing rotation so the physics system can take over.
    }
}
