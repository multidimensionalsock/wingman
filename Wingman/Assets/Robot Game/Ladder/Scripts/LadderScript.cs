using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScript : MonoBehaviour
{
    bool _playerAttached = false;
    Transform playerTransform;
    [SerializeField] float climbSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.tag == "Player")
            {
                _playerAttached = true;
                playerTransform = other.gameObject.transform;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.tag == "Player")
            {
                _playerAttached = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerAttached)
        {
            
            
        }
    }
}
