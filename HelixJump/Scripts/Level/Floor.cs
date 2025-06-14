using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] private List<Segment> defaultSegment;

    public void AddEmptySegment(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            defaultSegment[i].SetEmpty();
        }

        for (int i = amount; i >= 0; i--)
        {
            defaultSegment.RemoveAt(0);
        }

    }

    public void AddRandomTrapSegment(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int index = Random.Range(0, defaultSegment.Count);

            defaultSegment[index].SetTrap();
            defaultSegment.RemoveAt(index);
        }
    }
    public void SetRandomRotate()
    {
        transform.eulerAngles = new Vector3(0, Random.Range(0, 300), 0);
    }

    public void SetFinishSegment()
    {
        for (int i = 0; i < defaultSegment.Count; i++)
        {
            defaultSegment[i].SetFinish();
        }
    }
}
