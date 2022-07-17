using UnityEngine;

namespace GameModel.Entities.Weapons
{
    public class BulletEntity : MovableEntity
    {
        private int _remainingLifeDuration;
        
        public BulletEntity(
            Vector2 position, 
            Vector2 direction,
            int lifeDuration,
            float speed
        ) : base(position, direction.normalized * speed, 0)
        {
            _remainingLifeDuration = lifeDuration;
        }

        public override void TickUpdate()
        {
            base.TickUpdate();

            _remainingLifeDuration--;

            if (_remainingLifeDuration <= 0)
            {
                Destroy();
            }
        }

        protected override bool IsCanDestroyedBy(IEntity other)
        {
            return other is BigAsteroidEntity or SmallAsteroidEntity or UfoEntity;
        }
    }
}