using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet_SO")]
public class BulletSO : ScriptableObject
{
    public GameObject prefab;
    public List<string> elementals; // List<Element>
    public List<string> bulletTypes; // List<BulletType>
}