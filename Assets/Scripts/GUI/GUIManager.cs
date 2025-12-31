using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GUIManager : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private PlayerEvents _playerEvents;
    
    [Header("Panels")]
    [SerializeField] private GameObject _completedDialog;
    [SerializeField] private GameObject _pauseDialog;
    [SerializeField] private GameObject _gameoverDialog;
    [SerializeField] private GameObject _settingDialog;

    [Header("HeartUI")]
    [SerializeField] private PlayerLife _playerLife;
    [SerializeField] private Image[] _hearts;
    [SerializeField] private Sprite _fullHeart;
    [SerializeField] private Sprite _emptyHeart;

    [Header("Optional")]
    [SerializeField] private string _homeScene = "HomeScene";
    [SerializeField] private string _levelSelectionScene = "LevelSelection";

    [SerializeField] private Text fruitText;


    private void OnEnable()
    {
        if (_playerEvents == null) return;

        _playerEvents.OnFruitChanged += FruitChanged;
        _playerEvents.OnLivesChanged += LivesChanged;
    }

    private void FruitChanged(int collected, int total)
    {
        UpdateFruitText($"{collected}/{total}");
    }

    private void LivesChanged(int current, int max)
    {
        UpdateHearts(current, max);
    }

    public void UpdateFruitText(string text)
    {
        fruitText.text = text;
    }

    public void UpdateHearts(int currentLives, int maxLives)
    {
        for (int i = 0; i < maxLives; i++)
        {
            if (i < currentLives)
            {
                _hearts[i].sprite = _fullHeart;
            }
            else
            {
                _hearts[i].sprite = _emptyHeart;
            }
        }
    }

    private void OnDisable()
    {
        if (_playerEvents == null) return;

        _playerEvents.OnFruitChanged -= FruitChanged;
        _playerEvents.OnLivesChanged -= LivesChanged;
    }

    public void ShowPausedDialog()
    {
        if (_pauseDialog != null)
        {
            _pauseDialog.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ShowCompeletedDialog()
    {
        if (_completedDialog != null)
        {
            _completedDialog.SetActive(true);
        }
    }

    public void ShowGameoverDialog()
    {
        if (_gameoverDialog != null)
        {
            _gameoverDialog.SetActive(true);
        }
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        _playerLife.ResetLives();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Resume()
    {
        if (_pauseDialog != null)
            _pauseDialog.SetActive(false);

        Time.timeScale = 1f;
        _playerEvents?.RaiseResumeGame();
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        _playerLife.ResetLives();

        int nextIdx = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextIdx);
    }

    public void BackHome()
    {
        if (!string.IsNullOrEmpty(_homeScene))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(_homeScene);
        }
    }

    public void BackToLevelSelection()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(_levelSelectionScene);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
        SceneManager.LoadScene("HomeScene");
#else
        Application.Quit();
#endif  
    }

    public void StartButton()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    public void OptionButton()
    {
        _settingDialog.SetActive(true);
    }

    public void CloseOptionButton()
    {
        _settingDialog.SetActive(false);
    }
}
