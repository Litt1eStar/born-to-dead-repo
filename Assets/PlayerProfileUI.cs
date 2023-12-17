using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Helper;

public class PlayerProfileUI : MonoBehaviour
{
    public Image profile;

    public void SetPlayerProfileIcon()
    {
        profile.sprite = PlayerManager.Instance.player.c_class.sprite;
        DebugHelper.Debugger(this.name, "Set Player profile on UI Updated");
    }
}
