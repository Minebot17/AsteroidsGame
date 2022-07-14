using System.Collections.Generic;
using GameModel.Entities;
using UnityEngine;

namespace View.EntityViews
{
    public class Laser : EntityView<LaserEntity>
    {
        [SerializeField] 
        private LineRenderer _lineRenderer;

        protected override void Start()
        {
            base.Start();

            var linePositions = new Vector3[]
            {
                EntityModel.Position,
                EntityModel.Position + Entity.Direction * Entity.MaxLaserDistance
            };
            
            _lineRenderer.SetPositions(linePositions);
            var raycastHits = 
                Physics2D.RaycastAll(Entity.Position, Entity.Direction, Entity.MaxLaserDistance);

            foreach (var hit in raycastHits)
            {
                if (hit.collider.TryGetComponent(out IEntityView entityView))
                {
                    Entity.OnCollision(entityView.EntityModel);
                }
            }
        }
    }
}