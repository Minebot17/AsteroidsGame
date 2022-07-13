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
            
            transform.position = EntityModel.Position; // TODO remove copy-paste?
            transform.eulerAngles = new Vector3(0, 0, EntityModel.RotationAngle);
        }

        private void Update()
        {
            _transformEntityMapper.MapTransformFromEntity();
        }
        
        public void HandleMoveAction(InputAction.CallbackContext context)
        {
            Vector2 direction = context.ReadValue<Vector2>();
            EntityModel.SetMovingState(direction.y > 0);
            EntityModel.SetRotationState(
                direction.x < 0 
                    ? RotationState.Left 
                    : direction.x > 0 
                        ? RotationState.Right
                        : RotationState.None);
        }
    }
}