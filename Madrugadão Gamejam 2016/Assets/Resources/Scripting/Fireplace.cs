using UnityEngine;
using System.Collections;

public class Fireplace : MonoBehaviour
{
    [SerializeField]
    private float m_MaxFuel;
    [SerializeField]
    private float m_Fuel;
    [SerializeField]
    private float m_FuelConsumption;

    private Renderer m_Renderer;

    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        m_Fuel -= Time.deltaTime * m_FuelConsumption;
        m_Renderer.material.color = Color.Lerp(Color.blue, Color.red, m_Fuel / m_MaxFuel);
    }

    public bool Interact()
    {
        bool isValid = false;

        if (m_Fuel > 0)
        {
            m_Fuel--;
            isValid = true;
        }

        if (m_Fuel == 0f)
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
