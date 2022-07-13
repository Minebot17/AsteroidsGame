using System.Collections.Generic;
using GameModel.Entities;
using UnityEngine;

namespace GameModel.Map
{
    public class MapBorderEntityTeleporter : IMapBorderEntityTeleporter, IUpdatable
    {
        private readonly IEntityManager _entityManager;
        private readonly Vector2 _minBorders;
        private readonly Vector2 _maxBorders;

        public MapBorderEntityTeleporter(IEntityManager entityManager, IMapSizeManager mapSizeManager, float borderOffset)
        {
            _entityManager = entityManager;
            _minBorders = new Vector2(-mapSizeManager.MapSize.x / 2f - borderOffset, -mapSizeManager.MapSize.y / 2f - borderOffset);
            _maxBorders = new Vector2(mapSizeManager.MapSize.x / 2f + borderOffset, mapSizeManager.MapSize.y / 2f + borderOffset);
        }

        public void TeleportEntities(IEnumerable<IEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.Position.x < _minBorders.x)
                {
                    entity.Position = new Vector2(_maxBorders.x, entity.Position.y);
                }
                else if (entity.Position.x > _maxBorders.x)
                {
                    entity.Position = new Vector2(_minBorders.x, entity.Position.y);
                }
                else if (entity.Position.y < _minBorders.y)
                {
                    entity.Position = new Vector2(entity.Position.x, _maxBorders.y);
                }
                else if (entity.Position.y > _maxBorders.y)
                {
                    entity.Position = new Vector2(entity.Position.x, _minBorders.y);
                }
            }
        }

        public void FixedUpdate()
        {
            TeleportEntities(_entityManager.Entities);
        }
    }
}