using System;
using GameModel.Utils;
using UnityEngine;

namespace GameModel.Entities
{
    public class PlayerEntity : Entity
    {
        private readonly float _movingSpeed;
        private readonly float _rotationSpeed;
        private readonly float _dragModifier;
        
        private Vector2 _currentMovingVector;
        private RotationState _rotationState;
        private bool _isMoving;

        public PlayerEntity(float movingSpeed, float rotationSpeed, float dragModifier)
        {
            _movingSpeed = movingSpeed;
            _rotationSpeed = rotationSpeed;
            _dragModifier = dragModifier;
        }

        public override void FixedUpdate()
        {
            // Apply moving input to accelerate
            if (_isMoving)
            {
                _currentMovingVector += _movingSpeed * Vector2.up.Rotate(RotationAngle);
            }

            // Apply drag and move player by acceleration
            _currentMovingVector *= _dragModifier;
            Position += _currentMovingVector;

            // Rotate player from input
            if (_rotationState != RotationState.None)
            {
                RotationAngle += _rotationSpeed * (_rotationState == RotationState.Left ? 1 : -1);
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