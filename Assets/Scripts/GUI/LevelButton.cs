using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [Header("Config")]
    public int _levelIndex;
    public string _sceneName;

    [Header("References")]
    [SerializeField] private LevelProgressData _levelProgress;
    [SerializeField] private Button _button;
    [SerializeField] private Sprite _lockIcon;
    [SerializeField] private Sprite _activeStar;
    [SerializeField] private Sprite _inactiveStar;
    [SerializeField] private Image[] _stars;


    private void Start()
    {
        bool unlocked = _levelProgress.Unlocked[_levelIndex];

        _button.interactable = unlocked;

        if (!unlocked)
        {
            for (int i = 0; i < _stars.Length; i++)
            {
                _stars[i].sprite = _lockIcon;
            }

            return;
        }

        int starCount = _levelProgress.BestStars[_levelIndex];
        for (int i = 0; i < _stars.Length; i++)
        {
            _stars[i].sprite = (i < starCount) ? _activeStar : _inactiveStar;
        }

        _button.onClick.AddListener(OnClickLevel);
    }

    private void OnClickLevel()
    {
        if (!_levelProgress.Unlocked[_levelIndex]) return;

        SceneManager.LoadScene(_sceneName);
    }
}
