using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShootSkill : MonoBehaviour
{
    [Header("Data")]
    public BulletSO bulletData;
    
    [Header("References")]
    private Player player;
    private GameObject aimTransform;
    private GameObject bulletSpawnPos;
    
    private Vector3 mousePosition;

    private bool canShoot = true;

    private void Start()
    {
        player = GetComponent<Player>(); 
    }
    private void Update()
    {
        if (aimTransform==null)
            aimTransform = GameObject.FindGameObjectWithTag("Aim");

        if (bulletSpawnPos == null)
            bulletSpawnPos = GameObject.FindGameObjectWithTag("BulletSpawnPosition");
        

        AimHandler();

        if (canShoot)
        {
            //StartCoroutine(Shooting());
        }
    }
    IEnumerator Shooting()
    {
        if (player == null || bulletData == null)
        {
            Debug.LogWarning($"player is null");
            yield return null;
        }


        canShoot = false;

        for (int i = 0; i < player.projectileAmount.GetValue(); i++)
        {
            //Instantiate bullet prefab to scene
            GameObject newBullet = Instantiate(bulletData.prefab, bulletSpawnPos.transform.position, Quaternion.identity);
            //Set position of bullet
            //newBullet.transform.right = newBullet.transform.position - bulletSpawnPos.position;

            //Direction that bullet will go
            Vector2 direction = mousePosition - bulletSpawnPos.transform.position;
            direction.Normalize();

            //Setup bullet
            newBullet.GetComponent<Rigidbody2D>().velocity = direction * player.projectileSpeed.GetValue();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            newBullet.transform.rotation = Quaternion.Euler(0, 0, angle);
                
            yield return new WaitForSeconds(player.attackRatePerSecond.GetValue());
        }

        yield return new WaitForSeconds(player.cooldownToNextAttack.GetValue());
        canShoot = true;
    }
    private void AimHandler()
    {
        mousePosition = InputManager.Instance.GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.transform.eulerAngles = new Vector3(0, 0, angle);
    }

}
