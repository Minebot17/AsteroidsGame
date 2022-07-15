using GameModel.Map;

namespace GameModel.Entities.Factories
{
    public class MapUfoFactory : IEntityFactory<UfoEntity>
    {
        private readonly IEntity _targetEntity;
        private readonly IMapSizeManager _mapSizeManager;
        private readonly float _speed;

        public MapUfoFactory(IEntity targetEntity, IMapSizeManager mapSizeManager, float speed)
        {
            _targetEntity = targetEntity;
            _mapSizeManager = mapSizeManager;
            _speed = speed;
        }

        public UfoEntity Create()
        {
            return new UfoEntity(_targetEntity, _speed, _mapSizeManager.GetRandomPositionOnBorder());
        }
    }
}