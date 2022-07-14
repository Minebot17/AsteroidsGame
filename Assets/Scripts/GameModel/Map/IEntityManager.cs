using System;
using System.Collections.Generic;
using GameModel.Entities;

namespace GameModel.Map
{
    public interface IEntityManager : IUpdatable
    {
        event Action<IEntity> OnEntitySpawned;
        event Action<IEntity> OnEntityDestroyed;
        
        IReadOnlyList<IEntity> Entities { get; }
        
        void SpawnEntity(IEntity entity);
        void DestroyEntity(IEntity entity);
    }
}