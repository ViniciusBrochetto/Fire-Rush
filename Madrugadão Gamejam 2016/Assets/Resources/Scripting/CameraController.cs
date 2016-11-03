using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform m_Target;

    [SerializeField]
    private float m_Smoothing;

    [SerializeField]
    private float m_Distance;

    void Start()
    {
        if (!m_Target)
            m_Target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, m_Target.position, Time.fixedDeltaTime * m_Smoothing);
    }
}
