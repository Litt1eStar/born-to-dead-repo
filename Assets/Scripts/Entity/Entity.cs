using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[Serializable]
public class Entity : CharacterStats
{
    #region Main Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public SpriteRenderer sr { get; private set; }
    #endregion
    public override void Awake()
    {
        base.Awake();
    }
    public override void Start()
    {
        base.Start();

        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    public override void Update()
    {
        base.Update();

        if (anim == null) Debug.LogWarning($"{nameof(anim)} is null, Please Assign this component");
        if (rb == null) Debug.LogWarning($"{nameof(rb)} is null, Please Assign this component");
        if(sr == null) Debug.LogWarning($"{nameof(sr)} is null, Please Assign this component");
    }

    protected virtual void FixedUpdate()
    {

    }
}
