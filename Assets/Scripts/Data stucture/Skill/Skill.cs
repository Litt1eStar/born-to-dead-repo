using Assets.Scripts.Helper;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Data_stucture.Skill
{
    [CreateAssetMenu(menuName = "Skill")]
    public class Skill : ScriptableObject
    {
        public bool canShoot = true;
        public virtual void Activate(MonoBehaviour coroutineExecutor)
        {
            //DebugHelper.Debugger(this.name, $"canShoot is {canShoot} | {Time.time}");
        }
        
    }
}