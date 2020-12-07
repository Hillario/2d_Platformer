using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Character : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    private Animator anim;                    //the animation conroller component

    [SerializeField]
    private float movementSpeed = 6f;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;        //radius to indicate how close the player needs be in the ground

    [SerializeField]
    private LayerMask whatIsGround;   //determine what ground the character should stand on

    [SerializeField]
    private float jumpForce;

    private bool jump;               //this will refer to the isRun Parameter

    [SerializeField]
    private bool isGrounded;

    private bool isdead = false;      //check if character is alive

    private bool doubleJump;

    public static float distanceTravelled;

    public float horizontal = 1f;

    private bool onTouch = false;

    //public AudioClip runClip;
    //public AudioClip jumpClip;

    private AudioSource myaudio;
    public GameObject camRun;
    private AudioSource camListener;

    // Start is called before the first frame update
    void Start()
    {
        myaudio = GetComponent<AudioSource>();
        camListener = camRun.GetComponent<AudioSource>();
        myRigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        HandleInputs();
        onTouch = false;
        if (isGrounded)
        {
            doubleJump = false;
            camListener.enabled = true;
        }
        else if (!isGrounded)
        {
            camListener.enabled = false;
        }
        isGrounded = isGroundedMethod();
        HandleMovement(horizontal);

       
        anim.SetBool("IdleToRun", true);
    }

    private void HandleMovement(float horizontal)
    {
        
        myRigidBody.velocity = new Vector2(horizontal * movementSpeed, myRigidBody.velocity.y);

        //handle the jumps
        //anim.SetFloat("VSpeed", myRigidBody.velocity.y);
        anim.SetBool("runJump", isGrounded);
    }

    //Coroutine
    IEnumerator StartRun()
    {
        yield return new WaitForSeconds(3);
        //start run
        
    }

    private void HandleInputs()
    {
        if((isGrounded || !doubleJump) && Input.GetButtonDown("Jump"))
        {
            myRigidBody.AddForce(new Vector2(0f, jumpForce));

            if(!doubleJump && !isGrounded)
            {
                doubleJump = true;
            }
        }
    }

    private bool isGroundedMethod()
    {
        if (myRigidBody.velocity.y <= 0)
        {
            foreach(Transform point in groundPoints)
            {
                //get all colliders in contact with the player
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for(int i=0; i < colliders.Length; i++)
                {
                    //if colliders in player is not equivalent to attached, then it's grounded
                    if(colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnDeath()
    {

    }

    public void detectTouch()
    {

    }

    IEnumerator Load_Scene()
    {
        yield return new WaitForSeconds(3);
    }
}
