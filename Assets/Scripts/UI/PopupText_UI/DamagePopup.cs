using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] private float moveYSpeed;
    [SerializeField] private float dissapearSpeed;

    static Transform posToDestroy;
    public static DamagePopup Create(Vector3 pos, float v_damge, Transform _posToDestroy)
    {
        posToDestroy = _posToDestroy;
        Transform damagePopupTransform = Instantiate(PlayerManager.Instance.damagePopup, pos, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(v_damge);

        return damagePopup;
    }
    private TextMeshPro damageTxt;
    private float disapperTimer;
    private Color textColor;
    private void Awake()
    {
        damageTxt = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        disapperTimer -= Time.deltaTime;
        float dissaperSpeedTimer = dissapearSpeed;
        if (disapperTimer <= 0)
        {
            textColor.a -= dissaperSpeedTimer * Time.deltaTime;
            damageTxt.color = textColor;
        }
    }
    public void Setup(float damageAmount)
    {
        damageTxt.text = Mathf.FloorToInt(damageAmount).ToString();
        textColor = damageTxt.color;
        disapperTimer = 1f;
    }
}
