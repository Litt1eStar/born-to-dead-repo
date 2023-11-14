using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 2;
    public float timer = 0;

    public float bulletSpeed;
    private void Start()
    {
        timer = lifeTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy target = collision.GetComponent<Enemy>();
        if (target!=null)
        {
            target.TakeDamage(PlayerManager.instance.player.strenth.GetValue());
            Destroy(this.gameObject);            
        }
    }
}
