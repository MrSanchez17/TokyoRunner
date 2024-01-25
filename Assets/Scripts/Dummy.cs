using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Dummy : MonoBehaviour
{
    [SerializeField] float duration = 1f;
    [SerializeField] bool rotateClockwise = true;
    [SerializeField] Ease ease = Ease.Linear;
    void Start()
    {
        transform.DORotate(Vector3.forward * 360f * (rotateClockwise ? -1f : 1f), duration, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
    }
}
