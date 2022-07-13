using GameModel.Entities;
using UnityEngine;
using View.Utils;

namespace View
{
    public class BigAsteroid : MonoBehaviour, IEntityView<BigAsteroidEntity>
    {
        public BigAsteroidEntity EntityModel { get; set; }
        
        private ITransformEntityMapper _transformEntityMapper;

        private void Start()
        {
            _transformEntityMapper = new TransformEntityMapper(transform, EntityModel);
            EntityModel.OnDestroyed += () => Destroy(gameObject);

            transform.position = EntityModel.Position;
            transform.eulerAngles = new Vector3(0, 0, EntityModel.RotationAngle);
        }

        private void Update()
        {
            _transformEntityMapper.MapTransformFromEntity();
        }
    }
}