using System;
using GameModel.Map;
using GameModel.Utils;
using UnityEngine;

namespace GameModel.Entities
{
    public class PlayerEntity : Entity, ICollidable
    {
        private const float SpawnBulletOffset = 0.8f;

        private readonly IEntityManager _entityManager;
        private readonly float _movingSpeed;
        private readonly float _rotationSpeed;
        private readonly float _dragModifier;
        private readonly int _bulletCooldown;
        
        private Vector2 _currentMovingVector;
        private RotationState _rotationState;
        private bool _isMoving;
        private float _currentBulletCooldown;
        
        private Vector2 ForwardVector => Vector2.up.Rotate(RotationAngle);

        public PlayerEntity(
            IEntityManager entityManager, 
            float movingSpeed, 
            float rotationSpeed, 
            float dragModifier, 
            int bulletCooldown)
        {
            _entityManager = entityManager;
            _movingSpeed = movingSpeed;
            _rotationSpeed = rotationSpeed;
            _dragModifier = dragModifier;
            _bulletCooldown = bulletCooldown;
        }

        public override void TickUpdate()
        {
            // Apply moving input to accelerate
            if (_isMoving)
            {
                _currentMovingVector += _movingSpeed * ForwardVector;
            }

            // Apply drag and move player by acceleration
            _currentMovingVector *= _dragModifier;
            Position += _currentMovingVector;

            // Rotate player from input
            if (_rotationState != RotationState.None)
            {
                RotationAngle += _rotationSpeed * (_rotationState == RotationState.Left ? 1 : -1);
            }

            if (_currentBulletCooldown > 0)
            {
                _currentBulletCooldown--;
            }
        }

        public override void OnCollision(ICollidable other)
        {
            if (other is BigAsteroidEntity or SmallAsteroidEntity)
            {
                Destroy();
            }
        }

        public void TryFireBullet()
        {
            if (_currentBulletCooldown <= 0)
            {
                _currentBulletCooldown = _bulletCooldown;
                
                var bullet = new BulletEntity(
                    Position + ForwardVector * SpawnBulletOffset, ForwardVector, 80, 0.5f);
                
                _entityManager.SpawnEntity(bullet);
            }
        }

        public void SetMovingState(bool isMoving)
        {
            _isMoving = isMoving;
        }
        
        public void SetRotationState(RotationState rotationState)
        {
            _rotationState = rotationState;
        }
    }
}