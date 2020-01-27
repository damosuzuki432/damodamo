using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RequiredBlock : MonoBehaviour
{
    LevelManager levelManager;
    int requriedBlock;
    private TextMeshProUGUI requiredBlockText;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        requiredBlockText = GetComponentInChildren<TextMeshProUGUI>();
        requriedBlock = Mathf.FloorToInt(levelManager.numblockObject * 0.9f);
        requiredBlockText.text = requriedBlock.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
