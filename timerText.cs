using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timerText : MonoBehaviour
{
    [SerializeField] public float seconds = 45.0f;
    private TextMeshProUGUI timerTextUGUI;
  
    Ball ball;


    // Start is called before the first frame update
    void Start()
    {
        timerTextUGUI = GetComponentInChildren<TextMeshProUGUI>();
        ball = FindObjectOfType<Ball>();
        timerTextUGUI.text = seconds.ToString("00");
    }

    // Update is called once per frame
    void Update()
    {
        if (ball.hasStarted)
        {
            seconds -= Time.deltaTime;
            if (seconds <= 0f) { return; }
            timerTextUGUI.text = seconds.ToString("00");
        }
    }

    
}
