using System.Collections.Generic;
using GameModel.Entities;
using GameModel.Map;
using GameModel.Utils;

namespace GameModel
{
    public class GameModel : IGameModel
    {
        private readonly IEntitySpawner _entitySpawner;
        private readonly IUpdatable _asteroidsSpawner;

        public GameModel(IEntitySpawner entitySpawner)
        {
            _entitySpawner = entitySpawner;
            _asteroidsSpawner = new AsteroidsSpawner(5, 40);
        }
        
        public void FixedUpdate()
        {
            _asteroidsSpawner.FixedUpdate();
        }
    }
}