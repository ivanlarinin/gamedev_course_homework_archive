using UnityEngine;
using UnityEngine.UI;

public class UIKeyTex : MonoBehaviour
{
    [SerializeField] private Bag bag;
    [SerializeField] private Text text;

    private void Start()
    {
        bag.ChangeAmountKey.AddListener(OnChangeHitPoints);
    }

    private void OnDestroy()
    {
        bag.ChangeAmountKey.RemoveListener(OnChangeHitPoints);
    }

    private void OnChangeHitPoints()
    {
        text.text = bag.GetAmountKey().ToString();
    }
}
