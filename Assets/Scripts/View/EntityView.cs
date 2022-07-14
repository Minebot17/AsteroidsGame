using System;
using GameModel.Entities;
using UnityEngine;
using View.Utils;

namespace View
{
    public class EntityView<T> : MonoBehaviour, IEntityView where T : IEntity
    {
        [SerializeField] 
        private Rigidbody2D _rigidbodyToEnable;
        
        private IEntity _entityModel;
        public IEntity EntityModel
        {
            get => _entityModel;
            set
            {
                if (value is not T)
                {
                    Debug.LogError($"Wrong entity model type. EntityViewType: {typeof(T)} EntityModelType: {value.GetType()}");
                }
                
                _entityModel = value;
                Entity = (T) value;
            }
        }
        
        protected T Entity;
        private ITransformEntityMapper _transformEntityMapper;

        protected virtual void Start()
        {
            _transformEntityMapper = new TransformEntityMapper(transform, EntityModel);
            EntityModel.OnDestroyed += () => Destroy(gameObject);
            
            transform.position = EntityModel.Position;
            transform.eulerAngles = new Vector3(0, 0, EntityModel.RotationAngle);
            
            if (_rigidbodyToEnable)
            {
                _rigidbodyToEnable.simulated = true;
            }
        }

        protected virtual void Update()
        {
            _transformEntityMapper.MapTransformFromEntity();
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.TryGetComponent(out IEntityView entityView))
            {
                entityView.EntityModel.OnCollision(_entityModel);
            }
        }
    }
}