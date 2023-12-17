using System;
using UnityEngine;

[Serializable]
public class Entity : CharacterStats
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (anim == null) Debug.LogWarning($"{nameof(anim)} is null, Please Assign this component");
        if (rb == null) Debug.LogWarning($"{nameof(rb)} is null, Please Assign this component");
        if (sr == null) Debug.LogWarning($"{nameof(sr)} is null, Please Assign this component");
    }

    protected virtual void FixedUpdate()
    {

    }
}
