using GameModel.Entities;

namespace GameModel.Utils
{
    public interface IEntitySpawner
    {
        void SpawnEntity<T>(T entity) where T : IEntity;
    }
}