using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverListener : MonoBehaviour
{
    [Header("Listen To")]
    [SerializeField] private PlayerEvents _playerEvents;

    [Header("UI Data")]
    [SerializeField] private GUIManager _guiManager;


    private void OnEnable()
    {
        if (_playerEvents != null)
            _playerEvents.OnGameOver += GameOver;
    }

    private void GameOver()
    {
        if (_guiManager != null)
            _guiManager.ShowGameoverDialog();
    }

    private void OnDisable()
    {
        if (_playerEvents != null)
            _playerEvents.OnGameOver -= GameOver;
    }
}
