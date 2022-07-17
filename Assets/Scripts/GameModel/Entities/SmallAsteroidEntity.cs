using GameModel.Core;
using GameModel.Entities.Weapons;
using UnityEngine;

namespace GameModel.Entities
{
    public class SmallAsteroidEntity : MovableEntity, IScoreSource
    {
        public int Score { get; }

        public SmallAsteroidEntity(Vector2 position, Vector2 velocity, float torque, int score)
            : base(position, velocity, torque)
        {
            Score = score;
        }

        protected override bool IsCanDestroyedBy(IEntity other)
        {
            return other is BulletEntity or LaserEntity;
        }
    }
}