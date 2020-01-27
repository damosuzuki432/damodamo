using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageText : MonoBehaviour
{
    private TextMeshProUGUI stageTextUGUI;
    [SerializeField] TextMeshProUGUI stageNumText;


    // Start is called before the first frame update
    void Start()
    {
        stageTextUGUI = GetComponentInChildren<TextMeshProUGUI>();
        stageTextUGUI.text = stageNumText.text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
