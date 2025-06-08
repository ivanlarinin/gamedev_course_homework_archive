using UnityEngine;
using SimpleFPS;

public class Acceleration : MonoBehaviour
{
    [SerializeField] private int bonus;
    void OnTriggerEnter(Collider other)
    {
        FirstPersonController fps = other.GetComponent<FirstPersonController>();

        if (fps != null)
        {
            fps.m_JumpSpeed += bonus;
            fps.m_WalkSpeed += bonus;
            fps.m_RunSpeed += bonus;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        FirstPersonController fps = other.GetComponent<FirstPersonController>();

        if (fps != null)
        {
            fps.m_JumpSpeed -= bonus;
            fps.m_WalkSpeed -= bonus;
            fps.m_RunSpeed  -= bonus;
        }
    }
}
