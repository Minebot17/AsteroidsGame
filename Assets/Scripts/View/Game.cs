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
            _gameModel = new GameModel.GameModel();
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