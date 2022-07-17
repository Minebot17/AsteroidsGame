using GameModel.Core;
using GameModel.Entities.Weapons;
using UnityEngine;

namespace GameModel.Entities
{
    public class UfoEntity : MovableEntity, IScoreSource
    {
        private readonly IEntity _targetEntity;
        private readonly float _speed;

        public int Score { get; }

        public UfoEntity(IEntity targetEntity, float speed, Vector2 position, int score) 
            : base(position, Vector2.zero, 0)
        {
            _targetEntity = targetEntity;
            _speed = speed;
            Score = score;
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