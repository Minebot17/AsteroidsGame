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
        private GameObject _bigAsteroidPrefab;
        
        private IGameModel _gameModel;

        private void Start()
        {
            _gameModel = new GameModel.GameModel(ConstructEntitySpawner());
        }

        private void FixedUpdate()
        {
            _gameModel.FixedUpdate();
        }

        private EntitySpawner ConstructEntitySpawner()
        {
            EntitySpawner entitySpawner = new EntitySpawner();
            
            entitySpawner.RegisterEntityPrefab(typeof(BigAsteroidLogic), _bigAsteroidPrefab);

            return entitySpawner;
        }
    }
}