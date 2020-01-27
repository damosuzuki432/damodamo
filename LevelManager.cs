using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
/*
This is attached to "LevelManager".
the main funtions are:
 1. check the number of existing blocks by Tag.
 2. Load next scene when 1. is equal to zero.
*/

    //GameObject reference tagged "block"
    GameObject[] numblockObjects;

    //Return the length of Gameobject tagged "block"
    public int numblockObject;

    //use to avoid mutiple death at Update method
    bool multiDeath = false;

    //use to avoid multiple clear at Update method
    bool multiClear = false;

    public int reqdNumBlock;
    [SerializeField] TextMeshProUGUI reqdBlocks;
    [SerializeField] GameObject clearImage;
    [SerializeField] GameObject stageReadyPanel;
    [SerializeField] AudioSource backGroundMusic; // destroy when cleared
    [SerializeField] AudioClip clearJingle; // to play when cleared
    [SerializeField]float reqdRatio = 0.8f;


    Ball ball;
    timerText timerText;
    
    private void Start()
    {
        ball = FindObjectOfType<Ball>();
        timerText = FindObjectOfType<timerText>();
        CheckNumOfBlocks("Block"); //TODO NOTE that the tag is hard coded
        ShowRequiredBlocks();
        StartCoroutine(InactivateStageReady());
        Invoke("TitleImageFinishedAndLetGo", 2.0f);
        //Debug.Log(numblockObjects.Length);
    }

    IEnumerator InactivateStageReady()
    {
        yield return new WaitForSeconds(2.0f);
        stageReadyPanel.SetActive(false);
    }

    public void CheckNumOfBlocks(string blockTagObjects)
    {
        numblockObjects = GameObject.FindGameObjectsWithTag(blockTagObjects);
    }
    private void ShowRequiredBlocks()
    {
        numblockObject = numblockObjects.Length;
        reqdNumBlock = Mathf.FloorToInt(numblockObject * reqdRatio);
        reqdBlocks.text = reqdNumBlock.ToString();
    }
    private void TitleImageFinishedAndLetGo()
    {
        ball.state = Ball.State.Playable;
    }
    
    // Update is called once per frame
    void Update()
    {
        CheckNumOfBlocks("Block");
        LoadingManager();
    }

    private void LoadingManager() 
    {
        numblockObject = numblockObjects.Length;
        if (numblockObject == 0)
        {
            if (multiClear == true) {return;}
            StartClearSequence();
        }
        if (reqdNumBlock == 0　&& timerText.seconds < 0)
        {
            if (multiClear == true) {return;}
            StartClearSequence();
        }
        if (reqdNumBlock >=1 && timerText.seconds < 0)
        {
            if (multiDeath == true) {return;}
            else
            {
                StartDeathSequence();
            }
        }
    }

    private void StartDeathSequence()
    {
        ball.gameObject.SetActive(false); //inactivate ball
        FindObjectOfType<LifePanel>().DecraeseLife();
        FindObjectOfType<LoseCollider>().Restart();
        multiDeath = true;
        ball.state = Ball.State.title;
    }

    private void StartClearSequence()
    {
        //Debug.Log("clear");
        ball.state = Ball.State.Clear; //TODO meaning of this? 
        ball.gameObject.SetActive(false); //inactivate ball
        clearImage.SetActive(true); //show clear image
        multiClear = true; //prevent multiple clear seq  
        //Destroy(backGroundMusic); // stopBGM
        AudioSource.PlayClipAtPoint(clearJingle, Camera.main.transform.position); //play clear jingle
        //TODO stop time
        //TODO show button to next stage
        //TODO below is temporary
        Invoke("LoadNextScene", 2.0f);
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    

    public void DecreaseBlock()
    {
        if (reqdNumBlock > 0) //prevent reqdNumBlock from being less than 0
        {
            reqdNumBlock--;
            reqdBlocks.text = reqdNumBlock.ToString();
        }
        numblockObject--;
        Debug.Log(numblockObject);
    }
}
