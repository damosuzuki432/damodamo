using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleEdgeLeft : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FindObjectOfType<Ball>().leftTweek();
    }
  
}
