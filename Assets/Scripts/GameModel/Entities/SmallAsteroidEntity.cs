using GameModel.Core;
using GameModel.Entities.Weapons;
using UnityEngine;

namespace GameModel.Entities
{
    public class SmallAsteroidEntity : MovableEntity, IScoreSource
    {
        public int Score => 2;
        
        public SmallAsteroidEntity(Vector2 position, Vector2 velocity, float torque) : base(position, velocity, torque) { }

        protected override bool IsCanDestroyedBy(IEntity other)
        {
            return other is BulletEntity or LaserEntity;
        }
    }
}