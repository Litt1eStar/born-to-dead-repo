using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerShoot : MonoBehaviour
{
    [Header("References")]
    public Player player;
    public BulletSO bulletData;
    public Transform aimTransform;
    public Transform bulletSpawnPos;
    public Transform mouseIndicator;
    private Vector3 mousePosition;

    private bool canShoot = true;
    private void Update()
    {
        AimHandler();

        ShowMouseIndicator();

        if (canShoot)
            StartCoroutine(Shooting());
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
            GameObject newBullet = Instantiate(bulletData.prefab, bulletSpawnPos.position, Quaternion.identity);
            //Set position of bullet
            newBullet.transform.right = newBullet.transform.position - bulletSpawnPos.position;

            //Direction that bullet will go
            Vector2 direction = mousePosition - bulletSpawnPos.position;
            direction.Normalize();

            //Setup bullet
            newBullet.GetComponent<Rigidbody2D>().velocity = direction * player.projectileSpeed.GetValue();
                
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
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }
    private void ShowMouseIndicator()
    {
        mouseIndicator.transform.position = mousePosition;
    }
}
