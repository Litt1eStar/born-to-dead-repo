using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Helper
{
    public static class DebugHelper
    {
        public static void Debugger(string className ,string text)
        {
            Debug.Log($"<color={Color.red}>Send from : {className}</color> | {text}");
        }
    }

}