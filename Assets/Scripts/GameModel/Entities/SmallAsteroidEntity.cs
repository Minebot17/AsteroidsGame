using GameModel.Core;
using GameModel.Entities.Player;
using UnityEngine;

namespace GameModel.Entities
{
    public class SmallAsteroidEntity : MovableEntity, IScoreSource
    {
        public int Score => 2;
        
        public SmallAsteroidEntity(Vector2 position, Vector2 velocity, float torque) : base(position, velocity, torque) { }

        public override void OnCollision(IEntity other)
        {
            if (other is BulletEntity)
            {
                Destroy();
            }
        }
    }
}