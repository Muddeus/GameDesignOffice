using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    private Camera targetCamera;
    [Tooltip("The name of the target camera")]
    [SerializeField] string cameraName;
    private void Start()
    {
        targetCamera = GameObject.Find(cameraName)?.GetComponent<Camera>();
    }
    private void Update()
    {
        transform.LookAt(targetCamera.transform);
    }
}
