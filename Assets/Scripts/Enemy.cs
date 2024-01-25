using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumping;
    [SerializeField] bool playerDied = false;

    Rigidbody rb;
    Animator anim;
    PlayerHealth healthPlayer;


    bool LookingFrontRightEnemy = true;
    //bool isEnemyRunning;
    public bool enemyAnimationRunning;
    float rotationPositionZ;
    private float rotationPositionX;

    enum DoStatesEnemy
    {
        Run,
        Rotation
    }

    DoStatesEnemy EnemyStates = DoStatesEnemy.Run;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        healthPlayer = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            enemyAnimationRunning = true;
        }
        else if(EnemyStates == DoStatesEnemy.Rotation && transform.position.z >= rotationPositionZ) 
        {  
            EnemyRotation();
            EnemyStates = DoStatesEnemy.Run;
            
        }
        else if (EnemyStates == DoStatesEnemy.Rotation && transform.position.x >= rotationPositionX)
        {
            EnemyRotation();
            EnemyStates = DoStatesEnemy.Run;
        }
        
        if (enemyAnimationRunning)
        {
            EnemyRunning();
        }
        else
        {
            enemyAnimationRunning=false;
        }



        ChangeAnimationsEnemy();

    }
    public void EnemyJump()
    {
        anim.SetTrigger("enemyJump");
        rb.AddForce(Vector3.up * jumping, ForceMode.VelocityChange);
        //rb.AddForce(Vector3.forward * 1, ForceMode.VelocityChange);
    }

    void EnemyRunning()
    {
        float MoveEnemy  = speed * Time.deltaTime;
        transform.Translate(Vector3.forward * MoveEnemy);
    }
    void EnemyRotation()
    {
        if (LookingFrontRightEnemy)
        {
            LookingFrontRightEnemy = false;
            Quaternion deltaRotation = Quaternion.Euler(0, 90, 0);
            rb.MoveRotation(rb.rotation * deltaRotation);
            //transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));
        }
        else
        {
            Quaternion deltaRotation = Quaternion.Euler(0, -90, 0);
            rb.MoveRotation(rb.rotation * deltaRotation);
            //transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z)); // lo que hace el Matf es que te redondea los numero sin que pase por los decimales y eso deja en el medio el personaje
            LookingFrontRightEnemy = true;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jump"))
        {
            EnemyJump();
        }

        else if (other.gameObject.CompareTag("Rotation"))
        {
            rotationPositionZ = other.transform.position.z;
            rotationPositionX = other.transform.position.x;
            Debug.Log("rota");
            EnemyStates = DoStatesEnemy.Rotation;
        }

        else  if (other.gameObject.CompareTag("Ground"))
        {
            EnemyStates = DoStatesEnemy.Run;
            EnemyRunning();
        }

        else if(other.gameObject.CompareTag("Player"))
        {
            playerDied = true;
        }
    }

    public void ChangeAnimationsEnemy()
    {
        anim.SetBool("enemyRunning", enemyAnimationRunning);

        if (playerDied)
        {
            enemyAnimationRunning = false;
            anim.SetTrigger("enemyDancing");
        }
    }

}
