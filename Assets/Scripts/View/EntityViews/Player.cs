using System;
using GameModel.Entities;
using GameModel.Entities.Player;
using GameModel.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace View.EntityViews
{
    public class Player : EntityView<PlayerEntity>
    {
        private bool _bulletIsPressed;
        private bool _laserIsPressed;

        private void FixedUpdate()
        {
            if (_bulletIsPressed)
            {
                Entity.TryFireBullet();
            }
            
            if (_laserIsPressed)
            {
                Entity.TryFireLaser();
            }
        }

        public void HandleMoveAction(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            Entity.SetMovingState(direction.y > 0);
            Entity.SetRotationState(
                direction.x < 0 
                    ? RotationState.Left 
                    : direction.x > 0 
                        ? RotationState.Right
                        : RotationState.None);
        }

        public void HandleBulletAction(InputAction.CallbackContext context)
        {
            _bulletIsPressed = context.started;
        }

        public void HandleLaserAction(InputAction.CallbackContext context)
        {
            _laserIsPressed = context.started;
        }
    }
}