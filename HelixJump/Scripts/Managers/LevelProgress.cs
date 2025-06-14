using UnityEngine;

public class LevelProgress : MonoBehaviour
{

    [SerializeField] private BallController ballController;
    private int currentLevel = 1;
    private int CurrentLevel => currentLevel;

    private void Start()
    {
        ballController.CollisionSegment.AddListener(OnBallCollisionSegment);
    }

    private void OnDestroy()
    {
        ballController.CollisionSegment.RemoveListener(OnBallCollisionSegment);
    }

    private void OnBallCollisionSegment(SegmentType type)
    {
        if (type == SegmentType.Finish)
        {
            currentLevel++;
        }

    }
}
