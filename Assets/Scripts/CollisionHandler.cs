using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Exit":
                CompleteLevel();
                break;
            case "Friendly":
                break;
            default:
                DestroyShip();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Fuel":
                CollectFuel(other.gameObject);
                break;
            default:
                break;
        }
    }

    private void CompleteLevel()
    {
        throw new NotImplementedException();
    }

    private void CollectFuel(GameObject fuel)
    {
        Debug.Log("Fuel got");
        Destroy(fuel);
    }

    private void DestroyShip()
    {
        StartCoroutine(ReloadLevel());
    }

    IEnumerator ReloadLevel()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);
    }
}
