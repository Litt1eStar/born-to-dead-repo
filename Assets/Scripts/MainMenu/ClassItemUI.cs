using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClassItemUI : MonoBehaviour
{
    public CharacterClassSO c_class;
    public Image sr;
    public TextMeshProUGUI c_name;
    private void OnValidate()
    {
        this.name = "ClassItemUI : " + c_class.name;
    }

    private void Start()
    {
        sr.sprite = c_class.sprite;
        c_name.text = c_class.className;
    }
}
