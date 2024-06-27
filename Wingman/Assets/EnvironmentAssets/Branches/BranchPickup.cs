using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchPickup : MonoBehaviour
{
    public static System.Action BranchCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;

        BranchCollected?.Invoke();
        Destroy(gameObject);
    }
}
