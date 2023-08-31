using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip explosionSFX;
    [SerializeField] float volume = 0.6f;

    AudioPlayer audioPlayer;
    bool isDead = false;

    private void Start()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isDead)
        {
            switch (collision.gameObject.tag)
            {
                case "Exit":
                    StartCoroutine(CompleteLevel());
                    break;
                case "Friendly":
                    break;
                default:
                    DestroyShip();
                    break;
            }
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

    private IEnumerator CompleteLevel()
    {
        isDead = true;
        yield return new WaitForSecondsRealtime(levelLoadDelay);

        var nextLevelSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextLevelSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextLevelSceneIndex = 0;
        }

        SceneManager.LoadScene(nextLevelSceneIndex);
    }

    private void CollectFuel(GameObject fuel)
    {
        Debug.Log("Fuel got");
        Destroy(fuel);
    }

    private void DestroyShip()
    {
        if (!isDead)
        {
            var movement = GetComponent<Movement>();
            movement.DisableMovement();
            audioPlayer.PlayClipOnce(explosionSFX, volume, StaticReferences.SFXChannel);
            StartCoroutine(ReloadLevel());
            isDead = true;
        }
    }

    IEnumerator ReloadLevel()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);
    }
}
