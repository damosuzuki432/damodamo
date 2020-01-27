using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
/*
This is attached to "GameSession".
the main funtions are:
 1. control game speed. you can either increase/decrease the entire speed of the game.
 2. manage scoreborad. It is called by block class because these two are closely related.
 3. TODO manage BGM of the game.
 4. 1,2,3 must be maintained through the entire gamesession, so the concept of Singlton comes in.
    that is why you see Dont Destroy on Load below.
    if you need one thing, such as score, music, etc, and want to use it throuhg entire game,
    lets get it together in ONE class like below.   
 */

    //Game speed params
    [Range(0f,10f)][SerializeField] float timeScale = 1.0f;

    //Game score params
    public int score = 0;
    [SerializeField] TextMeshProUGUI scoreNum;

    //Game Debug bool
    [SerializeField] bool isAutoPlayEnabled;

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        int gameSessionCount = FindObjectsOfType<GameSession>().Length;
        if (gameSessionCount > 1) // if one already exists...
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Manage Speed of the Game
        Time.timeScale = timeScale;
        //Debug.Log(score);
    }

    public void Score(int scorePerBreak)
    {
        //get accumurated score
        score += scorePerBreak;
        
        //display score on scoreNumtext;
        scoreNum.text = score.ToString();

    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }
}
