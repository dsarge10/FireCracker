using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;

            case "Finish":
                Debug.Log("Finish");
                break;

            case "Fuel":
                Debug.Log("Fuel");
                break;

            default:
                Debug.Log("Sorry, you exploded!");
                break;
        }
    }
}
