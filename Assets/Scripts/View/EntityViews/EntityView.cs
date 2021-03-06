using GameModel.Entities;
using UnityEngine;
using View.Utils;

namespace View.EntityViews
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
                    Debug.LogError($"Wrong entity model type. " +
                                   $"EntityViewType: {typeof(T)} EntityModelType: {value.GetType()}");
                }
                
                _entityModel = value;
                Entity = (T) value;
            }
        }

        public T Entity { get; private set; }
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
        
        // Cтолкновения бы вынес в отдельный контракт в виде интерфейса в конструктор модели, т.к. по сути это внешняя логика и её надо явно обозначить
        // Но передавать столкновения из каждого монобеха в единый объект без DI библиотеки (а вы попросили написать без фреймворков) будет не очень красиво
        // Поэтому передаю коллизии напрямую в ентити из вьюхи, хотя осознаю, что это не лучшее решение
        // Как вариант можно было написать столкновения в модели, но подумал что тут это излишне
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.TryGetComponent(out IEntityView entityView))
            {
                entityView.EntityModel.OnCollision(_entityModel);
            }
        }
    }
}