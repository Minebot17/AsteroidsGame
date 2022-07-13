using System;
using System.Collections.Generic;
using GameModel.Entities;
using GameModel.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace View.Utils
{
    public class EntitySpawner : IEntitySpawner
    {
        private readonly Dictionary<Type, GameObject> _entityPrefabs = new();
        
        public void RegisterEntityPrefab(Type entityType, GameObject prefab)
        {
            _entityPrefabs.Add(entityType, prefab);
        }

        public void SpawnEntity<T>(T entity) where T : IEntity
        {
            IEntityView<T> entityView = Object.Instantiate(_entityPrefabs[entity.GetType()]).GetComponent<IEntityView<T>>();
            
            if (entityView == null)
            {
                Debug.LogError("EntityView not found");
                return;
            }

            entityView.EntityModel = entity;
        }
    }
}