using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IInteract
{
    Rigidbody _rigidbody;
    PlayerInput _input;
    Vector2 _movementDirection;
    Coroutine _movementCoroutine;
    bool movementLocked = false;
    [SerializeField] float movementForce;
    [SerializeField] float maxSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float jumpForce;

    private void OnEnable()
    {
        _input = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
        _movementCoroutine = null;
        _movementDirection = Vector2.zero;

        _input.currentActionMap.FindAction("Move").performed += StartMove;
        _input.currentActionMap.FindAction("Move").canceled += EndMove;
        _input.currentActionMap.FindAction("Jump").performed += Jump;
        _input.currentActionMap.FindAction("Interact").performed += Interact;
    }

    void StartMove(InputAction.CallbackContext context) 
    {
        if (movementLocked) return;
        _movementDirection = context.ReadValue<Vector2>();
        if (_movementCoroutine != null) return;
        _movementCoroutine = StartCoroutine(Move());
    }
    void EndMove(InputAction.CallbackContext context) 
    {
        _movementDirection = Vector2.zero;
        StopCoroutine(_movementCoroutine);
        _movementCoroutine = null;
    }

    IEnumerator Move() 
    {
        Debug.Log("running co");
        GameObject cameraLook = transform.GetChild(0).gameObject;
        GameObject model = transform.GetChild(1).gameObject;

        while (_movementDirection != Vector2.zero && !movementLocked)
        {
            Debug.Log("running while");
            _rigidbody.AddForce(cameraLook.transform.forward * _movementDirection.y * movementForce);
            _rigidbody.AddForce(cameraLook.transform.right * _movementDirection.x * movementForce);
            _rigidbody.velocity = Vector3.ClampMagnitude(Vector3.ProjectOnPlane(_rigidbody.velocity, Vector3.up), maxSpeed) + (Vector3.up * _rigidbody.velocity.y);

            Quaternion targetRot = cameraLook.transform.rotation * Quaternion.LookRotation(new Vector3(_movementDirection.x, 0, _movementDirection.y), Vector3.up);
            model.transform.rotation = Quaternion.Slerp(model.transform.rotation, targetRot, rotationSpeed);

            yield return new WaitForFixedUpdate();
        }
    }

    void Jump(InputAction.CallbackContext context) 
    {
        
        _rigidbody.AddForce(new Vector3(0f, jumpForce - _rigidbody.velocity.y, 0f), ForceMode.Impulse);
            
    }

    void Interact(InputAction.CallbackContext context) { }

    public void BeginInteract(int actionType) { movementLocked = true; }
    public void EndInteract() { movementLocked = false; }



}
