using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorBoost : MonoBehaviour
{
    public float duracionBoost = 15f;

    public void ActiveBoostDeArmadura()
    {   
        //gameObject.SetActive(false);
        StartCoroutine(DesactivarBoostDeArmadura());
    }

    private IEnumerator DesactivarBoostDeArmadura()
    {
        yield return duracionBoost;
    }
}
