using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float reload = 1f;
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
            //todo add particle effect upon crash.
            GetComponent<Movement>().enabled = false;
            Invoke("NextLevel", reload);
        }
        void StartCrashSequence() 
        {
            // todo add SFX upon crash.
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
