using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowVelocity : MonoBehaviour
{

    [SerializeField] float NewInicialVelocity = 1f;

    [SerializeField] public float aumentoDeVelocidad = 0.1f;

    public float ActualVelocity;
    float distanciaRecorrida;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            RestablecerVelocidadTiempo();
        }
    }

    private void Update()
    {
        distanciaRecorrida = transform.position.z;
        NewInicialVelocity = ActualVelocity + (distanciaRecorrida * aumentoDeVelocidad);
    }
    public void RestablecerVelocidadTiempo()
    {
        NewInicialVelocity = ActualVelocity;
    }

}