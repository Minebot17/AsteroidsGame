using UnityEngine;

namespace GameModel.Entities
{
    public class SmallAsteroidEntity : MovableEntity
    {
        public SmallAsteroidEntity(Vector2 position, Vector2 velocity, float torque) : base(position, velocity, torque) { }

        public override void OnCollision(ICollidable other)
        {
            if (other is BulletEntity)
            {
                Destroy();
            }
        }
    }
}