using UnityEngine;

namespace GameModel.Entities
{
    public abstract class MovableEntity : Entity
    {
        private readonly Vector2 _velocity;
        private readonly float _torque;
        
        public MovableEntity(Vector2 position, Vector2 velocity, float torque)
        {
            Position = position;
            _velocity = velocity;
            _torque = torque;
        }
        
        public override void FixedUpdate()
        {
            Position += _velocity;
            RotationAngle += _torque;
        }
    }
}