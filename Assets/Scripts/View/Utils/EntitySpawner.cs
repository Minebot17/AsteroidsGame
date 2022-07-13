using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GameModel.Entities;
using GameModel.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace View.Utils
{
    public class EntitySpawner
    {
        private readonly Dictionary<Type, GameObject> _entityPrefabs = new();
        
        public void RegisterEntityPrefab(Type entityType, GameObject prefab)
        {
            _entityPrefabs.Add(entityType, prefab);
        }

        public void SpawnEntity<T>(T entity) where T : IEntity
        {
            Component[] components = Object.Instantiate(_entityPrefabs[entity.GetType()]).GetComponents<Component>();
            var entityView =  components.FirstOrDefault(c =>
            {
                var interfaces = c.GetType().GetInterfaces();
                var interfaceType = typeof(IEntityView<>);
                return interfaces.Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType);
            });
            
            if (entityView == null)
            {
                Debug.LogError("EntityView not found");
                return;
            }
            
            PropertyInfo entityModelInfo = entityView.GetType().GetProperty("EntityModel");
            entityModelInfo.SetValue(entityView, Convert.ChangeType(entity, entityModelInfo.PropertyType), null);
        }
    }
}