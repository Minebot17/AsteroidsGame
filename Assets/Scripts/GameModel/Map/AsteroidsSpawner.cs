using System.Collections.Generic;
using GameModel.Entities;
using GameModel.Utils;
using UnityEngine;

namespace GameModel.Map
{
    public class AsteroidsSpawner : IUpdatable
    {
        private readonly IEntitySpawner _entitySpawner;
        private readonly int _maxAsteroids;
        private readonly int _spawnAsteroidTicksPeriod;
        private readonly List<IAsteroidLogic> _spawnedAsteroids;

        private int _currentTicks;
        
        public AsteroidsSpawner(IEntitySpawner entitySpawner, int maxAsteroids, int spawnAsteroidTicksPeriod)
        {
            _entitySpawner = entitySpawner;
            _maxAsteroids = maxAsteroids;
            _spawnAsteroidTicksPeriod = spawnAsteroidTicksPeriod;
        }
        
        public void FixedUpdate()
        {
            if (_currentTicks >= _spawnAsteroidTicksPeriod && _maxAsteroids < _spawnedAsteroids.Count)
            {
                _currentTicks = 0;
                IAsteroidLogic asteroidLogic = new BigAsteroidLogic();
                // TODO set random position
                _entitySpawner.SpawnEntity(asteroidLogic);
            }
            else
            {
                _currentTicks++;
            }
        }
    }
}