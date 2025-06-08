using SimpleFPS;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Teleport target;

    [HideInInspector] public bool IsReceive;

    void OnTriggerEnter(Collider other)
    {
        if (IsReceive == true) return;

        FirstPersonController fps = other.GetComponent<FirstPersonController>();

        if (fps != null)
        {
            target.IsReceive = true;

            fps.transform.position = target.transform.position;
        }
    }

        void OnTriggerExit(Collider other)
    {
        FirstPersonController fps = other.GetComponent<FirstPersonController>();

        if (fps != null)
        {
            IsReceive = false;
        }
    }
}
