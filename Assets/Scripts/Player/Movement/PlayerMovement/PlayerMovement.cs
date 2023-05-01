using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float speedMultiplier = 1f;
    [SerializeField] private LayerMask barrierColliders;

    private Vector3 moveDirection = Vector3.zero;
    private InputManager inputManager;
    private float rayDistance = 0.5f;

    private void Start()
    {
        inputManager = InputManager.Instance;
    }

    private void FixedUpdate()
    {
        GetMovementDirection();
        Move();
    }

    private void GetMovementDirection()
    {
        var inputVector = GetInputVector();
        moveDirection = new Vector3(inputVector.x, inputVector.y, 0);
    }

    private void Move()
    {
        if (IsMoving() && CanMove())
        {
            transform.position += (moveDirection * moveSpeed) * speedMultiplier;
        }
    }

    private Vector2 GetInputVector() => inputManager.GetMovementVectorNormalized();
    public bool IsMoving() => moveDirection != Vector3.zero;
    private bool CanMove() => !Physics2D.Raycast(transform.position, GetInputVector(), rayDistance, barrierColliders);
}
