using UnityEngine;

namespace GameModel.Entities
{
    public abstract class MovableEntity : Entity
    {
        public Vector2 Velocity { get; set; }
        public float Torque { get; set; }

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