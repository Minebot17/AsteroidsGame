using System;
using GameModel;
using GameModel.Entities;
using UnityEngine;
using View.Utils;

namespace View
{
    public class Game : MonoBehaviour
    {
        [SerializeField]
        private GameObject _playerPrefab;
        
        [SerializeField]
        private GameObject _bigAsteroidPrefab;
        
        private IGameModel _gameModel;
        private EntitySpawner _entitySpawner;

        private void Start()
        {
            Camera cam = Camera.main;
            Vector3 leftRightCameraPoint = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));
            Vector2 mapSize = new Vector2(leftRightCameraPoint.x * 2f, leftRightCameraPoint.y * 2f);
            
            _gameModel = new GameModel.GameModel(mapSize);
            _entitySpawner = ConstructEntitySpawner();
            _gameModel.EntityManager.OnEntitySpawned += OnEntitySpawned;
            
            _gameModel.StartGame();
        }

        private void FixedUpdate()
        {
            _gameModel.FixedUpdate();
        }

        private void OnEntitySpawned(IEntity entity)
        {
            _entitySpawner.SpawnEntity(entity);
        }

        private EntitySpawner ConstructEntitySpawner()
        {
            EntitySpawner entitySpawner = new EntitySpawner();
            
            entitySpawner.RegisterEntityPrefab(typeof(PlayerEntity), _playerPrefab);
            entitySpawner.RegisterEntityPrefab(typeof(BigAsteroidEntity), _bigAsteroidPrefab);

            return entitySpawner;
        }
    }
}