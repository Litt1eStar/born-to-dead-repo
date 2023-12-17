using Assets.Scripts.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashFX : Bullet
{
    private Transform posToSpawn;
    private Rigidbody2D rb;
    private Animator anim;

    public float moveSpeed;
    public float lifeTime = 2f;

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
    public void OnSpawn()
    {
        Destroy(this.gameObject, lifeTime);
    }
}
