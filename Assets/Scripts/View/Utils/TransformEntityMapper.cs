using GameModel.Entities;
using UnityEngine;

namespace View.Utils
{
    // Все entity в моделе обновляются в FixedUpdate, чтобы fps не влиял на скорость симуляции
    // Именно FixedUpdate вместо Update избавляет нас от надобности использования Time.deltaTime в моделе, и упращает просчёт движения (с учётом drag оно там не элементарное)
    // Однако если напрямую задавать позицию и вращение из модели в представление, то из-за разницы частоты обновления, движения объектов могут быть резкими
    // Поэтому сущность ниже отвечает за интерполяцию и установку значений из модели в представление для плавного перемещения объектов
    public class TransformEntityMapper : ITransformEntityMapper
    {
        private const float InterpolationMinDistance = 5f;
        
        private readonly Transform _transform;
        private readonly IEntity _entity;

        public TransformEntityMapper(Transform transform, IEntity entity)
        {
            _transform = transform;
            _entity = entity;
        }

        public void MapTransformFromEntity()
        {
            // Сила интерполяции зависит от разности между фремрейтом и тикрейтов
            // Например, если у нас фреймрейт больше в 2 раза, чем обновление модели, то вьюхи мы перемещаем только на половину расстояния к целевой позиции модели
            var interpolationModifier = Time.deltaTime / Time.fixedDeltaTime;
            
            if (Vector3.Distance(_transform.position, _entity.Position) < InterpolationMinDistance)
            {
                var maxInterpolationDelta =
                    Vector2.Distance(_entity.Position, _transform.position) * interpolationModifier;
                _transform.position = Vector2.MoveTowards(_transform.position, _entity.Position, maxInterpolationDelta);
            }
            else
            {
                _transform.position = _entity.Position;
            }
            
            var maxInterpolationDeltaRotation =
                Mathf.Abs(_transform.rotation.z - _entity.RotationAngle) * interpolationModifier;
            var interpolatedAngle = Mathf.MoveTowardsAngle(
                _transform.eulerAngles.z, _entity.RotationAngle, maxInterpolationDeltaRotation);
            _transform.eulerAngles = new Vector3(0, 0, interpolatedAngle);
        }
    }
}