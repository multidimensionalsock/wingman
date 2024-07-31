using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IInteract
{
    Rigidbody _rigidbody;
    PlayerInput _input;
    Vector2 _movementDirection;
    Coroutine _movementCoroutine;
    Coroutine _cameraCoroutine;
    bool movementLocked = false;
    Vector3 cameraDirection;
    [SerializeField] float movementForce;
    [SerializeField] float maxSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float climbSpeed = 0.01f;
    public bool onLadder = false;
    bool grounded = false;

    private void OnEnable()
    {
        _input = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
        _movementCoroutine = null;
        _cameraCoroutine = null;
        _movementDirection = Vector2.zero;

        _input.currentActionMap.FindAction("Move").performed += StartMove;
        _input.currentActionMap.FindAction("Move").canceled += EndMove;
        _input.currentActionMap.FindAction("Jump").performed += Jump;
        _input.currentActionMap.FindAction("Interact").performed += Interact;
        _input.currentActionMap.FindAction("Camera").performed += CameraStart;
        _input.currentActionMap.FindAction("Camera").canceled += CameraStop;
    }

    void CameraStart(InputAction.CallbackContext context)
    {
        cameraDirection = context.ReadValue<Vector3>() * Time.deltaTime;
        if (_cameraCoroutine != null) { StopCoroutine(_cameraCoroutine); }
        _cameraCoroutine = StartCoroutine(CameraMove());

    }

    void CameraStop(InputAction.CallbackContext context)
    {
        if (_cameraCoroutine != null)
        {
            StopCoroutine(_cameraCoroutine);
        }
        cameraDirection = Vector3.zero;
        
    }

    IEnumerator CameraMove()
    {
        Transform cameraLook = transform.GetChild(0).gameObject.transform;
        while (cameraDirection != Vector3.zero)
        {
            cameraLook.rotation = Quaternion.Slerp(cameraLook.rotation, cameraLook.rotation * Quaternion.LookRotation(cameraDirection), 0.1f / 5f);
            //cameraLook.rotation = cameraLook.rotation * (Quaternion.LookRotation(cameraDirection));
            yield return new WaitForFixedUpdate();
        }
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

    IEnumerator ClimbLadder()
    {
        _rigidbody.useGravity = false;
        while (onLadder)
        {
            if (_movementDirection == Vector2.zero) {  _rigidbody.velocity = Vector3.zero; }
            _rigidbody.velocity = new Vector3(0, _movementDirection.y * climbSpeed, 0);
            yield return new WaitForFixedUpdate();
        }
        _rigidbody.useGravity = true;
        if (_movementDirection != Vector2.zero)
        {
            _movementCoroutine = StartCoroutine(Move());
        }
    }

    IEnumerator Move() 
    {
        Debug.Log("running co");
        GameObject cameraLook = transform.GetChild(0).gameObject;
        GameObject model = transform.GetChild(1).gameObject;

        while (_movementDirection != Vector2.zero && !movementLocked)
        {
            if (!onLadder) { _rigidbody.AddForce(cameraLook.transform.forward * _movementDirection.y * movementForce); }
            _rigidbody.AddForce(cameraLook.transform.right * _movementDirection.x * movementForce);
            _rigidbody.velocity = Vector3.ClampMagnitude(Vector3.ProjectOnPlane(_rigidbody.velocity, Vector3.up), maxSpeed) + (Vector3.up * _rigidbody.velocity.y); 

            Quaternion targetRot = cameraLook.transform.rotation * Quaternion.LookRotation(new Vector3(_movementDirection.x, 0, _movementDirection.y), Vector3.up);
            model.transform.rotation = Quaternion.Slerp(model.transform.rotation, targetRot, rotationSpeed);

            yield return new WaitForFixedUpdate();
        }
        if (onLadder)
        {
            StartCoroutine(ClimbLadder());
        }
    }

    void Jump(InputAction.CallbackContext context) 
    {
        
        _rigidbody.AddForce(new Vector3(0f, jumpForce - _rigidbody.velocity.y, 0f), ForceMode.Impulse);
            
    }

    void Interact(InputAction.CallbackContext context) { }

    public void BeginInteract(int actionType) { movementLocked = true; }
    public void EndInteract() { movementLocked = false; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ladder")
        {
            onLadder = true;
            StartCoroutine(ClimbLadder());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ladder")
        {
            onLadder = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }

}
