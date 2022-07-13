using System;
using GameModel.Entities;
using UnityEngine;
using UnityEngine.InputSystem;
using View.Utils;

namespace View
{
    public class Player : MonoBehaviour, IEntityView<PlayerEntity>
    {
        public PlayerEntity EntityModel { get; set; }
        
        private ITransformEntityMapper _transformEntityMapper;

        private void Start()
        {
            _transformEntityMapper = new TransformEntityMapper(transform, EntityModel);
            EntityModel.OnDestroyed += () => Destroy(gameObject);
            
            transform.position = EntityModel.Position;
            transform.eulerAngles = new Vector3(0, 0, EntityModel.RotationAngle);
        }

        private void Update()
        {
            _transformEntityMapper.MapTransformFromEntity();
        }

        private void FixedUpdate()
        {
            // TODO переделать инпут на норм экшены
            EntityModel.SetMovingState(Keyboard.current.wKey.isPressed);
            EntityModel.SetRotationState(
                Keyboard.current.aKey.isPressed 
                ? RotationState.Left 
                : Keyboard.current.dKey.isPressed 
                    ? RotationState.Right
                    : RotationState.None);
        }
    }
}