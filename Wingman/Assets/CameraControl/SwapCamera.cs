using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Areas
{
    Island1Cliffs
}


public class SwapCamera : MonoBehaviour
{
    [SerializeField] Areas cameraArea;

    public static System.Action<Areas, bool> CameraSwap;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        CameraSwap?.Invoke(cameraArea, true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player") return;
        CameraSwap?.Invoke(cameraArea, false);
    }
}

