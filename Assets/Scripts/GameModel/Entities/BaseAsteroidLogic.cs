using UnityEngine;

namespace GameModel.Entities
{
    public abstract class BaseAsteroidLogic : IAsteroidLogic
    {
        public abstract Vector2 Position { get; }
        public abstract float RotationAngle { get; }
        public abstract void FixedUpdate();
    }
}