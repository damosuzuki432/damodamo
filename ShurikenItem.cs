using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenItem : MonoBehaviour
{
    [SerializeField]GameObject Shuriken;
    paddle paddle;
    [SerializeField]AudioClip powerUpSound;
    float distance = 0.6f; //y distance btw paddle to instantiate
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Paddle")
        {
            paddle = FindObjectOfType<paddle>();
            Vector2 shurikenPos = new Vector2(paddle.transform.position.x, paddle.transform.position.y + distance);
            Instantiate(Shuriken, shurikenPos, transform.rotation);
            AudioSource.PlayClipAtPoint(powerUpSound,Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
