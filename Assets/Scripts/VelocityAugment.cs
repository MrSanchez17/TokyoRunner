using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityAugment : MonoBehaviour
{
    [SerializeField] public float aumentoDeVelocidad = 0.1f;
    [SerializeField] public float velocidadInicial;  
    
    public float maximaVelocidad = 20f;
    float distanciaRecorrida;
    float nuevaVelocidad;

    private void Start()
    {
        velocidadInicial = 1f;
    }

    private void Update()
    {
         distanciaRecorrida = transform.position.z;

        nuevaVelocidad = velocidadInicial + (distanciaRecorrida * aumentoDeVelocidad);

        // Limitar la velocidad máxima
        nuevaVelocidad = Mathf.Min(nuevaVelocidad, maximaVelocidad);

        Time.timeScale = nuevaVelocidad;
    }
}