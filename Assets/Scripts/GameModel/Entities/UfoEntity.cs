using UnityEngine;

namespace GameModel.Entities
{
    public class UfoEntity : MovableEntity
    {
        private readonly IEntity _targetEntity;
        private readonly float _speed;
        
        public UfoEntity(IEntity targetEntity, float speed, Vector2 position) : base(position, Vector2.zero, 0)
        {
            _targetEntity = targetEntity;
            _speed = speed;
        }
        
        public override void TickUpdate()
        {
            var direction = _targetEntity.Position - Position;
            Velocity = direction.normalized * _speed;
            Position += Velocity;
        }

        public override void OnCollision(IEntity other)
        {
            if (other is BulletEntity)
            {
                Destroy();
            }
        }
    }
}