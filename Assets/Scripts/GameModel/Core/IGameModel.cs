using GameModel.Map;

namespace GameModel.Core
{
    public interface IGameModel : IUpdatable
    {
        IEntityManager EntityManager { get; }
        IScoreManager ScoreManager { get; }
        
        void StartGame();
    }
}