using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private LevelProgressData _levelProgress;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _levelProgress.Init();
    }
}
