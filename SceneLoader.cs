using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    int currentSceneIndex;
    string currentSceneName;
    AudioSource audioSource;
    [SerializeField] AudioClip startSFX;
    bool avoidMultipleSound = true;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "NanoLogo")
        {

            Color color = Color.black;
            Initiate.Fade("StartMenu", color, 0.5f);
        }

    }

    private void Update()
    {
        if (currentSceneName == "StartMenu")
        {
            GetStarted();
        }
    }

    private void GetStarted()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (avoidMultipleSound == true)
            {
                AudioSource.PlayClipAtPoint(startSFX, Camera.main.transform.position);
                Invoke("LoadNextScene", 1f);
                avoidMultipleSound = false;
            }
        }
    }

    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex+1);
    }

    public void LoadStartScene()
    {
        FindObjectOfType<GameSession>().ResetGame();
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadInfoScene()
    {
        SceneManager.LoadScene(4);
    }

    public void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOver");
        //TODO NOTE that this is hard coded
    }
}
