using System;
using GameModel.Utils;
using UnityEngine;

namespace View.Utils
{
    [Serializable]
    [CreateAssetMenu(fileName = "GameSettingsAsset", menuName = "ScriptableObjects/GameSettingsAsset")]
    public class GameSettings : ScriptableObject, IGameSettings
    {
        [Header("Player Moving")]
        [SerializeField] private float _playerMovingSpeed = 0.0075f;
        [SerializeField] private float _playerRotationSpeed = 4f;
        [SerializeField] private float _playerDragModifier = 0.987f;
        
        [Header("Player Weapons")]
        [SerializeField] private int _bulletFireCooldown = 10;
        [SerializeField] private int _bulletLifeDuration = 100;
        [SerializeField] private float _bulletSpeed = 0.5f;
        [SerializeField] private int _laserFireCooldown = 50;
        [SerializeField] private int _laserChargeCooldown = 200;
        [SerializeField] private int _maxLaserCharges = 4;

        [Header("Map")] 
        [SerializeField] private float _bigAsteroidsSpeed = 0.5f;
        [SerializeField] private float _bigAsteroidsTorque = 1f;
        [SerializeField] private int _bigAsteroidsFragmentsCount = 3;
        [SerializeField] private float _ufoSpeed = 0.08f;
        [SerializeField] private int _maxBigAsteroidsCount = 4;
        [SerializeField] private int _spawnBigAsteroidPeriod = 40;
        [SerializeField] private int _maxUfosCount = 1;
        [SerializeField] private int _spawnUfoPeriod = 400;
        [SerializeField] private float _teleporterBorderOffset = 2f;
        
        [Header("Score")]
        [SerializeField] private int _scoreBigAsteroid = 5;
        [SerializeField] private int _scoreSmallAsteroid = 2;
        [SerializeField] private int _scoreUfo = 15;

        public float PlayerMovingSpeed => _playerMovingSpeed;
        public float PlayerRotationSpeed => _playerRotationSpeed;
        public float PlayerDragModifier => _playerDragModifier;

        public int BulletFireCooldown => _bulletFireCooldown;
        public int BulletLifeDuration => _bulletLifeDuration;
        public float BulletSpeed => _bulletSpeed;
        public int LaserFireCooldown => _laserFireCooldown;
        public int LaserChargeCooldown => _laserChargeCooldown;
        public int MaxLaserCharges => _maxLaserCharges;

        public float BigAsteroidsSpeed => _bigAsteroidsSpeed;
        public float BigAsteroidsTorque => _bigAsteroidsTorque;
        public int BigAsteroidsFragmentsCount => _bigAsteroidsFragmentsCount;
        public float UfoSpeed => _ufoSpeed;
        public int MaxBigAsteroidsCount => _maxBigAsteroidsCount;
        public int SpawnBigAsteroidPeriod => _spawnBigAsteroidPeriod;
        public int MaxUfosCount => _maxUfosCount;
        public int SpawnUfoPeriod => _spawnUfoPeriod;
        public float TeleporterBorderOffset => _teleporterBorderOffset;

        public int ScoreBigAsteroid => _scoreBigAsteroid;
        public int ScoreSmallAsteroid => _scoreSmallAsteroid;
        public int ScoreUfo => _scoreUfo;

        public Vector2 MapSize { get; set; }
    }
}