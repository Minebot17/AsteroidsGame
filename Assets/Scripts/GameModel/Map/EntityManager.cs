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
        private readonly List<IEntity> _entitiesToDestroy = new();
        
        public IReadOnlyList<IEntity> Entities => _entities.AsReadOnly();

        public void SpawnEntity(IEntity entity)
        {
            entity.OnSelfDestroy += () => DestroyEntity(entity);
            _entities.Add(entity);
            OnEntitySpawned?.Invoke(entity);
        }

        public void DestroyEntity(IEntity entity, bool immediate = false)
        {
            if (immediate)
            {
                DestroyEntityImmediately(entity);
            }
            else
            {
                _entitiesToDestroy.Add(entity);
            }
        }

        public void TickUpdate()
        {
            foreach (var entity in _entities)
            {
                entity.TickUpdate();
            }
            
            foreach (var entity in _entitiesToDestroy)
            {
                DestroyEntityImmediately(entity);
            }
            
            _entitiesToDestroy.Clear();
        }
        
        private void DestroyEntityImmediately(IEntity entity)
        {
            _entities.Remove(entity);
            entity.Destroyed();
            OnEntityDestroyed?.Invoke(entity);
        }
    }
}