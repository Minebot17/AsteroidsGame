using System;
using UnityEngine;

namespace GameModel.Entities
{
    public abstract class Entity : IEntity
    {
        public event Action OnSelfDestroy;
        public event Action OnDestroyed;

        public Vector2 Position { get; set; }
        public float RotationAngle { get; set; }
        
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