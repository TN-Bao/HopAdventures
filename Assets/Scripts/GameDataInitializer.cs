using UnityEngine;

public class GameDataInitializer : MonoBehaviour
{
    [SerializeField] private GameData _gameData;

    private static bool _initialized = false;

    private void Awake()
    {
        if (_initialized) return;

        _gameData.ResetAll();
        _initialized = true;
    }
}
