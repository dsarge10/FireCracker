using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor
    [SerializeField] float reload = 1f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    //CACHE - references for readability or speed
    AudioSource audioSource;

    //STATE - private instance (member) variables

    void Start() 
    {
        //CACHE - audioSource to use within other methods
        audioSource = GetComponent<AudioSource>();
    }
    
    void OnCollisionEnter(Collision other)
    {
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
            // todo add SFX upon crash.
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(success);
            }
            //todo add particle effect upon crash.
            GetComponent<Movement>().enabled = false;
            Invoke("NextLevel", reload);
        }
        void StartCrashSequence() 
        {
            // todo add SFX upon crash.
            audioSource.PlayOneShot(crash);
            //todo add particle effect upon crash.
            GetComponent<Movement>().enabled = false;
            //ReloadLevel();
            Invoke("ReloadLevel", reload);
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
