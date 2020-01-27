using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/*
This is attached to "Lose Collider", which trigger the gameover kind of things
for the player.
Main function is to load next scene on collison to this collider.
*/


public class LoseCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            LifePanel lifepanel = FindObjectOfType<LifePanel>();
            lifepanel.DecraeseLife();
            Destroy(collision.gameObject);
            //if (lifepanel.lifeIcons.Length >= 1)
            //{
            //    lifepanel.DecraeseLife();
            //    Destroy(collision.gameObject);
            //    Restart();
            //    //TODO Restart()

            //}
            //if (lifepanel.lifeIcons.Length <1)
            //{
            //    FindObjectOfType<SceneLoader>().Invoke("LoadGameOverScene", 2);
            //    //wait 2 seconds to load GameOver Scene
            //    //TODO NOTE that this is referred by function name

            //}
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
    public void Restart()
    {
        FindObjectOfType<SceneLoader>().Invoke("LoadCurrentScene", 2);
    }
  }
