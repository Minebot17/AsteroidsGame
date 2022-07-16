using System;
using System.Collections;
using GameModel;
using GameModel.Core;
using GameModel.Entities;
using GameModel.Entities.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using View.EntityViews;
using View.UI;
using View.Utils;

namespace View
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _ufoPrefab;
        [SerializeField] private GameObject _bigAsteroidPrefab;
        [SerializeField] private GameObject _smallAsteroidPrefab;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private GameObject _laserPrefab;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private PauseMenuController _pauseMenuController;

        private IEntityViewSpawner _entityViewSpawner;
        private bool _isPaused;

        public IGameModel GameModel { get; private set; }
        public Player Player { get; private set; }

        private void Awake()
        {
            var cam = Camera.main;
            var leftRightCameraPoint = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));
            var mapSize = new Vector2(leftRightCameraPoint.x * 2f, leftRightCameraPoint.y * 2f);
            
            _entityViewSpawner = ConstructEntitySpawner();
            _entityViewSpawner.OnEntityViewSpawned += OnEntityViewSpawned;
            GameModel = new GameModel.Core.GameModel(mapSize);
            GameModel.EntityManager.OnEntitySpawned += OnEntitySpawned;
            GameModel.StartGame();
            Player.EntityModel.OnDestroyed += OnPlayerDestroyed;
                
            // TODO вынести инпут в отдельный класс
            // TODO пофиксить баг с ghost entity из-за инпута (обрабатывать его после апдейта, или мб создание новых ентити вывести отдельно как и дестрой)
            // TODO вырубать все игровые экшены при поставке на паузу
            var moveAction = _playerInput.actions["Move"];
            moveAction.started += Player.HandleMoveAction;
            moveAction.performed += Player.HandleMoveAction;
            moveAction.canceled += Player.HandleMoveAction;
            
            var bulletAction = _playerInput.actions["Bullet"];
            bulletAction.performed += Player.HandleBulletAction;
            
            var laserAction = _playerInput.actions["Laser"];
            laserAction.performed += Player.HandleLaserAction;

            var pauseAction = _playerInput.actions["Pause"];
            pauseAction.performed += HandlePauseAction;
        }

        private void FixedUpdate()
        {
            if (!_isPaused)
            {
                GameModel.TickUpdate();
            }
        }

        public void SetPause(bool isPaused)
        {
            _isPaused = isPaused;
        }

        private void OnEntitySpawned(IEntity entity)
        {
            _entityViewSpawner.SpawnEntity(entity);
        }

        private void OnEntityViewSpawned(IEntityView entityView)
        {
            if (entityView is Player player)
            {
                Player = player;
            }
        }

        private IEntityViewSpawner ConstructEntitySpawner()
        {
            var entitySpawner = new EntityViewSpawner();
            
            entitySpawner.RegisterEntityPrefab(typeof(PlayerEntity), _playerPrefab);
            entitySpawner.RegisterEntityPrefab(typeof(UfoEntity), _ufoPrefab);
            entitySpawner.RegisterEntityPrefab(typeof(BigAsteroidEntity), _bigAsteroidPrefab);
            entitySpawner.RegisterEntityPrefab(typeof(SmallAsteroidEntity), _smallAsteroidPrefab);
            entitySpawner.RegisterEntityPrefab(typeof(BulletEntity), _bulletPrefab);
            entitySpawner.RegisterEntityPrefab(typeof(LaserEntity), _laserPrefab);

            return entitySpawner;
        }

        private void HandlePauseAction(InputAction.CallbackContext context)
        {
            if (!_isPaused)
            {
                _pauseMenuController.Show("Pause", false);
            }
        }

        private void OnPlayerDestroyed()
        {
            _pauseMenuController.Show("Game Over", true);
        }
    }
}