using System;
using System.Collections.Generic;
using GameModel.Entities;
using GameModel.Utils;

namespace GameModel.Map
{
    public class EntityManager : IEntityManager
    {
        public event Action<IEntity> OnEntitySpawned;
        public event Action<IEntity> OnEntityDestroyed;

        private readonly List<IEntity> _entities = new();
        private readonly IEntitySpawner _entitySpawner;
        
        public IReadOnlyList<IEntity> Entities => _entities.AsReadOnly();

        public EntityManager(IEntitySpawner entitySpawner)
        {
            _entitySpawner = entitySpawner;
        }

        public void SpawnEntity<T>(T entity) where T : IEntity
        {
            entity.OnSelfDestroy += () => DestroyEntity(entity);
            _entities.Add(entity);
            OnEntitySpawned?.Invoke(entity);
            _entitySpawner.SpawnEntity(entity);
        }

        public void DestroyEntity(IEntity entity)
        {
            _entities.Remove(entity);
            entity.Destroyed();
            OnEntityDestroyed?.Invoke(entity);
        }

        public void FixedUpdate()
        {
            foreach (var entity in _entities)
            {
                entity.FixedUpdate();
            }
        }
    }
}