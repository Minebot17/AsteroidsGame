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
        
        public abstract void TickUpdate();
        protected abstract bool IsCanDestroyedBy(IEntity other);
        
        public void OnCollision(IEntity other)
        {
            if (IsCanDestroyedBy(other))
            {
                Destroy();
            }
        }

        public virtual void Destroy()
        {
            OnSelfDestroy?.Invoke();
        }
        
        public virtual void Destroyed()
        {
            OnDestroyed?.Invoke();
        }
    }
}