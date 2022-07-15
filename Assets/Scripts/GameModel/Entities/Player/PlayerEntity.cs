using GameModel.Core;
using GameModel.Map;
using GameModel.Utils;
using UnityEngine;

namespace GameModel.Entities.Player
{
    public class PlayerEntity : MovableEntity
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
        
        private bool _isMoving;
        private int _currentBulletCooldown;
        private int _currentLaserFireCooldown;
        private int _currentLaserChargeCooldown;
        private int _currentLaserCharges;
        
        public int CurrentLaserCharges => _currentLaserCharges;
        public int MaxLaserCharges => _maxLaserCharges;
        public int CurrentLaserChargeCooldown => _currentLaserChargeCooldown;
        public int LaserChargeCooldown => _laserChargeCooldown;
        
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
            int maxLaserCharges
            ) : base(Vector2.zero, Vector2.zero, 0)
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
            _currentLaserChargeCooldown = _laserChargeCooldown;
        }

        public override void TickUpdate()
        {
            // Apply moving input to accelerate
            if (_isMoving)
            {
                Velocity += _movingSpeed * ForwardVector;
            }

            // Apply drag and move player by acceleration and rotation
            Velocity *= _dragModifier;
            Position += Velocity;
            RotationAngle += Torque;

            // Cooldown weapons and laser charges
            if (_currentBulletCooldown > 0)
            {
                _currentBulletCooldown--;
            }

            if (_currentLaserFireCooldown > 0)
            {
                _currentLaserFireCooldown--;
            }

            if (_currentLaserCharges < _maxLaserCharges)
            {
                if (_currentLaserChargeCooldown > 0)
                {
                    _currentLaserChargeCooldown--;
                }
                else 
                {
                    _currentLaserCharges++;
                    _currentLaserChargeCooldown = _laserChargeCooldown;
                }
            }
        }

        public override void OnCollision(IEntity other)
        {
            if (other is BigAsteroidEntity or SmallAsteroidEntity or UfoEntity)
            {
                Destroy();
            }
        }

        // TODO абстрагировать оружия от игрока, обязательно
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
            Torque = rotationState switch
            {
                RotationState.Left => _rotationSpeed,
                RotationState.Right => -_rotationSpeed,
                _ => 0
            };
        }
    }
}