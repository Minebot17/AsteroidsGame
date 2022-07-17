using GameModel.Core;
using GameModel.Map;
using UnityEngine;

namespace GameModel.Entities.Weapons
{
    public class LaserWeapon : Weapon
    {
        private readonly IMapSizeManager _mapSizeManager;
        private readonly int _laserChargeCooldown;
        private readonly int _maxLaserCharges;
        
        private int _currentLaserChargeCooldown;
        private int _currentLaserCharges;

        public override bool IsCanFire => CurrentFireCooldown <= 0 && _currentLaserCharges > 0;
        
        public int CurrentLaserCharges => _currentLaserCharges;
        public int MaxLaserCharges => _maxLaserCharges;
        public int CurrentLaserChargeCooldown => _currentLaserChargeCooldown;
        public int LaserChargeCooldown => _laserChargeCooldown;
        
        public LaserWeapon(
            IMapSizeManager mapSizeManager, 
            IEntityManager entityManager, 
            int fireCooldown, 
            int laserChargeCooldown, 
            int maxLaserCharges) 
            : base(entityManager, fireCooldown)
        {
            _mapSizeManager = mapSizeManager;
            _laserChargeCooldown = laserChargeCooldown;
            _maxLaserCharges = maxLaserCharges;
            _currentLaserCharges = _maxLaserCharges;
            _currentLaserChargeCooldown = _laserChargeCooldown;
        }

        public override void Fire(Vector2 position, Vector2 direction)
        {
            base.Fire(position, direction);
            
            _currentLaserCharges--;
            var laser = new LaserEntity(_mapSizeManager, position, direction);
            EntityManager.SpawnEntity(laser);
        }

        public override void TickUpdate()
        {
            base.TickUpdate();
            
            if (_currentLaserCharges < _maxLaserCharges)
            {
                if (_currentLaserChargeCooldown > 0)
                {
                    _currentLaserChargeCooldown--;
                }
                else 
                {
                    _currentLaserCharges++;
                    _currentLaserChargeCooldown = _laserChargeCooldown;
                }
            }
        }
    }
}