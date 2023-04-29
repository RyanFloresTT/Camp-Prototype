using System;using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Detection : MonoBehaviour
{
    [SerializeField] private LayerMask detectableLayer;
    public event EventHandler<GameObject> ObjectEnteredRange;
    public event EventHandler<GameObject> ObjectExitedRange;

    private void Start()
    {
        gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsInLayerMask(other.gameObject, detectableLayer))
        {
            ObjectEnteredRange?.Invoke(this, other.gameObject);
            Debug.Log("Detect Success.");
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (IsInLayerMask(other.gameObject, detectableLayer))
        {
            ObjectExitedRange?.Invoke(this, other.gameObject);
        }
    }

    private bool IsInLayerMask(GameObject obj, LayerMask layerMask) => ((layerMask.value & (1 << obj.layer)) > 0);

}
