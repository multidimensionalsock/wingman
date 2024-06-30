using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDollyPosition : MonoBehaviour
{
    CinemachineDollyCart cart;
    [SerializeField] GameObject ToFollow;
    // Start is called before the first frame update
    void Start()
    {
        cart = GetComponent<CinemachineDollyCart>();
    }

    // Update is called once per frame
    void Update()
    {
        cart.m_Position = cart.m_Path.FindClosestPoint(ToFollow.transform.position, 0, -1, 10);
    }
}
