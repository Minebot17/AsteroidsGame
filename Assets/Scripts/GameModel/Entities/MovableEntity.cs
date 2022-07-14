using UnityEngine;

namespace GameModel.Entities
{
    public abstract class MovableEntity : Entity
    {
        protected readonly Vector2 Velocity;
        protected readonly float Torque;
        
        public MovableEntity(Vector2 position, Vector2 velocity, float torque)
        {
            Position = position;
            Velocity = velocity;
            Torque = torque;
        }
        
        public override void TickUpdate()
        {
            Position += Velocity;
            RotationAngle += Torque;
        }
    }
}