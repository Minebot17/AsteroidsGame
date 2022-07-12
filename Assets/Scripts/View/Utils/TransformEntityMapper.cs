using GameModel.Entities;
using UnityEngine;

namespace View.Utils
{
    // Все entity в моделе обновляются в FixedUpdate, чтобы fps не влиял на скорость симуляции
    // Именно FixedUpdate вместо Update избавляет нас от надобности использования Time.deltaTime в моделе, и упращает просчёт движения (с учётом drag оно там не элементарное)
    // Однако если напрямую задавать позицию и вращение из модели в представление, то из-за разницы частоты обновления, движения объектов могут быть резкими
    // Поэтому сущность ниже будет отвечать за интерполяцию и установку значений из модели в представление для плавного перемещения объектов
    public class TransformEntityMapper : ITransformEntityMapper
    {
        private const float PositionInterpolationSpeed = 5f;
        
        private readonly Transform _transform;
        private readonly IEntity _entity;

        public TransformEntityMapper(Transform transform, IEntity entity)
        {
            _transform = transform;
            _entity = entity;
        }

        public void MapTransformFromEntity()
        {
            _transform.position = Vector3.Lerp(_transform.position, _entity.Position, Time.deltaTime * PositionInterpolationSpeed);
            _transform.eulerAngles = new Vector3(0, 0, _entity.RotationAngle);
        }
    }
}