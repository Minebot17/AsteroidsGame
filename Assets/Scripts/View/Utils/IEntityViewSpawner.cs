using System;
using GameModel.Entities;
using View.EntityViews;

namespace View.Utils
{
    public interface IEntityViewSpawner
    {
        event Action<IEntityView> OnEntityViewSpawned;
        
        void SpawnEntity(IEntity entity);
    }
}