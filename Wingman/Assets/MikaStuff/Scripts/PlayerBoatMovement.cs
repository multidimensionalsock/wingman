using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerBoatMovement : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerInput _input;
    Rigidbody _rigidbody;
    Vector3 _movementDirection;
    float _movementDirectionX;
    float _movementDirectionY;
    
    Coroutine _movementCoroutine;
    [SerializeField] float movementSpeed;
    [SerializeField] float reverseSpeed;

    [SerializeField] float turningSpeed;
    /*[SerializeField] Transform cameraTranform;*/
    [SerializeField] GameObject pivotPoint;

    void Start()
    {
        
    }
    private void OnEnable()
    {
        _input = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
        _input.currentActionMap.FindAction("Move").performed += StartMove;
        _input.currentActionMap.FindAction("Move").canceled += EndMove;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void StartMove(InputAction.CallbackContext context)
    {
        Vector2 temp = context.ReadValue<Vector2>();
        _movementDirectionX = temp.x;
        _movementDirectionY = temp.y;
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
        GameObject model = transform.GetChild(0).gameObject;
        float tempSpeed = movementSpeed;
        while (_movementDirection != Vector3.zero)
        {
            if (_movementDirectionY > 0) { _rigidbody.AddForce(transform.forward * movementSpeed * _movementDirectionY); }
            else { _rigidbody.AddForce(transform.forward * reverseSpeed * _movementDirectionY); }

            transform.Rotate(Vector3.up,turningSpeed * _movementDirectionX);
            
            yield return new WaitForFixedUpdate();
        }
    }
}
