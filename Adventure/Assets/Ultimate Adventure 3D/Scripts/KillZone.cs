using System;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    public int damage;
    private void OnTriggerEnter(Collider other)
    {
        Destructable destructable = other.GetComponent<Destructable>();
        if (destructable != null)
        {
            destructable.Kill();
        }
    }
}
