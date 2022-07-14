using UnityEngine;

namespace GameModel.Entities
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

        public override void OnCollision(ICollidable other)
        {
            if (other is BigAsteroidEntity or SmallAsteroidEntity)
            {
                Destroy();
            }
        }
    }
}