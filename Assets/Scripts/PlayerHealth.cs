using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    Animator anim;
    Collider playerCollider;
    Player playerController;
    ArmorBoost armor;
    [SerializeField] GameObject MenuGameOver;
    [SerializeField] GameObject ArmorImg;


    public int maxHealth = 8;
    public int currentHealth;
    public int shield;

    

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        playerCollider = GetComponentInChildren<Collider>();
        playerController = GetComponent<Player>();
        armor = GetComponent<ArmorBoost>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        shield = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") || other.CompareTag("Enemy"))
        {
            if (shield > 0)
            {
                DamageShield(1);
                Debug.Log("Sin Armadura");
            }
            else
            {
                TakeDamage(1);
                Debug.Log("Ha muerto");
            }

        }

        if (other.CompareTag("ArmorBoost")) 
        {
            GiveShield(1);
            Debug.Log("Da escudo");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void GiveShield(int givingShield) 
    {
        ArmorImg.SetActive(true);
        shield += givingShield;
        StartCoroutine(DesactivedShield());
    }

    private void DamageShield(int damage) 
    {
        shield -= damage;
        if (shield == 0)
        {
            ArmorImg.SetActive(false);
            shield = 0;
        }
    }

    private IEnumerator DesactivedShield()
    {
        yield return new WaitForSeconds(5f);
        shield = 0;
        ArmorImg.SetActive(false);
    }


    private void Die()
    {
        playerController.isRunning = false;
        anim.SetTrigger("Died");
        StartCoroutine(DeactivateAfterAnimation());

    }

    private IEnumerator DeactivateAfterAnimation()
    {
        // Espera el tiempo necesario para que la animación de muerte se reproduzca completamente
        yield return new WaitForSeconds(2f);
        playerCollider.enabled = false;
        gameObject.SetActive(false);
        MenuGameOver.SetActive(true);
    }


}
