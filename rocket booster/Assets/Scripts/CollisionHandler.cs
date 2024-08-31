using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;

    [SerializeField] float loadLevelDelay;

    bool istransition = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();      
    }

    void OnCollisionEnter(Collision collision)
    {
        if (istransition) return;

        switch (collision.gameObject.tag) {

            case "LaunchPad":
                Debug.Log("LaunchPad");
                break;

            case "LandingPad":
                Debug.Log("LandingPad");
                StartLoading(1);
                break;

            default:
                Debug.Log("You blowed up!");
                StartLoading(0);
                break;
        }
    }

    void StartLoading(int status)
    {
        gameObject.GetComponent<Movement>().enabled = false;
        istransition = true;

        if (status == 0)
        {
            playAudio(crash);
            Invoke("RestartLevel", loadLevelDelay);
        }
        else if (status == 1) 
        {
            playAudio(success);
            Invoke("LoadNextLevel",loadLevelDelay);
        }
        
    }

    void playAudio(AudioClip clip)
    { 
            audioSource.PlayOneShot(clip);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadNextLevel()
    {
        int level = SceneManager.GetActiveScene().buildIndex;
        int nextScene = level + 1;

        if (nextScene == SceneManager.sceneCountInBuildSettings) {
            nextScene = 0;
        }

        SceneManager.LoadScene(nextScene);
    }

}
