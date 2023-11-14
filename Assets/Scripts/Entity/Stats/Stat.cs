using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat 
{
    public float baseValue;
    public List<float> modifier = new List<float>();
    public float GetValue()
    {
        float finalValue = baseValue;
        modifier.ForEach(x => finalValue += x);
        return finalValue;
    }
    public void AddModifier(float _modifier)
    {
        if(_modifier != 0)
            modifier.Add(_modifier);
    }
    public void RemoveModifier(float _modifier) 
    {
        if(_modifier != 0)
            modifier.Remove(_modifier);
    }
}
