using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;

    [SerializeField] AudioClip crashRocket;
    [SerializeField] AudioClip finish;
    [SerializeField] ParticleSystem finishVFX;
    [SerializeField] ParticleSystem crashVFX;
    bool isTransitioning = false;
    bool isColiison = false;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        RespondDebugKey();
    }

    void RespondDebugKey()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            NextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            isColiison = true;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(isTransitioning || isColiison) { return; }

        switch(other.gameObject.tag) 
        {
            case "Finish":
                Debug.Log("You won the game");
                StartSuccessSequence();
                break;
            case "Obstacle":
                Debug.Log("Reducing your hp...");
                StartCrashSequence();
                break;
            case "Fuel":
                Debug.Log("Go ahead!");
                break;
            case "Start":
                break;
            default:
                Debug.Log("Restart the game");
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        crashVFX.Play();
        audioSource.PlayOneShot(crashRocket);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        finishVFX.Play();
        audioSource.PlayOneShot(finish);
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", levelLoadDelay);
    }

    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    void NextLevel()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        if(nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }
}
