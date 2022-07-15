using System.Collections;
using GameModel;
using GameModel.Entities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using View.EntityViews;
using View.Utils;

namespace View
{
    public class Game : MonoBehaviour
    {
        [SerializeField]
        private GameObject _playerPrefab;

        [SerializeField] 
        private GameObject _ufoPrefab;
        
        [SerializeField]
        private GameObject _bigAsteroidPrefab;

        [SerializeField] 
        private GameObject _smallAsteroidPrefab;
        
        [SerializeField]
        private GameObject _bulletPrefab;

        [SerializeField]
        private GameObject _laserPrefab;

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
            _player.EntityModel.OnDestroyed += () => StartCoroutine(RestartScene());
                
            // TODO вынести инпут в отдельный класс
            var moveAction = _playerInput.actions["Move"];
            moveAction.started += _player.HandleMoveAction;
            moveAction.performed += _player.HandleMoveAction;
            moveAction.canceled += _player.HandleMoveAction;
            
            var bulletAction = _playerInput.actions["Bullet"];
            bulletAction.performed += _player.HandleBulletAction;
            
            var laserAction = _playerInput.actions["Laser"];
            laserAction.performed += _player.HandleLaserAction;
        }

        private void FixedUpdate()
        {
            _gameModel.TickUpdate();
        }

        private void OnEntitySpawned(IEntity entity)
        {
            _entitySpawner.SpawnEntity(entity);
        }

        private IEntitySpawner ConstructEntitySpawner()
        {
            var entitySpawner = new EntitySpawner();
            
            entitySpawner.RegisterEntityPrefab(typeof(PlayerEntity), _playerPrefab);
            entitySpawner.RegisterEntityPrefab(typeof(UfoEntity), _ufoPrefab);
            entitySpawner.RegisterEntityPrefab(typeof(BigAsteroidEntity), _bigAsteroidPrefab);
            entitySpawner.RegisterEntityPrefab(typeof(SmallAsteroidEntity), _smallAsteroidPrefab);
            entitySpawner.RegisterEntityPrefab(typeof(BulletEntity), _bulletPrefab);
            entitySpawner.RegisterEntityPrefab(typeof(LaserEntity), _laserPrefab);

            return entitySpawner;
        }
        
        private IEnumerator RestartScene()
        {
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}