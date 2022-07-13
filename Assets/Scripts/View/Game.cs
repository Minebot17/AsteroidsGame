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
        private EntitySpawner _entitySpawner;
        private Player _player;

        private void Start()
        {
            Camera cam = Camera.main;
            Vector3 leftRightCameraPoint = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));
            Vector2 mapSize = new Vector2(leftRightCameraPoint.x * 2f, leftRightCameraPoint.y * 2f);
            
            _gameModel = new GameModel.GameModel(mapSize, ConstructEntitySpawner());
            _player = FindObjectOfType<Player>(); // TODO better way to get player
                
            InputAction moveAction = _playerInput.actions["Move"];
            moveAction.started += _player.HandleMoveAction;
            moveAction.performed += _player.HandleMoveAction;
            moveAction.canceled += _player.HandleMoveAction;
        }

        private void FixedUpdate()
        {
            _gameModel.FixedUpdate();
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