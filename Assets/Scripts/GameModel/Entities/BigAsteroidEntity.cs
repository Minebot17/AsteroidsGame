using GameModel.Core;
using GameModel.Entities.Weapons;
using GameModel.Utils;
using UnityEngine;

namespace GameModel.Entities
{
    public class BigAsteroidEntity : MovableEntity, IScoreSource
    {
        private const float SpeedModifierForSmallAsteroids = 2.5f;

        private readonly IEntityManager _entityManager;
        private readonly int _spawnSmallAsteroidsCount;
        private readonly int _smallAsteroidScore;

        public int Score { get; }

        public BigAsteroidEntity(
            IEntityManager entityManager,
            Vector2 position, 
            Vector2 velocity, 
            float torque, 
            int spawnSmallAsteroidsCount,
            int bigAsteroidScore,
            int smallAsteroidScore) : base(
            position, velocity, torque)
        {
            _entityManager = entityManager;
            _spawnSmallAsteroidsCount = spawnSmallAsteroidsCount;
            Score = bigAsteroidScore;
            _smallAsteroidScore = smallAsteroidScore;
        }

        public override void Destroyed()
        {
            base.Destroyed();
            
            for (var i = 0; i < _spawnSmallAsteroidsCount; i++)
            {
                var smallAsteroid = new SmallAsteroidEntity(
                    Position,
                    Velocity.Rotate(Random.value * 360) * SpeedModifierForSmallAsteroids,
                    Torque * (Random.value > 0.5 ? 1 : -1),
                    _smallAsteroidScore);

                _entityManager.SpawnEntity(smallAsteroid);
            }
        }
        
        protected override bool IsCanDestroyedBy(IEntity other)
        {
            return other is BulletEntity or LaserEntity;
        }
    }
}