using System;
using GameModel.Map;
using GameModel.Utils;
using UnityEngine;

namespace GameModel.Entities
{
    public class PlayerEntity : Entity
    {
        private const float SpawnWeaponsOffset = 1f;

        private readonly IMapSizeManager _mapSizeManager;
        private readonly IEntityManager _entityManager;
        private readonly float _movingSpeed;
        private readonly float _rotationSpeed;
        private readonly float _dragModifier;
        private readonly int _bulletCooldown;
        private readonly int _laserFireCooldown;
        private readonly int _laserChargeCooldown;
        private readonly int _maxLaserCharges;
        
        private Vector2 _currentMovingVector;
        private RotationState _rotationState;
        private bool _isMoving;
        private int _currentBulletCooldown;
        private int _currentLaserFireCooldown;
        private int _currentLaserChargeCooldown;
        private int _currentLaserCharges;
        
        private Vector2 ForwardVector => Vector2.up.Rotate(RotationAngle);

        public PlayerEntity(
            IMapSizeManager mapSizeManager,
            IEntityManager entityManager, 
            float movingSpeed, 
            float rotationSpeed, 
            float dragModifier, 
            int bulletCooldown,
            int laserFireCooldown,
            int laserChargeCooldown,
            int maxLaserCharges)
        {
            _mapSizeManager = mapSizeManager;
            _entityManager = entityManager;
            _movingSpeed = movingSpeed;
            _rotationSpeed = rotationSpeed;
            _dragModifier = dragModifier;
            _bulletCooldown = bulletCooldown;
            _laserFireCooldown = laserFireCooldown;
            _laserChargeCooldown = laserChargeCooldown;
            _maxLaserCharges = maxLaserCharges;
            _currentLaserCharges = _maxLaserCharges;
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

            // Cooldown weapons and laser charges
            if (_currentBulletCooldown > 0)
            {
                _currentBulletCooldown--;
            }

            if (_currentLaserFireCooldown > 0)
            {
                _currentLaserFireCooldown--;
            }
            
            if (_currentLaserChargeCooldown > 0)
            {
                _currentLaserChargeCooldown--;
            }
            else if (_currentLaserCharges < _maxLaserCharges)
            {
                _currentLaserCharges++;
                _currentLaserChargeCooldown = _laserChargeCooldown;
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
                    Position + ForwardVector * SpawnWeaponsOffset, ForwardVector, 100, 0.5f);
                
                _entityManager.SpawnEntity(bullet);
            }
        }

        public void TryFireLaser()
        {
            if (_currentLaserFireCooldown <= 0 && _currentLaserCharges > 0)
            {
                _currentLaserFireCooldown = _laserFireCooldown;
                _currentLaserCharges--;
                
                var laser = new LaserEntity(_mapSizeManager, 
                    Position + ForwardVector * SpawnWeaponsOffset, ForwardVector);
                
                _entityManager.SpawnEntity(laser);
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