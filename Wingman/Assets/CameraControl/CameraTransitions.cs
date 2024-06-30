using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraTransitions : MonoBehaviour
{
    Animator animator;
    static CameraTransitions instance;
    int currentCamera = 0;
    
    
    void Start()
    {
        if (instance != null) { Destroy(gameObject); }
        DontDestroyOnLoad(gameObject);
        animator = GetComponent<Animator>();
        SwapCamera.CameraSwap += SetCameraArea;
    }

    void SetCameraArea(Areas camera, bool state)
    {
        switch(camera)
        {
            case Areas.Island1Cliffs:
                animator.SetBool("Island1Cliffs", state);
                break;
        }
        
    }
}
