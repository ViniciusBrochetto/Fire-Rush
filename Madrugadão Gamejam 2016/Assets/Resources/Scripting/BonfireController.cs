using UnityEngine;
using System.Collections;
using System;

public class BonfireController : MonoBehaviour, IInteractable
{
    [SerializeField]
    private float m_MaxFuelBeforeSpread;
    [SerializeField]
    private float m_Fuel;
    [SerializeField]
    private float m_FuelConsumption;
    [SerializeField]
    private float m_FuelPerBranch;

    private Renderer m_Renderer;

    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        //Reduce fire per frame (with minumum of 0f)
        m_Fuel = Mathf.Max(0f, m_Fuel - (Time.deltaTime * m_FuelConsumption));

        //Update the color based on fire strength (Max color when its dangerous)
        m_Renderer.material.color = Color.Lerp(Color.blue, Color.red, Mathf.Min(m_Fuel / m_MaxFuelBeforeSpread, 1f));

        //When fuel reaches 0 there's consequences
        if (m_Fuel == 0f)
        {
            //TODO - People Die
        }

        //When fuel surpasses MaxFuelBeforeSpread there's also consequences
        if (m_Fuel > m_MaxFuelBeforeSpread)
        {
            //TODO - People Die
        }
    }

    public bool Interact()
    {
        //Alway let the fire get hotter
        m_Fuel += m_FuelPerBranch;

        Debug.Log("Interacted with: " + transform.name + ". The fire burns brighter!");

        return true;
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
        return InteractType.Bonfire;
    }
}
