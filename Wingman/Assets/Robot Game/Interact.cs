using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractType
{
    talk = 0,
    pickFruit = 1
}

public interface IInteract 
{
    void BeginInteract(int actionType);
    void EndInteract();
}
