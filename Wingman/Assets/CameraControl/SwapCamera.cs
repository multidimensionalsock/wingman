using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraDirections
{ 
    North = 0,
    NorthNorthEast = 1,
    NorthEast = 2,
    EastNorthEast = 3,
    East = 4,
    EastSouthEast = 5,
    SouthEast = 6,
    SouthSouthEast = 7,
    South = 8,
    SouthSouthWest = 9,
    SouthWest = 10,
    WestSouthWest = 11,
    West = 12,
    WestNorthWest = 13,
    NorthWest = 14,
    NorthNorthWest = 15,
}


public class SwapCamera : MonoBehaviour
{
    [SerializeField] CameraDirections cameraDirection;

    public static System.Action<int> CameraSwap;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        CameraSwap?.Invoke((int)cameraDirection);
    }
}

