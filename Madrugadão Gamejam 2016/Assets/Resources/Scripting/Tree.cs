using UnityEngine;
using System.Collections;
using System;

public class Tree : MonoBehaviour, IInteractable
{
    [SerializeField]
    private int m_MaxBranches;
    [SerializeField]
    private int m_Branches;

    private Renderer m_Renderer;

    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
    }

    public bool Interact()
    {
        bool isValid = false;

        if (m_Branches > 0)
        {
            m_Branches--;
            isValid = true;
        }

        if (m_Branches == 0f)
            m_Renderer.material.color = Color.red;

        Debug.Log("Interacted with: " + transform.name + (isValid ? ". And got a branch!" : ". But the Tree is empty!"));

        return isValid;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<PlayerController>().setInteractable(this);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<PlayerController>().setInteractable(null);
    }

    InteractType IInteractable.GetType()
    {
        return InteractType.Tree;
    }
}
