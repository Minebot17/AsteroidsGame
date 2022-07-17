using GameModel.Core;
using UnityEngine;

namespace GameModel.Entities.Weapons
{
    public class BulletWeapon : Weapon
    {
        private readonly int _bulletLifeDuration;
        private readonly float _bulletSpeed;

        public BulletWeapon(IEntityManager entityManager, int fireCooldown, int bulletLifeDuration, float bulletSpeed)
            : base(entityManager, fireCooldown)
        {
            _bulletLifeDuration = bulletLifeDuration;
            _bulletSpeed = bulletSpeed;
        }

        public override void Fire(Vector2 position, Vector2 direction)
        {
            base.Fire(position, direction);
            
            var bullet = new BulletEntity(position, direction, _bulletLifeDuration, _bulletSpeed);
            EntityManager.SpawnEntity(bullet);
        }
    }
}