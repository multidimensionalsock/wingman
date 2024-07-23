using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerAnimator : MonoBehaviour, IInteract
{
    PlayerInput _input;
    bool _movementLock;
    Animator _animator;
    private void OnEnable()
    {
        _input = GetComponent<PlayerInput>();
        _animator = GetComponentInChildren<Animator>();
        _input.currentActionMap.FindAction("Move").performed += StartMove;
        _input.currentActionMap.FindAction("Move").canceled += EndMove;
        _input.currentActionMap.FindAction("Jump").performed += Jump;
    }

    void StartMove(InputAction.CallbackContext context)
    {
        if (_movementLock) return;
        _animator.SetBool("Moving", true);
    }

    void EndMove(InputAction.CallbackContext context)
    {
        _animator.SetBool("Moving", false); 
    }

    void Jump(InputAction.CallbackContext context) 
    {
        if (_movementLock) return;
        _animator.SetTrigger("Jump");
    }

    public void BeginInteract(int actionType) 
    { 
        _movementLock = true;
        _animator.SetInteger("InteractionType", actionType);
        _animator.SetBool("Interacting", true);
    }
    public void EndInteract() 
    { 
        _movementLock = false;
        _animator.SetBool("Interacting",  false);
    }
}
