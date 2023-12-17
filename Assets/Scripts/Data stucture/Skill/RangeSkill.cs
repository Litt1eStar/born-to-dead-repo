using Assets.Scripts.Helper;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Data_stucture.Skill
{
    [CreateAssetMenu(menuName = "Skill/RangeSkill")]
    public class RangeSkill : Skill
    {

        public override void Activate(MonoBehaviour coroutineExecutor)
        {
            base.Activate(coroutineExecutor);
            
            if(canShoot)
                coroutineExecutor.StartCoroutine(Shooting());
        }
        IEnumerator Shooting()
        {
            Player player = PlayerManager.Instance.player;
            GameObject bulletSpawnPos = GameObject.FindGameObjectWithTag("BulletSpawnPosition");

            if (player == null || player.weapon.bullet == null)
            {
                Debug.LogWarning($"player is null");
                yield return null;
            }

            canShoot = false;

            for (int i = 0; i < player.projectileAmount.GetValue(); i++)
            {
                //Instantiate bullet prefab to scene
                GameObject newBullet = Instantiate(player.weapon.bullet.prefab, bulletSpawnPos.transform.position, Quaternion.identity);
                //Set position of bullet
                //newBullet.transform.right = newBullet.transform.position - bulletSpawnPos.position;

                //Direction that bullet will go
                Vector2 direction = InputManager.Instance.GetMouseWorldPosition() - bulletSpawnPos.transform.position;
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
    }
}