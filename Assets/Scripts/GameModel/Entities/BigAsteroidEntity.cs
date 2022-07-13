using UnityEngine;

namespace GameModel.Entities
{
    public class BigAsteroidEntity : MovableEntity
    {
        public BigAsteroidEntity(Vector2 position, Vector2 velocity, float torque) : base(position, velocity, torque)
        {
        }
    }
}