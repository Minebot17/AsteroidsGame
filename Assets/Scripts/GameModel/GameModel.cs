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
        private PlayerEntity _player;

        public IEntityManager EntityManager => _entityManager;

        public GameModel()
        {
            _entityManager = new EntityManager();
            _mapSizeManager = new MapSizeManager(new Vector2(30, 30)); // TODO change map size to screen with offsets
        }
        
        public void StartGame()
        {
            _player = new PlayerEntity(0.0075f, 4f, 0.987f);
            _entityManager.SpawnEntity(_player);
            
            _updatables.Add(new AsteroidsSpawner(_entityManager, _mapSizeManager, 5, 40, 0.05f, 1));
        }
        
        public void FixedUpdate()
        {
            _entityManager.FixedUpdate();
            _updatables.ForEach(u => u.FixedUpdate());
        }
    }
}