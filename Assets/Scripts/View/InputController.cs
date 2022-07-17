using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using View.UI;

namespace View
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private PauseMenuController _pauseMenuController;
        [SerializeField] private GameView _gameView;
        
        private readonly List<InputAction> _disableOnPauseActions = new();
        private InputAction _moveAction;
        private InputAction _bulletAction;
        private InputAction _laserAction;
        private InputAction _pauseAction;
        
        private void Awake()
        {
            _pauseMenuController.OnPauseChanged += OnPauseChanged;

            var player = _gameView.Player;
            _moveAction = _playerInput.actions["Move"];
            _moveAction.started += player.HandleMoveAction;
            _moveAction.performed += player.HandleMoveAction;
            _moveAction.canceled += player.HandleMoveAction;
            
            _bulletAction = _playerInput.actions["Bullet"];
            _bulletAction.started += player.HandleBulletAction;
            _bulletAction.canceled += player.HandleBulletAction;
            
            _laserAction = _playerInput.actions["Laser"];
            _laserAction.started += player.HandleLaserAction;
            _laserAction.canceled += player.HandleLaserAction;

            _pauseAction = _playerInput.actions["Pause"];
            _pauseAction.performed += HandlePauseAction;
            
            _disableOnPauseActions.AddRange(new []{ _moveAction, _bulletAction, _laserAction });
        }

        private void OnDestroy()
        {
            var player = _gameView.Player;
            _moveAction.started -= player.HandleMoveAction;
            _moveAction.performed -= player.HandleMoveAction;
            _moveAction.canceled -= player.HandleMoveAction;
            
            _bulletAction.started -= player.HandleBulletAction;
            _bulletAction.canceled -= player.HandleBulletAction;
            
            _laserAction.started -= player.HandleLaserAction;
            _laserAction.canceled -= player.HandleLaserAction;
            
            _pauseAction.performed -= HandlePauseAction;
        }

        private void HandlePauseAction(InputAction.CallbackContext context)
        {
            if (!_pauseMenuController.IsPaused)
            {
                _pauseMenuController.Show("Pause", false);
            }
            else
            {
                _pauseMenuController.Hide();
            }
        }

        private void OnPauseChanged(bool isPaused)
        {
            foreach (var action in _disableOnPauseActions)
            {
                if (isPaused)
                {
                    action.Disable();
                }
                else
                {
                    action.Enable();
                }
            }
        }
    }
}