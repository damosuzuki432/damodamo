using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oban : MonoBehaviour
{
    //SFX reference
    [SerializeField] AudioClip audioClip;
    [SerializeField] public int points = 2000;

    //showscore
    [SerializeField] GameObject showScore;


    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Paddle")
        {
            TriggerSFX();
            ShowScore();
            Destroy(gameObject);
            AddScore();

        }
    }
    private void TriggerSFX()
    {
        AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
    }

    private void ShowScore()
    {
        GameObject showScoretext = Instantiate(showScore, transform.position, transform.rotation);
        showScoretext.AddComponent<Rigidbody2D>().velocity = new Vector2(0, 0.5f);
        Destroy(showScoretext, 2.0f);
    }

    private void AddScore()
    {
        FindObjectOfType<GameSession>().Score(points);
    }
}
