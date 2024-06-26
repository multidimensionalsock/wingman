using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody _rigidbody;
    Animator _animator;
    PlayerInput _input;
    Vector3 _movementDirection;
    Coroutine _movementCoroutine;
    bool grounded = true;
    bool isSprinting = false;
    int currentJumpCount;
    [SerializeField] float jumpForce;
    [SerializeField] int numberOfJumps = 1;
    [SerializeField] float movementSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] Transform cameraTranform;
    private void OnEnable()
    {
        _input = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _input.currentActionMap.FindAction("Move").performed += StartMove;
        _input.currentActionMap.FindAction("Move").canceled += EndMove;
        _input.currentActionMap.FindAction("Jump").performed += Jump;
        _input.currentActionMap.FindAction("Sprint").performed += Sprint;
        _input.currentActionMap.FindAction("Sprint").canceled += StopSprint;


    }

    void StartMove(InputAction.CallbackContext context)
    {
        Vector2 temp = context.ReadValue<Vector2>();
        _movementDirection = new Vector3(temp.x, 0f, temp.y);
        if (_movementCoroutine != null) return;
        _movementCoroutine = StartCoroutine(Move());
        
    }
   
    void EndMove(InputAction.CallbackContext context) 
    {
        _movementDirection = Vector3.zero;
        
        StopCoroutine(_movementCoroutine);
        _movementCoroutine = null;
    }

    IEnumerator Move()
    {
        GameObject cameraLook = transform.GetChild(0).gameObject;
        GameObject model = transform.GetChild(1).gameObject;
        float tempSpeed;
        //_animator.SetBool("isWalking?", false);
        while (_movementDirection != Vector3.zero)
        {
            //if (m_movementLock)
            //{
            //    yield return new WaitForFixedUpdate();
            //    continue;
            //}
            _animator.SetBool("isWalking?", true);
            if (isSprinting)
            {
                tempSpeed = sprintSpeed;
            }
            else
            {
                tempSpeed = movementSpeed;
            }
             Vector3 prepos = new Vector3(model.transform.position.x,-90, transform.position.z);
            transform.position += cameraTranform.forward * _movementDirection.z * tempSpeed * Time.fixedDeltaTime;
            transform.position += cameraTranform.right * _movementDirection.x * tempSpeed * Time.fixedDeltaTime;
            Vector3 newpos = new Vector3(model.transform.position.x, -90, transform.position.z);
            model.transform.rotation = Quaternion.LookRotation(newpos - prepos, Vector3.up);

            //Quaternion targetRot = cameraLook.transform.rotation * Quaternion.LookRotation(_movementDirection, Vector3.up);
           // model.transform.rotation = Quaternion.Slerp(model.transform.rotation, targetRot, rotationSpeed);

            yield return new WaitForFixedUpdate();
        }
    }

    void Jump(InputAction.CallbackContext context)
    {
        if (currentJumpCount < numberOfJumps)
        {
            _rigidbody.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
            currentJumpCount++;
            _animator.SetBool("isJumping?", true);
        }
    }
    void Sprint(InputAction.CallbackContext context)
    {
        isSprinting = true;
        _animator.SetBool("isSprinting?", true);
    }
    void StopSprint(InputAction.CallbackContext context)
    {
        isSprinting = false;
        _animator.SetBool("isSprinting?", false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            _animator.SetBool("isJumping?", false);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }
    private void FixedUpdate()
    {
        if (grounded)
        {
            currentJumpCount = 0;
        }
    }
}
