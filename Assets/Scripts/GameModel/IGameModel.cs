using GameModel.Entities;
using GameModel.Map;

namespace GameModel
{
    public interface IGameModel : IUpdatable
    {
        IEntityManager EntityManager { get; }
        
        void StartGame();
    }
}