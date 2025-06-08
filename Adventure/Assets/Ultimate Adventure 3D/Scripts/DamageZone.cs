using System.Threading;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float damageRate;
    private Destructable destructable_obj;
    private float timer;
    private void Update()
    {
        if (destructable_obj == null) return;

        timer += Time.deltaTime;

        if (timer >= damageRate)
        {
            if (destructable_obj != null)
            {
                destructable_obj.ApplyDamage(damage);
            }

            timer = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        destructable_obj = other.GetComponent<Destructable>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Destructable>() == destructable_obj)
        {
            destructable_obj = null;
        }
    }
}
