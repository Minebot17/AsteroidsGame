﻿using System.Collections.Generic;
using GameModel.Map;
using GameModel.Utils;
using UnityEngine;

namespace GameModel.Entities
{
    public class BigAsteroidEntity : MovableEntity
    {
        private const float SpeedModifierForSmallAsteroids = 2f;

        private readonly IEntityManager _entityManager;
        private readonly int _spawnSmallAsteroidsCount;
        
        public BigAsteroidEntity(
            IEntityManager entityManager,
            Vector2 position, 
            Vector2 velocity, 
            float torque, 
            int spawnSmallAsteroidsCount) : base(
            position, velocity, torque)
        {
            _entityManager = entityManager;
            _spawnSmallAsteroidsCount = spawnSmallAsteroidsCount;
        }

        public override void OnCollision(ICollidable other)
        {
            if (other is BulletEntity)
            {
                Destroy();
                
                for (var i = 0; i < _spawnSmallAsteroidsCount; i++)
                {
                    var smallAsteroid = new SmallAsteroidEntity(
                        Position,
                        Velocity.Rotate(Random.value * 360) * SpeedModifierForSmallAsteroids,
                        Torque * (Random.value > 0.5 ? 1 : -1));

                    _entityManager.SpawnEntity(smallAsteroid);
                }
            }
        }
    }
}