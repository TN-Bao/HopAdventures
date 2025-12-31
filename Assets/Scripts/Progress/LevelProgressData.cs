using UnityEngine;

[CreateAssetMenu(menuName = "Game Data/Level Progress")]
public class LevelProgressData : ScriptableObject
{
    [Header("Config")]
    [SerializeField] public int _levelCount = 6;
    [SerializeField] private int[] _bestStars;
    [SerializeField] private bool[] _unlocked;

    public bool[] Unlocked => _unlocked;
    public int[] BestStars => _bestStars;

    public void Init()
    {        
        if (_bestStars == null || _bestStars.Length != _levelCount)
            _bestStars = new int[_levelCount];

        if (_unlocked == null || _unlocked.Length != _levelCount)
        {
            _unlocked = new bool[_levelCount];
        }

        if (_unlocked.Length > 0)
            _unlocked[0] = true;
    }

    public void SetResult(int levelIdx, int stars)
    {
        if (levelIdx < 0 || levelIdx >= _levelCount) return;

        if (stars > _bestStars[levelIdx])
            _bestStars[levelIdx] = stars;

        if (stars > 0 && levelIdx + 1 < _levelCount)
            _unlocked[levelIdx + 1] = true;
    }
}
