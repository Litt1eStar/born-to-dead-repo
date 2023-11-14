using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float currentHealth;

    public Stat hp;
    public Stat mp;
    public Stat defense;
    public Stat strenth;
    public Stat speed;
    public Stat attackRatePerSecond;
    public Stat projectileSpeed;
    public Stat projectileAmount;
    public Stat cooldownToNextAttack;
    public List<string> skillTypes; // List<Skill>
    public List<string> elementals; // List<Element>

    public virtual void Start()
    {

    }
    public virtual void Awake()
    {
        currentHealth = hp.GetValue();
    }
    public virtual void Update()
    {
    }
    public virtual void TakeDamage(float _damage)
    {
        _damage -= defense.GetValue();
        _damage = Mathf.Clamp(_damage, 0, int.MaxValue);
        currentHealth -= _damage;
        Debug.Log(transform.name + " takes " + _damage);

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(this.gameObject);
    }
}