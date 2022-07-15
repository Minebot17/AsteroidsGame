using GameModel.Entities;
using GameModel.Entities.Player;
using GameModel.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace View.EntityViews
{
    public class Player : EntityView<PlayerEntity>
    {
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
            // TODO fire many times if holding down
            var isPressed = context.ReadValue<float>() > 0;
            if (isPressed)
            {
                Entity.TryFireBullet();
            }
        }

        public void HandleLaserAction(InputAction.CallbackContext context)
        {
            var isPressed = context.ReadValue<float>() > 0;
            if (isPressed)
            {
                Entity.TryFireLaser();
            }
        }
    }
}