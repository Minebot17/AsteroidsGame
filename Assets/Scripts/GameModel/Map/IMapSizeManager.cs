using UnityEngine;

namespace GameModel.Map
{
    public interface IMapSizeManager
    {
        Vector2 MapSize { get; }

        Vector2 GetRandomPositionOnBorder();
    }
}