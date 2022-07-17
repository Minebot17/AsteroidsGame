using System.Collections.Generic;
using GameModel.Entities;
using GameModel.Entities.Factories;
using GameModel.Entities.Weapons;
using GameModel.Map;
using GameModel.Utils;

namespace GameModel.Core
{
    public class GameModel : IGameModel
    {
        private readonly List<IUpdatable> _updatables = new();
        private readonly IMapSizeManager _mapSizeManager;
        private readonly PlayerEntity _player;
        private readonly IGameSettings _gameSettings;

        public IEntityManager EntityManager { get; }
        public IScoreManager ScoreManager { get; }

        public GameModel(IGameSettings gameSettings)
        {
            _gameSettings = gameSettings;
            EntityManager = new EntityManager();
            ScoreManager = new ScoreManager(EntityManager);
            _mapSizeManager = new MapSizeManager(_gameSettings.MapSize);
            _player = new PlayerEntity(
                _gameSettings.PlayerMovingSpeed, _gameSettings.PlayerRotationSpeed, _gameSettings.PlayerDragModifier, 
                new BulletWeapon(EntityManager, _gameSettings.BulletFireCooldown, 
                    _gameSettings.BulletLifeDuration, _gameSettings.BulletSpeed), 
                new LaserWeapon(_mapSizeManager, EntityManager, _gameSettings.LaserFireCooldown, 
                    _gameSettings.LaserChargeCooldown, _gameSettings.MaxLaserCharges));
        }

        public void StartGame()
        {
            EntityManager.SpawnEntity(_player);

            var bigAsteroidsFactory = new MapBigAsteroidFactory(
                EntityManager, _mapSizeManager, _gameSettings.BigAsteroidsSpeed, _gameSettings.BigAsteroidsTorque, 
                _gameSettings.BigAsteroidsFragmentsCount, _gameSettings.ScoreBigAsteroid, _gameSettings.ScoreSmallAsteroid);
            var mapUfoFactory = new MapUfoFactory(_player, _mapSizeManager, _gameSettings.UfoSpeed, _gameSettings.ScoreUfo);
            
            _updatables.Add(new EntityTimedSpawner<BigAsteroidEntity>(bigAsteroidsFactory, EntityManager, 
                _gameSettings.MaxBigAsteroidsCount, _gameSettings.SpawnBigAsteroidPeriod));
            _updatables.Add(new EntityTimedSpawner<UfoEntity>(
                mapUfoFactory, EntityManager, _gameSettings.MaxUfosCount, _gameSettings.SpawnUfoPeriod));
            _updatables.Add(new MapBorderEntityTeleporter(
                EntityManager, _mapSizeManager, _gameSettings.TeleporterBorderOffset));
            _updatables.Add(EntityManager);
        }
        
        public void TickUpdate()
        {
            _updatables.ForEach(u => u.TickUpdate());
        }
    }
}