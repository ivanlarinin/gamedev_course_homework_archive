using UnityEngine;

public class Key : Pickup
{
    [SerializeField] private GameObject impactEffect;
    protected override void HandleTriggerEnter(Collider other)
    {
        base.HandleTriggerEnter(other);

        Bag bag = other.GetComponent<Bag>();

        if (bag != null)
        {
            bag.AddKey(1);

            // Instantiate the effect at the Key's position
            if (impactEffect != null)
            {
                Instantiate(impactEffect, transform.position, Quaternion.identity);
            }
        }
    }
}