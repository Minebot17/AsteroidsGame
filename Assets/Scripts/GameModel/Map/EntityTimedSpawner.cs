using GameModel.Entities;
using GameModel.Entities.Factories;

namespace GameModel.Map
{
    public class EntityTimedSpawner<T> : IUpdatable where T : IEntity
    {
        private readonly IEntityFactory<T> _entityFactory;
        private readonly IEntityManager _entityManager;
        private readonly int _maxEntities;
        private readonly int _spawnEntitiesTicksPeriod;

        private int _currentEntitiesCount;
        private int _currentTicks;
        
        public EntityTimedSpawner(
            IEntityFactory<T> entityFactory,
            IEntityManager entityManager, 
            int maxEntities, 
            int spawnEntitiesTicksPeriod)
        {
            _entityFactory = entityFactory;
            _entityManager = entityManager;
            _maxEntities = maxEntities;
            _spawnEntitiesTicksPeriod = spawnEntitiesTicksPeriod;

            _entityManager.OnEntitySpawned += OnEntitySpawned;
            _entityManager.OnEntityDestroyed += OnEntityDestroyed;
        }
        
        public void TickUpdate()
        {
            if (_currentTicks >= _spawnEntitiesTicksPeriod)
            {
                _currentTicks = 0;

                if (_maxEntities <= _currentEntitiesCount)
                {
                    return;
                }
                
                _entityManager.SpawnEntity(_entityFactory.Create());
            }
            else
            {
                _currentTicks++;
            }
        }

        private void OnEntitySpawned(IEntity entity)
        {
            if (entity is T)
            {
                _currentEntitiesCount++;
            }
        }
        
        private void OnEntityDestroyed(IEntity entity)
        {
            if (entity is T)
            {
                _currentEntitiesCount--;
            }
        }
    }
}