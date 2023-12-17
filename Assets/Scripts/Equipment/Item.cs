using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item Setting")]
    public float duration = 0.3f;
    public float speedOfItemAnimation = 10f;
    public int quantity = 1;

    [Header("References")]
    protected Rigidbody2D rb;
    protected Vector3 targetPosition;

    protected bool hasTarget;
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    protected virtual void FixedUpdate()
    {
        if (hasTarget) // Only move if not picked up
        {
            ItemMagnetAnimation();
        }
    }
    protected virtual void ItemMagnetAnimation()
    {
        float t = Time.fixedDeltaTime / duration;
        Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, t);
        rb.MovePosition(newPosition);

        // Check if the item has reached the target
        if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
        {
            hasTarget = false;
            rb.velocity = Vector2.zero;
        }
    }

    public void SetTarget(Vector3 position)
    {
        targetPosition = position;
        hasTarget = true;
    }
}
