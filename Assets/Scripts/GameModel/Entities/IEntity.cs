using UnityEngine;

namespace GameModel.Entities
{
    public interface IEntity : IUpdatable
    {
        Vector2 Position { get; }
        float RotationAngle { get; }
    }
}