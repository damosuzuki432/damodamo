using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

/*
This is attached to "Ball", which you hit to break the blocks.
Main functions are:
 1. refer to paddle, and stick ball to the paddle before starting the game.
 2. refert to audio, and play the ball collides something.
    NOTE that the sound is not attached to paddle.
    this is because, it is the BALL that make noise. the ball hits many different things,
    so it is not a good idea if you make noise by only paddle.
 3. release the first shot.
 4. make the ball speed constant.
 5. add random factor to the ball movement.
 6. manage tweek of right/left edge hit.

*/
    //config params
    [SerializeField] paddle paddle1; //paddle referenc
    [SerializeField] AudioClip[] audioClips; //audio reference
    [SerializeField] float xThrow = 2f; //tweek for first shot vector x
    [SerializeField] float yThrow = 15f; // tweek for first shot vector y
    [SerializeField] float randomFactor = 0.2f; // tweek for randomness of bounce
    [SerializeField] float left = -2.0f; //tweek for left edge NOTE right is negative of this
    [SerializeField] float ballConstSpeed = 8;
   
    //relationship between ball and paddle
    Vector2 ballPos; // position of the ball
    Vector2 paddleToBallVector; // distance between ball and paddle

    //on/off switches
    public bool hasStarted = false; //hasn't started by default
    bool ReleaseEnabler = true; //enabled to release by default

    public enum State {title, Playable, Dying, Clear}
    public State state = State.title;

    //cached component reference
    AudioSource audioSource;
    Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        //first, get component of audi and rigidbody
        audioSource = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody2D>();

        //then, get distance btw ball and paddle
        paddleToBallVector = transform.position - paddle1.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Playable)
        {
            //before start, stick ball to paddle
            if (!hasStarted)
            {
                LockBallToPaddle();
            }

            //before start, player is able to release ball
            if (ReleaseEnabler)
            {
                LaunchOnMouseClick();
            }
        }
    }

    private void LockBallToPaddle()
    {
        //stick ball with paddle
        //x pos of the ball is equal to paddle.
        //y pos of the ball is pos of paddle PLUS the distance you've got at Start method
        ballPos = new Vector2
            (paddle1.transform.position.x,
             paddle1.transform.position.y + paddleToBallVector.y);
        //once defined the ball pos, then replace it by this
        transform.position = ballPos;
    }

    private void LaunchOnMouseClick()
    {
        //start by left click. Note 0 means left click of a mouse button
        if (Input.GetMouseButtonDown(0))
        {
            //turn on hasstarted switch
            hasStarted = true;
            //replace velocity property to move ball upward (this is the first shot)
            rigidbody.velocity = new Vector2(xThrow, yThrow);
            //turn off release enabler so that player no longer can release ball upward
            ReleaseEnabler = false;
        }
    }

    private void FixedUpdate()
    {
        CheckConstantSpeed();
    }

    private void CheckConstantSpeed()
    {
        Vector2 VelocityNormalized = rigidbody.velocity.normalized;
        rigidbody.velocity = VelocityNormalized * ballConstSpeed;
        //TODO is this the right way to use Time.Deltatime?
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //only if hasstarted is ON. Without this, ball make collision noise when started. 
        if (hasStarted)
        {
            //if collide, make random noise 
            MakeRandomNoise();
            VectorManager();
        }
    }

    private void VectorManager()
    {
        //TODO Clean up!!

        //first, generate randomize factor
        Vector2 velocityTweek;

        //then two different ways

        //1. when the ball is going down, push downward(meaning, negative Y)
        if ((rigidbody.velocity.x <= 0 && rigidbody.velocity.y <=0)||
            (rigidbody.velocity.x >= 0 && rigidbody.velocity.y <=0))
        {
            velocityTweek = new Vector2
                    (Random.Range(-randomFactor, randomFactor),
                     Random.Range(randomFactor * -5, 0));
            //also, normalize the velocity and factor by fixed magnitude to make constant ball speed
            //without this, ball speed is inconsistant and the game become boring!
            Vector2 constVelocity = rigidbody.velocity.normalized * ballConstSpeed;
            //Finally, replace velocity by "Fixed Speed" and "a little randomness"
            rigidbody.velocity = constVelocity + velocityTweek;
        }


        //2. when the ball is goind up, push upward(meaning, positive Y)
        if ((rigidbody.velocity.x >= 0 && rigidbody.velocity.y >= 0) ||
           (rigidbody.velocity.x <= 0 && rigidbody.velocity.y >= 0))
        {
            velocityTweek = new Vector2
                    (Random.Range(-randomFactor, randomFactor),
                     Random.Range(0, randomFactor * 5));
            Vector2 constVelocity = rigidbody.velocity.normalized * ballConstSpeed;
            rigidbody.velocity = constVelocity + velocityTweek;
        }
    }

    private void MakeRandomNoise()
    {
        int random = Random.Range(0, audioClips.Length);
        AudioClip clip = audioClips[random];
        audioSource.PlayOneShot(clip);
    }

    //************below is called by PaddleEdgeL/R Method****************


    public void leftTweek()
    {
        Vector2 lefttweek = new Vector2(left, 0);
        rigidbody.velocity += lefttweek;
    }

    public void rightTweek()
    {
        Vector2 righttweek = new Vector2(-left, 0);
        rigidbody.velocity += righttweek;
    }

}
