using UnityEngine;
using SimpleFPS;

public class SpringPlatform : MonoBehaviour
{
    [SerializeField] private int JumpForce;
    [SerializeField] private new AudioSource audio;
    private float previousJumpForce;
    void OnTriggerEnter(Collider other)
    {
        FirstPersonController fps = other.GetComponent<FirstPersonController>();

        if (fps != null)
        {
            previousJumpForce = fps.m_JumpSpeed;
            fps.m_JumpSpeed += JumpForce;
            fps.m_Jump = true;

            audio.Play();
        }
    }
    
        void OnTriggerExit(Collider other)
    {
        FirstPersonController fps = other.GetComponent<FirstPersonController>();

        if (fps != null)
        {
            fps.m_JumpSpeed = previousJumpForce;
        }
    }
}
