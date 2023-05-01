using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float speedMultiplier = 1f;
    public static MainCameraController Instance { get; private set; }
    private Transform targetTransform;
    private Transform originalTransform;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        originalTransform = transform;
    }

    private void Update()
    {
        if (targetTransform!= null)
        {
            MoveCameraToTarget();
        }
    }

    public void PlayCampsiteCinematic(Transform campTransform)
    {
        // Move Camera to Target
        targetTransform = campTransform;
        // Once Camera is @ Target, wait a few seconds
        // Move Camera Back to Player
    }

    private void MoveCameraToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetTransform.position, moveSpeed * speedMultiplier);
        if (transform.position == targetTransform.position)
        {
            targetTransform = originalTransform;
        }
    }
}
