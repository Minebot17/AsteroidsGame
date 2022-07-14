using System.Collections.Generic;
using GameModel.Entities;
using GameModel.Map;
using UnityEngine;

namespace GameModel
{
    public class GameModel : IGameModel
    {
        private readonly List<IUpdatable> _updatables = new();
        private readonly IEntityManager _entityManager;
        private readonly IMapSizeManager _mapSizeManager;
        private readonly PlayerEntity _player;

        public IEntityManager EntityManager => _entityManager;

        public GameModel(Vector2 mapSize)
        {
            _entityManager = new EntityManager();
            _mapSizeManager = new MapSizeManager(mapSize);
            _player = new PlayerEntity(_mapSizeManager, _entityManager, 0.0075f, 4f, 
                0.987f, 10, 50, 200, 4); // TODO вынести настройки в ScriptableObject
        }

        public void StartGame()
        {
            _entityManager.SpawnEntity(_player);
            
            _updatables.Add(new AsteroidsSpawner(_entityManager, _mapSizeManager, 1, 40, 0.05f, 1));
            _updatables.Add(new MapBorderEntityTeleporter(_entityManager, _mapSizeManager, 2));
        }
        
        public void TickUpdate()
        {
            _entityManager.TickUpdate();
            _updatables.ForEach(u => u.TickUpdate());
        }
    }
}