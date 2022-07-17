using GameModel.Core;
using UnityEngine;

namespace GameModel.Entities.Weapons
{
    public abstract class Weapon : IWeapon
    {
        protected readonly IEntityManager EntityManager;
        
        protected readonly int FireCooldown;
            
        protected int CurrentFireCooldown;
        
        public virtual bool IsCanFire => CurrentFireCooldown <= 0;
        
        public Weapon(IEntityManager entityManager, int fireCooldown)
        {
            EntityManager = entityManager;
            FireCooldown = fireCooldown;
        }

        public void TryFire(Vector2 position, Vector2 direction)
        {
            if (IsCanFire)
            {
                Fire(position, direction);
            }
        }

        public virtual void Fire(Vector2 position, Vector2 direction)
        {
            CurrentFireCooldown = FireCooldown;
        }
        
        public virtual void TickUpdate()
        {
            if (CurrentFireCooldown > 0)
            {
                CurrentFireCooldown--;
            }
        }
    }
}