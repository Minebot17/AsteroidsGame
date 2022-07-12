using System;
using GameModel.Entities;
using UnityEngine;
using UnityEngine.InputSystem;
using View.Utils;

namespace View
{
    public class Player : MonoBehaviour
    {
        private IPlayerLogic _playerLogic;
        private ITransformEntityMapper _transformEntityMapper;

        private void Start()
        {
            _playerLogic = new PlayerLogic(0.005f, 4f, 0.98f);
            _transformEntityMapper = new TransformEntityMapper(transform, _playerLogic);
        }

        private void Update()
        {
            _transformEntityMapper.MapTransformFromEntity();
        }

        private void FixedUpdate()
        {
            // TODO переделать инпут на норм экшены
            _playerLogic.SetMovingState(Keyboard.current.wKey.isPressed);
            _playerLogic.SetRotationState(
                Keyboard.current.aKey.isPressed 
                ? RotationState.Left 
                : Keyboard.current.dKey.isPressed 
                    ? RotationState.Right
                    : RotationState.None);
            
            _playerLogic.FixedUpdate();
        }
    }
}