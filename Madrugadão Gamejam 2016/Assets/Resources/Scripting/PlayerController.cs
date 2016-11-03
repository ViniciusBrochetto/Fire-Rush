using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float m_Speed;
    [SerializeField]
    private float m_JumpSpeed;

    [Header("Temperature")]
    [SerializeField]
    private float m_Temperature;
    [SerializeField]
    private float m_MaxTemperature;

    [Header("Branches")]
    [SerializeField]
    private int m_BranchesCarrying;
    [SerializeField]
    private int m_MaxBranchesCarrying;

    [Header("Water")]
    [SerializeField]
    private bool m_IsCarryingBucket;
    [SerializeField]
    private float m_WaterCarrying;

    [Header("Actions")]
    [SerializeField]
    private bool canJump;
    [SerializeField]
    private bool canInteract;

    private Vector3 moveDirection;
    private Rigidbody m_Rigidbody;

    private IInteractable m_CurrInteractable;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleInputs();
    }

    void FixedUpdate()
    {
        if (moveDirection.magnitude != 0)
            Move();
    }

    private void HandleInputs()
    {
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.z = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump") && canJump)
            Jump();

        if (Input.GetButtonDown("Interact") && canInteract)
            Interact();
    }

    private void Move()
    {
        m_Rigidbody.MovePosition(transform.position + moveDirection * Time.fixedDeltaTime * m_Speed);
    }

    private void Jump()
    {
        m_Rigidbody.AddForce(Vector3.up * m_JumpSpeed);
    }

    private void Interact()
    {
        if (m_CurrInteractable != null)
        {
            InteractType type = m_CurrInteractable.GetType();
            switch (type)
            {
                case InteractType.FirePlace:
                    if (m_BranchesCarrying > 0)
                    {
                        if (m_CurrInteractable.Interact())
                            m_BranchesCarrying--;
                    }
                    break;
                case InteractType.Tree:
                    if (m_BranchesCarrying < m_MaxBranchesCarrying)
                    {
                        if (m_CurrInteractable.Interact())
                            m_BranchesCarrying++;
                    }
                    break;
                case InteractType.River:
                    if (m_IsCarryingBucket)
                    {
                        if (m_CurrInteractable.Interact())
                            m_WaterCarrying += 0.1f;
                    }
                    break;
                case InteractType.Invalid:
                    break;
                default:
                    break;
            }
        }
        else
        {
            if (m_IsCarryingBucket) { }
            //TODO - Drop Bucket
            else if (m_BranchesCarrying > 0)
            {
                m_BranchesCarrying = 0;
            }
        }
    }

    public void setInteractable(IInteractable i)
    {
        m_CurrInteractable = i;
    }
}
