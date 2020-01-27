using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public float angle = 90.0f;
    public float slowly = 5.0f;
    public float xThrow;
    public float yThrow;
    float lifeTimeInSeconds = 5.0f;

    Rigidbody2D rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        xThrow = Random.Range(-5.0f, 5.0f);
        yThrow = Random.Range(8.0f, 10.0f);
        rigidbody.velocity = new Vector2(xThrow, yThrow);
        StartCoroutine(DestroyTimer());
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(lifeTimeInSeconds);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        gameObject.transform.Rotate(0, 0, angle/slowly);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Ball")
    //    {
    //        var circleCollider = gameObject.GetComponent<CircleCollider2D>();
    //        circleCollider.isTrigger = true;

    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    Debug.Log("out");
    //}

    //private void CountUpDestroy()
    //{
    //    timesHit++;
    //    if (timesHit > 3)
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
