using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace View.UI
{
    public class PauseMenuController : MonoBehaviour
    {
        public event Action<bool> OnPauseChanged; // is paused
        
        [SerializeField] private string _mainMenuSceneName;
        [SerializeField] private GameView _gameView;
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;

        public bool IsPaused => gameObject.activeSelf;
        
        private void Start()
        {
            _continueButton.onClick.AddListener(OnContinueButtonClick);
            _restartButton.onClick.AddListener(OnRestartButtonClick);
            _exitButton.onClick.AddListener(OnExitButtonClick);
        }

        public void Show(string title, bool isGameOver)
        {
            _titleText.text = title;
            _continueButton.gameObject.SetActive(!isGameOver);
            _scoreText.gameObject.SetActive(isGameOver);
            gameObject.SetActive(true);

            if (isGameOver)
            {
                _scoreText.text = $"Score: {_gameView.GameModel.ScoreManager.Score}";
            }
            
            OnPauseChanged?.Invoke(true);
        }

        public void Hide()
        {
            OnContinueButtonClick();
        }
        
        private void OnContinueButtonClick()
        {
            gameObject.SetActive(false);
            OnPauseChanged?.Invoke(false);
        }
        
        private void OnRestartButtonClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        private void OnExitButtonClick()
        {
            SceneManager.LoadScene(_mainMenuSceneName);
        }
    }
}