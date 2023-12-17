using Assets.Scripts.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float bulletSpeed;
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy target = collision.GetComponent<Enemy>();
        if (target != null)
        {
            target.TakeDamage(PlayerManager.Instance.player.strength.GetValue());
            if (target.TryGetComponent<KnockbackFeedback>(out KnockbackFeedback enemyFeedback))
            {
                enemyFeedback.PlayFeedback(transform.gameObject);
            }
            Destroy(this.gameObject);
        }
        
    }
}
