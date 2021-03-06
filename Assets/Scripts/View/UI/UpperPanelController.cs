using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View.UI
{
    public class UpperPanelController : MonoBehaviour
    {
        [SerializeField] private GameView _gameView;
        [SerializeField] private GameObject _laserCellPrefab;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _coordXText;
        [SerializeField] private TextMeshProUGUI _coordYText;
        [SerializeField] private TextMeshProUGUI _angleText;
        [SerializeField] private TextMeshProUGUI _speedText;
        [SerializeField] private RectTransform _laserCellsContainer;

        private readonly List<Image> _laserCellImages = new();

        private void Start()
        {
            var player = _gameView.Player.Entity;
            for (var i = 0; i < player.LaserWeapon.MaxLaserCharges; i++)
            {
                var chargeImage = Instantiate(_laserCellPrefab, _laserCellsContainer);
                _laserCellImages.Add(chargeImage.GetComponent<Image>());
            }
        }

        private void FixedUpdate()
        {
            var player = _gameView.Player.Entity;
            _scoreText.text = $"Score: {_gameView.GameModel.ScoreManager.Score}";
            _coordXText.text = $"X: {(int) player.Position.x}";
            _coordYText.text = $"Y: {(int) player.Position.y}";
            _angleText.text = $"Angle: {(player.RotationAngle > 0 ? player.RotationAngle % 360 : 360 + player.RotationAngle % 360)}°";
            _speedText.text = $"Speed: {(int) (player.Velocity.magnitude / Time.fixedDeltaTime)} m/s";
            
            for (var i = 0; i < player.LaserWeapon.MaxLaserCharges; i++)
            {
                var chargeImage = _laserCellImages[i];
                chargeImage.fillAmount = player.LaserWeapon.CurrentLaserCharges > i 
                    ? 1
                    : player.LaserWeapon.CurrentLaserCharges < i 
                        ? 0
                        : 1 - (float) player.LaserWeapon.CurrentLaserChargeCooldown / player.LaserWeapon.LaserChargeCooldown;
            }
        }
    }
}