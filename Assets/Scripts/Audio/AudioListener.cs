using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioListener : MonoBehaviour
{
    [SerializeField] private PlayerEvents _playerEvent;

    [Header("SFX Clips")]
    [SerializeField] private AudioClip _jump;
    [SerializeField] private AudioClip _collect;
    [SerializeField] private AudioClip _damage;
    [SerializeField] private AudioClip _die;
    [SerializeField] private AudioClip _win;


    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneLoaded;
        RebindPlayerEvents();
    }

    private void Win() => AudioManager.Ins.PlaySFX(_win);
    private void Die() => AudioManager.Ins.PlaySFX(_die);
    private void Damage() => AudioManager.Ins.PlaySFX(_damage);
    private void Collect() => AudioManager.Ins.PlaySFX(_collect);
    private void Jump() => AudioManager.Ins.PlaySFX(_jump);

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
        Unsubscribe(_playerEvent);
    }

    private void Subscribe(PlayerEvents events)
    {
        if (events == null) return;
        
        events.OnJumpedAudio += Jump;
        events.OnCollectedAudio += Collect;
        events.OnDamagedAudio += Damage;
        events.OnDiedAudio += Die;
        events.OnWonAudio += Win;
    }

    private void Unsubscribe(PlayerEvents events)
    {
        if (events == null) return;

        events.OnJumpedAudio -= Jump;
        events.OnCollectedAudio -= Collect;
        events.OnDamagedAudio -= Damage;
        events.OnDiedAudio -= Die;
        events.OnWonAudio -= Win;
    }

    private void RebindPlayerEvents()
    {
        Unsubscribe(_playerEvent);

        _playerEvent = Object.FindFirstObjectByType<PlayerEvents>();
        Subscribe(_playerEvent);
    }

    private void SceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        RebindPlayerEvents();
    }
}
