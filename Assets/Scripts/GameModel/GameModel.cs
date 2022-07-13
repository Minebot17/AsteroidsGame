using System.Collections.Generic;
using GameModel.Entities;
using GameModel.Map;
using GameModel.Utils;
using UnityEngine;

namespace GameModel
{
    public class GameModel : IGameModel
    {
        private readonly List<IUpdatable> _updatables = new();
        private readonly IEntityManager _entityManager;
        private readonly IMapSizeManager _mapSizeManager;
        private readonly PlayerEntity _player;

        public GameModel(Vector2 mapSize, IEntitySpawner entitySpawner)
        {
            _entityManager = new EntityManager(entitySpawner);
            _mapSizeManager = new MapSizeManager(mapSize);
            _player = new PlayerEntity(0.0075f, 4f, 0.987f);
            _entityManager.SpawnEntity(_player);
            
            _updatables.Add(new AsteroidsSpawner(_entityManager, _mapSizeManager, 6, 40, 0.05f, 1));
            _updatables.Add(new MapBorderEntityTeleporter(_entityManager, _mapSizeManager, 2));
        }

        public void FixedUpdate()
        {
            _entityManager.FixedUpdate();
            _updatables.ForEach(u => u.FixedUpdate());
        }
    }
}