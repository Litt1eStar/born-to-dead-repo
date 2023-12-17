using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    protected override void Introduction()
    {
        base.Introduction();
        Debug.Log("IM SKELETON");
    }
}
