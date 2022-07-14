using System;
using GameModel;
using GameModel.Entities;
using UnityEngine;
using UnityEngine.InputSystem;
using View.Utils;

namespace View
{
    public class Game : MonoBehaviour
    {
        [SerializeField]
        private GameObject _playerPrefab;
        
        [SerializeField]
        private GameObject _bigAsteroidPrefab;

        [SerializeField] 
        private PlayerInput _playerInput;
        
        private IGameModel _gameModel;
        private IEntitySpawner _entitySpawner;
        private Player _player;

        private void Start()
        {
            var cam = Camera.main;
            var leftRightCameraPoint = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));
            var mapSize = new Vector2(leftRightCameraPoint.x * 2f, leftRightCameraPoint.y * 2f);
            
            _entitySpawner = ConstructEntitySpawner();
            _gameModel = new GameModel.GameModel(mapSize);
            _gameModel.EntityManager.OnEntitySpawned += OnEntitySpawned;
            _gameModel.StartGame();
            _player = FindObjectOfType<Player>(); // TODO better way to get player
                
            var moveAction = _playerInput.actions["Move"];
            moveAction.started += _player.HandleMoveAction;
            moveAction.performed += _player.HandleMoveAction;
            moveAction.canceled += _player.HandleMoveAction;
        }

        private void FixedUpdate()
        {
            _gameModel.FixedUpdate();
        }

        private void OnEntitySpawned(IEntity entity)
        {
            _entitySpawner.SpawnEntity(entity);
        }

        private IEntitySpawner ConstructEntitySpawner()
        {
            var entitySpawner = new EntitySpawner();
            
            entitySpawner.RegisterEntityPrefab(typeof(PlayerEntity), _playerPrefab);
            entitySpawner.RegisterEntityPrefab(typeof(BigAsteroidEntity), _bigAsteroidPrefab);

            return entitySpawner;
        }
    }
}