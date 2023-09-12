using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor
    [SerializeField] float reloadDelay = 1f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    //CACHE - references for readability or speed
    AudioSource audioSource;

    //STATE - private instance (member) variables
    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start() 
    {
        //CACHE - audioSource to use within other methods
        audioSource = GetComponent<AudioSource>();
    }
    
    //void Update() 
    //{
      //  DebugKeys();
    //}

    //Disable before publishing.
    //void DebugKeys()
      //  {
        //    if(Input.GetKey(KeyCode.L))
          //  {
            //    NextLevel();
            //}
            //else if(Input.GetKeyDown(KeyCode.C))
            //{
              //  collisionDisabled = !collisionDisabled; // toggle collision
            //}
        //}

    void OnCollisionEnter(Collision other)
    {
        //if isTransitioning is equal to false jump out of OnCollisionEnter method without reaching the switch statement.
        if(isTransitioning || collisionDisabled) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                //Debug.Log("Sorry, you exploded!");
                StartCrashSequence();
                break;
        }
    }

        void StartSuccessSequence()
        {
            isTransitioning = true;
            audioSource.Stop();
            // SFX upon crash.
            audioSource.PlayOneShot(success);
            //particle effect upon success.
            successParticles.Play();
            GetComponent<Movement>().enabled = false;
            Invoke("NextLevel", reloadDelay);
        }
        void StartCrashSequence() 
        {
            isTransitioning = true;
            audioSource.Stop();
            // todo add SFX upon crash.
            audioSource.PlayOneShot(crash);
            //todo add particle effect upon crash.
            crashParticles.Play();
            GetComponent<Movement>().enabled = false;
            //ReloadLevel();
            Invoke("ReloadLevel", reloadDelay);
        }
        void ReloadLevel() 
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }

        void NextLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;
            if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }
            SceneManager.LoadScene(nextSceneIndex);
        }
}
