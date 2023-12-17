using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ClassList
{
    private static List<CharacterClassSO> classList;

    public static void Initialize(List<CharacterClassSO> classes)
    {
        classList = classes;
    }

    public static CharacterClassSO FindClassByName(string className)
    {
        return classList.Find(c => c.className == className);
    }
}
