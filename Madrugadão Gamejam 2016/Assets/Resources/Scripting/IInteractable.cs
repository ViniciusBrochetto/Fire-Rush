using UnityEngine;
using System.Collections;

public interface IInteractable
{
    InteractType GetType();
    bool Interact();
}

public enum InteractType
{
    FirePlace,
    Tree,
    River,
    Invalid
}
