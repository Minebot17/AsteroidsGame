using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View.UI
{
    public class UpperPanelView : MonoBehaviour
    {
        [SerializeField] private GameObject _laserCellPrefab;
        [SerializeField] private TextMeshProUGUI _coordXText;
        [SerializeField] private TextMeshProUGUI _coordYText;
        [SerializeField] private TextMeshProUGUI _angleText;
        [SerializeField] private TextMeshProUGUI _speedText;
        [SerializeField] private RectTransform _laserCellsContainer;

        private readonly List<Image> _laserCellImages = new();

        private void Start()
        {
            var player = GameView.Instance.Player.Entity;
            for (var i = 0; i < player.MaxLaserCharges; i++)
            {
                var chargeImage = Instantiate(_laserCellPrefab, _laserCellsContainer);
                _laserCellImages.Add(chargeImage.GetComponent<Image>());
            }
        }

        private void FixedUpdate()
        {
            var player = GameView.Instance.Player.Entity;
            _coordXText.text = $"X: {(int) player.Position.x}";
            _coordYText.text = $"Y: {(int) player.Position.y}";
            _angleText.text = $"Angle: {(player.RotationAngle > 0 ? player.RotationAngle % 360 : 360 + player.RotationAngle % 360)}°";
            _speedText.text = $"Speed: {(int) (player.Velocity.magnitude / Time.fixedDeltaTime)} m/s";
            
            for (var i = 0; i < player.MaxLaserCharges; i++)
            {
                var chargeImage = _laserCellImages[i];
                chargeImage.fillAmount = player.CurrentLaserCharges > i 
                    ? 1
                    : player.CurrentLaserCharges < i 
                        ? 0
                        : 1 - (float) player.CurrentLaserChargeCooldown / player.LaserChargeCooldown;
            }
        }
    }
}