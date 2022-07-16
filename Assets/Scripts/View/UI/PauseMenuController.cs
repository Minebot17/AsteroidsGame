using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace View.UI
{
    public class PauseMenuController : MonoBehaviour
    {
        [SerializeField] private string _mainMenuSceneName;
        [SerializeField] private GameView _gameView;
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;
        
        private void Start()
        {
            _continueButton.onClick.AddListener(OnContinueButtonClick);
            _restartButton.onClick.AddListener(OnRestartButtonClick);
            _exitButton.onClick.AddListener(OnExitButtonClick);
        }

        public void Show(string title, bool isGameOver)
        {
            _gameView.SetPause(true);
            _titleText.text = title;
            _continueButton.gameObject.SetActive(!isGameOver);
            gameObject.SetActive(true);
        }
        
        private void OnContinueButtonClick()
        {
            _gameView.SetPause(false);
            gameObject.SetActive(false);
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