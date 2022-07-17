using GameModel.Map;

namespace GameModel.Entities.Factories
{
    public class MapUfoFactory : IEntityFactory<UfoEntity>
    {
        private readonly IEntity _targetEntity;
        private readonly IMapSizeManager _mapSizeManager;
        private readonly float _speed;
        private readonly int _score;

        public MapUfoFactory(IEntity targetEntity, IMapSizeManager mapSizeManager, float speed, int score)
        {
            _targetEntity = targetEntity;
            _mapSizeManager = mapSizeManager;
            _speed = speed;
            _score = score;
        }

        public UfoEntity Create()
        {
            return new UfoEntity(_targetEntity, _speed, _mapSizeManager.GetRandomPositionOnBorder(), _score);
        }
    }
}