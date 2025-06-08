using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    [SerializeField] private Destructable destructable;
    [SerializeField] private Image image;

    private void Start()
    {
        destructable.ChangeHitPoints.AddListener(OnChangeHitPoints);
    }

    private void OnDestroy()
    {
        destructable.ChangeHitPoints.RemoveListener(OnChangeHitPoints);
    }

    private void OnChangeHitPoints()
    {
        image.fillAmount = (float)destructable.GetHitPoints() / destructable.GetMaxHitPoints();
    }
}
