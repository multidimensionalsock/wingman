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

    void SetCameraArea(int camera)
    {
        //transform.GetChild(currentCamera).gameObject.active = false;
        transform.GetChild(camera).gameObject.SetActive(true);
        animator.SetInteger("CameraPhase", camera);
        currentCamera = camera;
        
    }
}
