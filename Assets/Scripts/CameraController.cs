using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera mainCam;
    [SerializeField] private float buffer = 1f;
    private (Vector3, float) valueTuple;

    private void Start()
    {
        
    }

    private void Awake()
    {
        mainCam = Camera.main;
        mainCam.transform.position = new Vector3(0f, 0f, -10f);
    }
    
    
    void Update()
    {
        valueTuple = CameraHelper.CalculateOrthoSize(mainCam, buffer);
        (Vector3 center, float size) = valueTuple;
        mainCam.orthographicSize = size; // Set the camera's orthographic size
        mainCam.transform.position = center; // Set the camera's position
    }
    
}
