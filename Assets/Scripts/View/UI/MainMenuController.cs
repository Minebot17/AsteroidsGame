using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace View.UI
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private string _gameSceneName;
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _exitButton;

        private void Start()
        {
            _newGameButton.onClick.AddListener(OnNewGameClick);
            _exitButton.onClick.AddListener(OnExitClick);
        }
        
        private void OnNewGameClick()
        {
            SceneManager.LoadScene(_gameSceneName);
        }
        
        private void OnExitClick()
        {
            Application.Quit();
        }
    }
}