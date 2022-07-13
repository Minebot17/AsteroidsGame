using GameModel.Entities;
using UnityEngine;

namespace GameModel.Utils
{
    public interface IEntitySpawner
    {
        void SpawnEntity<T>(T entity) where T : IEntity;
    }
}