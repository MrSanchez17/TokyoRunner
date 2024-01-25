using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    Animator anim;
    Rigidbody rb;
    public GameObject Sounds;


    [SerializeField] float jumping;
    [SerializeField] float speed;
    [SerializeField] float cooldDownTumble;
    [SerializeField] int maxJump;

    private AudioSource jumpSound;
    int currentJump;


    float InitialColdDownTumble;

    bool canJump = false;
    bool LookingFrontRight = true;
    bool mustRotate = false;
    public bool isRunning = false;

    //bool isJumping = false;



    enum DoStates
    {
        Jump,
        Rotation
    }

    DoStates playerStates = DoStates.Jump;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        InitialColdDownTumble = cooldDownTumble;
        currentJump = 0;
        jumpSound = Sounds.transform.Find("Jump").GetComponent<AudioSource>();

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isRunning = true;

        }
        if (Input.GetMouseButtonDown(0) && playerStates == DoStates.Rotation)
        {
            mustRotate = true;
            Movement();
        }
        if (Input.GetMouseButtonDown(0) && playerStates == DoStates.Jump && canJump)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Space) && cooldDownTumble <= 0 ) 
        {
            Tumble();
            cooldDownTumble = InitialColdDownTumble;

        }

        if(isRunning)
        {
            PlayerRunning();
        }

        ChangeAnimations();
        CooldDownIsTumble(); 

    }


    public void PlayerRunning()
    {

            float MovePlayer = speed * Time.deltaTime;
            transform.Translate(Vector3.forward * MovePlayer); // Translate: que siga de manera continua la direccion y a si no se vaya tepeando
    }

    public void Jump()
    {
        if (maxJump > currentJump)
        {
            anim.SetTrigger("Jump");
            jumpSound.Play();
            rb.AddForce(Vector3.up * jumping, ForceMode.VelocityChange);
            currentJump++;
            GameManager.UpdatewhenPlayerPassCube(1);
        }
    }

    public void Movement()
    {
        if (mustRotate)
        {
            mustRotate = false;

            if (LookingFrontRight)
            {
                LookingFrontRight = false;
                Quaternion deltaRotation = Quaternion.Euler(0, 90, 0);
                rb.MoveRotation(rb.rotation * deltaRotation);
                transform.position = new Vector3 (Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));
            }
            else
            {
                Quaternion deltaRotation = Quaternion.Euler(0, -90, 0);
                rb.MoveRotation(rb.rotation * deltaRotation);
                transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));
                LookingFrontRight = true;
            }
        }
        GameManager.UpdatewhenPlayerPassCube(1);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jump"))
        {
            playerStates = DoStates.Jump;
            canJump = true; 
            currentJump = 0;
        }

        if (other.gameObject.CompareTag("Rotation"))
        {
            playerStates = DoStates.Rotation;            
        }
    }
    public void Tumble()
    {
        anim.SetTrigger("Tumble");
        rb.AddForce(Vector3.forward * 1, ForceMode.VelocityChange);
    }
    public void CooldDownIsTumble()
    {
        if (cooldDownTumble > 0)
        {
            cooldDownTumble -= Time.deltaTime;
        }
    }

    public void ChangeAnimations()
    {
        anim.SetBool("Running",isRunning);
    }
}