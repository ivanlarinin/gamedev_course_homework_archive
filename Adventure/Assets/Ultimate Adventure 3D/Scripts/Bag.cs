using UnityEngine;
using UnityEngine.Events;
public class Bag : MonoBehaviour
{
    private int amountKey;

    public UnityEvent ChangeAmountKey;
    public void AddKey(int amount)
    {
        amountKey += amount;
        ChangeAmountKey.Invoke();
    }

    public bool DrawKey(int amount)
    {
        // If less than 0
        if (amountKey - amount < 0) return false;

        amountKey -= amount;
        ChangeAmountKey.Invoke();

        return true;
    }
    public int GetAmountKey()
    {
        return amountKey;
    }
}
