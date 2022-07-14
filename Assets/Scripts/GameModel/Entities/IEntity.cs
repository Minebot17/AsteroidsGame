using System;
using UnityEngine;

namespace GameModel.Entities
{
    public interface IEntity : IUpdatable, ICollidable
    {
        event Action OnSelfDestroy;
        event Action OnDestroyed;
        
        Vector2 Position { get; set; }
        float RotationAngle { get; set; }

        void Destroy();
        void Destroyed();
    }
}