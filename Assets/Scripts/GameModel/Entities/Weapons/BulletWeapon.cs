using GameModel.Core;
using UnityEngine;

namespace GameModel.Entities.Weapons
{
    public class BulletWeapon : Weapon
    {
        public BulletWeapon(IEntityManager entityManager, int fireCooldown) : base(entityManager, fireCooldown) { }

        public override void Fire(Vector2 position, Vector2 direction)
        {
            base.Fire(position, direction);
            
            var bullet = new BulletEntity(position, direction, 100, 0.5f);
            EntityManager.SpawnEntity(bullet);
        }
    }
}