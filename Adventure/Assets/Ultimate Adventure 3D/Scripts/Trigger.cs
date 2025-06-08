using SimpleFPS;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{

    public UnityEvent Enter;
    public UnityEvent Exit;
    
    void OnTriggerEnter(Collider other)
    {
        FirstPersonController fps = other.GetComponent<FirstPersonController>();


        if (fps != null)
        {
            Enter.Invoke();
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        FirstPersonController fps = other.GetComponent<FirstPersonController>();

        if (fps != null)
        {
            Exit.Invoke();
        }
    }
}
