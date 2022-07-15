using System;
using System.Collections.Generic;
using GameModel.Entities;
using GameModel.Map;

namespace GameModel.Core
{
    public interface IEntityManager : IUpdatable
    {
        event Action<IEntity> OnEntitySpawned;
        event Action<IEntity> OnEntityDestroyed;
        
        IReadOnlyList<IEntity> Entities { get; }
        
        void SpawnEntity(IEntity entity);
        void DestroyEntity(IEntity entity, bool immediate = false);
    }
}