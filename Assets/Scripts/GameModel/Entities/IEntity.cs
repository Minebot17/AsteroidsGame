using System;
using UnityEngine;

namespace GameModel.Entities
{
    public interface IEntity : IUpdatable
    {
        event Action OnSelfDestroy;
        event Action OnDestroyed;
        
        Vector2 Position { get; set; }
        float RotationAngle { get; set; }
        
        void Destroyed();
    }
}