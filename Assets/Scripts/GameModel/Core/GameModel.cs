using System.Collections.Generic;
using GameModel.Entities;
using GameModel.Entities.Factories;
using GameModel.Entities.Player;
using GameModel.Map;
using UnityEngine;

namespace GameModel.Core
{
    public class GameModel : IGameModel
    {
        private readonly List<IUpdatable> _updatables = new();
        private readonly IMapSizeManager _mapSizeManager;
        private readonly PlayerEntity _player;

        public IEntityManager EntityManager { get; }
        public IScoreManager ScoreManager { get; }

        public GameModel(Vector2 mapSize)
        {
            EntityManager = new EntityManager();
            ScoreManager = new ScoreManager(EntityManager);
            _mapSizeManager = new MapSizeManager(mapSize);
            _player = new PlayerEntity(_mapSizeManager, EntityManager, 0.0075f, 4f, 
                0.987f, 10, 50, 200, 4); // TODO вынести настройки в ScriptableObject
        }

        public void StartGame()
        {
            EntityManager.SpawnEntity(_player);

            var bigAsteroidsFactory = new MapBigAsteroidFactory(EntityManager, _mapSizeManager, 0.05f, 1, 3);
            var mapUfoFactory = new MapUfoFactory(_player, _mapSizeManager, 0.08f);
            
            _updatables.Add(new EntityTimedSpawner<BigAsteroidEntity>(bigAsteroidsFactory, EntityManager, 5, 40));
            _updatables.Add(new EntityTimedSpawner<UfoEntity>(mapUfoFactory, EntityManager, 1, 400));
            _updatables.Add(new MapBorderEntityTeleporter(EntityManager, _mapSizeManager, 2));
            _updatables.Add(EntityManager);
        }
        
        public void TickUpdate()
        {
            _updatables.ForEach(u => u.TickUpdate());
        }
    }
}