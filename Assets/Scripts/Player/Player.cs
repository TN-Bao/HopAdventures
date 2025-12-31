using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerInputGame _playerInput;
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private PlayerAnimation _playerAnimation;
    [SerializeField] private PlayerLife _playerLife;
    [SerializeField] private GUIManager _guiManager;
    [SerializeField] private PlayerEvents _playerEvents;
    [SerializeField] private PlayerStateMachine _playerStateMachine;

    [Header("Level Info")]
    [SerializeField] private int _levelIndex;

    private int _totalFruits;
    private int _collectedFruits;

    public PlayerInputGame PlayerInput => _playerInput;
    public PlayerMove PlayerMove => _playerMove;
    public PlayerAnimation PlayerAnimation => _playerAnimation;
    public PlayerLife PlayerLife => _playerLife;
    public GUIManager GuiManager => _guiManager;
    public PlayerEvents PlayerEvents => _playerEvents;
    public int LevelIndex => _levelIndex;
    public int TotalFruits => _totalFruits;
    public int CollectedFruits => _collectedFruits;


    private void Awake()
    {
        _playerStateMachine ??= GetComponent<PlayerStateMachine>();

        if (_playerLife.CurrentLives <= 0) _playerLife.ResetLives();
    }

    private void Start()
    {
        _totalFruits = GameObject.FindGameObjectsWithTag("Collectable").Length;

        _playerEvents?.RaiseFruitChanged(_collectedFruits, _totalFruits);
        _playerEvents?.RaiseLivesChanged(_playerLife.CurrentLives, _playerLife.MaxLives);

        _playerStateMachine.ChangeState(new PlayerNormalState(this));
    }

    private void OnEnable()
    {
        if (_playerInput == null || _playerMove == null || _playerAnimation == null)
        {
            Debug.LogError($"{nameof(Player)} is missing required reference", this);
            return;
        }
        
        _playerInput.OnMoveInput.AddListener(_playerMove.SetMovementInput);
        _playerInput.OnMoveInput.AddListener(_playerAnimation.ChangeAnimationDirection);

        _playerInput.OnJump += _playerMove.Jumping;
        _playerInput.OnPause += PauseGame;
        _playerEvents.OnResume += HandleResume;
    }

    private void OnDisable()
    {
        if (_playerInput == null || _playerMove == null || _playerAnimation == null)
            { return; }

        _playerInput.OnMoveInput.RemoveListener(_playerMove.SetMovementInput);
        _playerInput.OnMoveInput.RemoveListener(_playerAnimation.ChangeAnimationDirection);

        _playerInput.OnJump -= _playerMove.Jumping;
        _playerInput.OnPause -= PauseGame;
        _playerEvents.OnResume -= HandleResume;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            _playerStateMachine.ChangeState(new PlayerDeadState(this));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fire"))
        {
            _playerStateMachine.ChangeState(new PlayerDeadState(this));
        }

        if (collision.gameObject.CompareTag("Collectable"))
        {
            Destroy(collision.gameObject);
            _collectedFruits++;

            _playerEvents?.RaiseFruitChanged(_collectedFruits, _totalFruits);
            _playerEvents?.RaiseCollectedAudio();
        }

        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            _playerStateMachine.ChangeState(new PlayerCompletedState(this));
        }
    }

    private void PauseGame()
    {
        _playerStateMachine.ChangeState(new PlayerPauseState(this));
    }

    private void HandleResume()
    {
        SetInputEnable(true);
        SetControlEnable(true);
        _playerStateMachine.ChangeState(new PlayerNormalState(this));
    }

    internal void SetInputEnable(bool enabled)
        => _playerInput.SetGamePlayInput(enabled);

    internal void SetControlEnable(bool enabled)
    {
        if (enabled) _playerMove.EnableControl();
        else _playerMove.DisableControl();
    }
}
