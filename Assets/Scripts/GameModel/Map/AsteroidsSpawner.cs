using System.Collections.Generic;
using GameModel.Entities;
using GameModel.Utils;
using UnityEngine;

namespace GameModel.Map
{
    public class AsteroidsSpawner : IUpdatable
    {
        private readonly IEntityManager _entityManager;
        private readonly IMapSizeManager _mapSizeManager;
        private readonly int _maxAsteroids;
        private readonly int _spawnAsteroidTicksPeriod;
        private readonly float _bigAsteroidSpeed;
        private readonly float _bigAsteroidTorque;

        private int _currentBigAsteroidsCount;
        private int _currentTicks;
        
        public AsteroidsSpawner(
            IEntityManager entityManager, 
            IMapSizeManager mapSizeManager, 
            int maxAsteroids, 
            int spawnAsteroidTicksPeriod,
            float bigAsteroidSpeed,
            float bigAsteroidTorque)
        {
            _entityManager = entityManager;
            _mapSizeManager = mapSizeManager;
            _maxAsteroids = maxAsteroids;
            _spawnAsteroidTicksPeriod = spawnAsteroidTicksPeriod;
            _bigAsteroidSpeed = bigAsteroidSpeed;
            _bigAsteroidTorque = bigAsteroidTorque;

            _entityManager.OnEntitySpawned += OnEntitySpawned;
            _entityManager.OnEntityDestroyed += OnEntityDestroyed;
        }
        
        public void FixedUpdate()
        {
            if (_currentTicks >= _spawnAsteroidTicksPeriod)
            {
                _currentTicks = 0;

                if (_maxAsteroids < _currentBigAsteroidsCount)
                {
                    return;
                }
                
                BigAsteroidEntity bigAsteroidEntity = new BigAsteroidEntity(
                    _mapSizeManager.GetRandomPositionOnBorder(), 
                    _bigAsteroidSpeed * Vector2.right.Rotate(Random.value * 360), 
                    _bigAsteroidTorque);
                
                _entityManager.SpawnEntity(bigAsteroidEntity);
            }
            else
            {
                _currentTicks++;
            }
        }

        private void OnEntitySpawned(IEntity entity)
        {
            if (entity is BigAsteroidEntity)
            {
                _currentBigAsteroidsCount++;
            }
        }
        
        private void OnEntityDestroyed(IEntity entity)
        {
            if (entity is BigAsteroidEntity)
            {
                _currentBigAsteroidsCount--;
            }
        }
    }
}