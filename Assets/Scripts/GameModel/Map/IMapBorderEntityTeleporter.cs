using System.Collections.Generic;
using GameModel.Entities;

namespace GameModel.Map
{
    public interface IMapBorderEntityTeleporter
    {
        void TeleportEntities(IEnumerable<IEntity> entities);
    }
}