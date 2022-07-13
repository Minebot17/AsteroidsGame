using System;
using UnityEngine;

namespace GameModel.Entities
{
    public abstract class Entity : IEntity
    {
        public event Action OnSelfDestroy;
        public event Action OnDestroyed;

        public Vector2 Position { get; protected set; }
        public float RotationAngle { get; protected set; }
        
        public abstract void FixedUpdate();

        protected void Destroy()
        {
            OnSelfDestroy?.Invoke();
        }
        
        public void Destroyed()
        {
            OnDestroyed?.Invoke();
        }
    }
}