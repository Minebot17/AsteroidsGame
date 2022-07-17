using GameModel.Entities.Weapons;
using GameModel.Utils;
using UnityEngine;

namespace GameModel.Entities
{
    public class PlayerEntity : MovableEntity
    {
        private const float SpawnWeaponsOffset = 1f;
        
        private readonly float _movingSpeed;
        private readonly float _rotationSpeed;
        private readonly float _dragModifier;
        private readonly BulletWeapon _bulletWeapon;
        private readonly LaserWeapon _laserWeapon;

        private bool _isMoving;
        
        public LaserWeapon LaserWeapon => _laserWeapon;

        private Vector2 ForwardVector => Vector2.up.Rotate(RotationAngle);

        public PlayerEntity(
            float movingSpeed, 
            float rotationSpeed, 
            float dragModifier, 
            BulletWeapon bulletWeapon,
            LaserWeapon laserWeapon
            ) : base(Vector2.zero, Vector2.zero, 0)
        {
            _movingSpeed = movingSpeed;
            _rotationSpeed = rotationSpeed;
            _dragModifier = dragModifier;
            _bulletWeapon = bulletWeapon;
            _laserWeapon = laserWeapon;
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
            _bulletWeapon.TickUpdate();
            _laserWeapon.TickUpdate();
        }

        public void TryFireBullet()
        {
            _bulletWeapon.TryFire(Position + ForwardVector * SpawnWeaponsOffset, ForwardVector);
        }

        public void TryFireLaser()
        {
            _laserWeapon.TryFire(Position + ForwardVector * SpawnWeaponsOffset, ForwardVector);
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
        
        protected override bool IsCanDestroyedBy(IEntity other)
        {
            return other is BigAsteroidEntity or SmallAsteroidEntity or UfoEntity;
        }
    }
}