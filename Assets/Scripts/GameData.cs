using UnityEngine;

[CreateAssetMenu(menuName = "Game Data/Game Data")]
public class GameData : ScriptableObject
{
    [Header("Player")]
    public int maxLives = 3;
    [HideInInspector] public int currentLives;


    public void ResetAll()
    {
        currentLives = maxLives;
    }
}
