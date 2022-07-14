using System;
using GameModel.Entities;
using UnityEngine;
using UnityEngine.InputSystem;
using View.Utils;

namespace View
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
    }
}