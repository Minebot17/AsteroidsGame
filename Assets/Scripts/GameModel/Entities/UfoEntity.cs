using GameModel.Core;
using GameModel.Entities.Weapons;
using UnityEngine;

namespace GameModel.Entities
{
    public class UfoEntity : MovableEntity, IScoreSource
    {
        private readonly IEntity _targetEntity;
        private readonly float _speed;

        public int Score => 15;
        
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

        protected override bool IsCanDestroyedBy(IEntity other)
        {
            return other is BulletEntity or LaserEntity;
        }
    }
}