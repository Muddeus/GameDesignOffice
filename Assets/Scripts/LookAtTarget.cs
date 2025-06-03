using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public Transform lookAtTargetTransform;
    private void Update()
    {
        transform.LookAt(lookAtTargetTransform);
    }
}
