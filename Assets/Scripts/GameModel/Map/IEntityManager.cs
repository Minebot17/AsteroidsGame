using System;
using GameModel.Entities;

namespace GameModel.Map
{
    public interface IEntityManager : IUpdatable
    {
        event Action<IEntity> OnEntitySpawned;
        event Action<IEntity> OnEntityDestroyed;
        
        void SpawnEntity(IEntity entity);
        void DestroyEntity(IEntity entity);
    }
}