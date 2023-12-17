using Assets.Scripts.Helper;
using Assets.Scripts.Manager;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Data_stucture.Skill
{
    [CreateAssetMenu(menuName = "Skill/MeleeSkill")]
    public class MeleeSkill : Skill
    {
        public override void Activate(MonoBehaviour coroutineExecutor)
        {
            base.Activate(coroutineExecutor);
            DebugHelper.Debugger(this.name, "Melee is Activating");

            if (canShoot)
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
                GameObject fxObj = Instantiate(GameAssetManager.Instance.slashFx, bulletSpawnPos.transform.position, Quaternion.identity);
                Vector2 direction = InputManager.Instance.GetMouseWorldPosition() - bulletSpawnPos.transform.position;
                direction.Normalize();

                //Setup bullet
                fxObj.GetComponent<Rigidbody2D>().velocity = direction * 5f;
                fxObj.GetComponent<SlashFX>().OnSpawn();
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                fxObj.transform.rotation = Quaternion.Euler(0, 0, angle);

                yield return new WaitForSeconds(player.attackRatePerSecond.GetValue());
            }

            yield return new WaitForSeconds(player.cooldownToNextAttack.GetValue());
            canShoot = true;
        }
    }
}