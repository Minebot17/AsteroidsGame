using GameModel.Core;
using GameModel.Map;
using GameModel.Utils;
using UnityEngine;

namespace GameModel.Entities.Factories
{
    public class MapBigAsteroidFactory : IEntityFactory<BigAsteroidEntity>
    {
        private readonly IEntityManager _entityManager;
        private readonly IMapSizeManager _mapSizeManager;
        private readonly float _initSpeed;
        private readonly float _initTorque;
        private readonly int _spawnSmallAsteroidsCount;
        private readonly int _bigAsteroidScore;
        private readonly int _smallAsteroidScore;
        
        public MapBigAsteroidFactory(
            IEntityManager entityManager,
            IMapSizeManager mapSizeManager,
            float initSpeed,
            float initTorque,
            int spawnSmallAsteroidsCount,
            int bigAsteroidScore,
            int smallAsteroidScore)
        {
            _entityManager = entityManager;
            _mapSizeManager = mapSizeManager;
            _initSpeed = initSpeed;
            _initTorque = initTorque;
            _spawnSmallAsteroidsCount = spawnSmallAsteroidsCount;
            _bigAsteroidScore = bigAsteroidScore;
            _smallAsteroidScore = smallAsteroidScore;
        }

        public BigAsteroidEntity Create()
        {
            return new BigAsteroidEntity(
                _entityManager,
                _mapSizeManager.GetRandomPositionOnBorder(),
                _initSpeed * Vector2.right.Rotate(Random.value * 360),
                _initTorque * (Random.value > 0.5 ? 1 : -1),
                _spawnSmallAsteroidsCount,
                _bigAsteroidScore,
                _smallAsteroidScore);
        }
    }
}