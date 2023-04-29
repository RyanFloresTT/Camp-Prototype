using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float speedMultiplier = 1f;
    [SerializeField] private Detection detection;

    private Transform followTarget;

    private void Start()
    {
        detection.ObjectEnteredRange += Handle_ObjectEnterdRange;
        detection.ObjectExitedRange += Handle_ObjectExitedRange;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (!CanMove()) return;
        transform.position = Vector2.MoveTowards(transform.position, GetMovementDirection(), moveSpeed * speedMultiplier);
    }
    private void Handle_ObjectEnterdRange(object sender, GameObject e)
    {
        followTarget = e.transform;
    }
    private void Handle_ObjectExitedRange(object sender, GameObject e)
    {
        followTarget = null;
    }

    private Vector3 GetMovementDirection() => followTarget == null ? Vector3.zero : followTarget.transform.position;
    private bool CanMove() => followTarget != null;

}
