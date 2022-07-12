using UnityEngine;

namespace GameModel.Entities
{
    public interface IEntity
    {
        Vector2 Position { get; }
        float RotationAngle { get; }
    }
}