using GameModel.Map;
using UnityEngine;

namespace GameModel.Entities.Weapons
{
    public class LaserEntity : Entity
    {
        private const int Lifetime = 4;

        private int _remainingLifetime = Lifetime;

        public Vector2 Direction { get; }
        public float MaxLaserDistance { get; }

        public LaserEntity(IMapSizeManager mapSizeManager, Vector2 position, Vector2 direction)
        {
            MaxLaserDistance = mapSizeManager.MapSize.x > mapSizeManager.MapSize.y 
                ? mapSizeManager.MapSize.x 
                : mapSizeManager.MapSize.y;
            
            Position = position;
            Direction = direction;
        }

        public override void TickUpdate()
        {
            if (_remainingLifetime <= 0)
            {
                Destroy();
            }
            else
            {
                _remainingLifetime--;
            }
        }

        protected override bool IsCanDestroyedBy(IEntity other)
        {
            return false;
        }
    }
}