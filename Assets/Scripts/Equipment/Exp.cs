using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : Item, ICollectible
{
    public int expAmount;
    public void Collect()
    {
        PlayerManager.Instance.player.HandleExperienceChange(expAmount);
        Destroy(gameObject);
    }
}
