using UnityEngine;
using System.Collections;

public interface IInteractable
{
    InteractType GetType();
    bool Interact();
}

public enum InteractType
{
    Bonfire,
    Tree,
    River,
    Invalid
}
