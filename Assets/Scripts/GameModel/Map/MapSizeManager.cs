using UnityEngine;

namespace GameModel.Map
{
    public class MapSizeManager : IMapSizeManager
    {
        public Vector2 MapSize { get; private set; }

        public MapSizeManager(Vector2 mapSize)
        {
            MapSize = mapSize;
        }

        public Vector2 GetRandomPositionOnBorder()
        {
            Vector2 mapSize = MapSize;
            int randomEdge = Random.Range(0, 4);
            Vector2 randomPosition = Vector2.zero;
            float randomX = Random.Range(-mapSize.x / 2f, mapSize.x / 2f);
            float randomY = Random.Range(-mapSize.y / 2f, mapSize.y / 2f);
            
            switch (randomEdge)
            {
                case 0:
                    randomPosition = new Vector2(randomX, -mapSize.y / 2f);
                    break;
                case 1:
                    randomPosition = new Vector2(mapSize.x / 2f, randomY);
                    break;
                case 2:
                    randomPosition = new Vector2(randomX, mapSize.y / 2f);
                    break;
                case 3:
                    randomPosition = new Vector2(-mapSize.x / 2f, randomY);
                    break;
            }
            
            return randomPosition;
        }
    }
}