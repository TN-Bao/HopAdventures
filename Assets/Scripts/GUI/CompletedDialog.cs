using UnityEngine;
using UnityEngine.UI;

public class CompletedDialog : MonoBehaviour
{
    [Header("StarUI")]
    [SerializeField] private Image[] _stars;
    [SerializeField] private Sprite _activeStar;
    [SerializeField] private Sprite _inactiveStar;

    [Range(0f, 1f)]
    [SerializeField] private float _fruitWeight = 0.6f;

    [Range(0f, 1f)]
    [SerializeField] private float _threeStar = 0.8f;

    [Range(0f, 1f)]
    [SerializeField] private float _twoStar = 0.5f;


    public int SetStarResult(int collectedFruits, int totalFruits, int remainingLives, int maxLives)
    {
        int starCount = CalculateStarCount(collectedFruits, totalFruits, remainingLives, maxLives);
        UpdateStarUI(starCount);

        return starCount;
    }


    public int CalculateStarCount(int collectedFruits, int totalFruits,
        int remainingLives, int maxLives)
    {
        remainingLives = Mathf.Clamp(remainingLives, 0, maxLives);
        
        float fruitRatio = (float)collectedFruits / totalFruits;
        float lifeRatio = (float)remainingLives / maxLives;

        float score = fruitRatio * _fruitWeight + lifeRatio * (1 - _fruitWeight);

        if (score >= _threeStar) return 3;
        else if (score >= _twoStar) return 2;

        return 1;
    }

    private void UpdateStarUI(int starCount)
    {
        if (_stars == null || _stars.Length == 0) return;

        for (int i = 0; i < _stars.Length; i++)
        {            
            Debug.Log("BAODEG: Update Star UI");

            _stars[i].sprite = (i < starCount) ? _activeStar : _inactiveStar;
        }
    }
}
