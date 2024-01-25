using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] GameObject FolloweCamera;

    private void Update()
    {
        transform.position = FolloweCamera.transform.position;
    }
}
