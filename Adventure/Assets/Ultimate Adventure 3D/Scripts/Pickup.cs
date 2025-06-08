using UnityEngine;
using SimpleFPS;

public class Pickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        HandleTriggerEnter(other);
    }

    protected virtual void HandleTriggerEnter(Collider other)
    {
        FirstPersonController fps = other.GetComponent<FirstPersonController>();

        if (fps != null)
        {
            Destroy(gameObject);
        }
    }
}