using GameModel.Entities;

namespace GameModel.Core
{
    public class ScoreManager : IScoreManager
    {
        public int Score { get; private set; }
        
        public ScoreManager(IEntityManager entityManager)
        {
            entityManager.OnEntityDestroyed += OnEntityDestroyed;
        }

        private void OnEntityDestroyed(IEntity entity)
        {
            if (entity is IScoreSource scoreSource)
            {
                Score += scoreSource.Score;
            }
        }
    }
}