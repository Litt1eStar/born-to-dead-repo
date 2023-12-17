using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class KnockbackFeedback : MonoBehaviour
{
    public Rigidbody2D rb;
    public float strength = 16;
    public float delay = 0.15f;

    public UnityEvent OnBegin, OnDone;

    public void PlayFeedback(GameObject sender)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 dir;
        if (PlayerManager.Instance.player.transform.position.y > transform.position.y)
            dir = (transform.position - sender.transform.position).normalized;
        else
            dir = -(transform.position - sender.transform.position).normalized;

        rb.AddForce(dir*strength, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }
    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector2.zero;
        OnDone?.Invoke();
    }

    public void OnBeginHandler()
    {
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
        }
    }

    public void OnDoneHandler()
    {
        Destroy(rb);
    }
}
