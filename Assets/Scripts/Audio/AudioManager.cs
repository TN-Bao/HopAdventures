using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Ins {  get; private set; }
    
    [Header("Mixer")]
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private string _musicVolume = "MusicVol";
    [SerializeField] private string _sfxVolume = "SFXVol";

    [Header("Sources")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioClip _musicClip;
    [SerializeField] private bool _playBgmOnStart = true;


    private void Awake()
    {
        if (Ins != null)
        {
            Destroy(gameObject);
            return;
        }

        Ins = this;
        DontDestroyOnLoad(gameObject);

        if (_playBgmOnStart && _musicSource != null)
            PlayMusic(_musicClip, _playBgmOnStart);
    }

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (clip == null) return;

        if (_musicSource.clip == clip && _musicSource.isPlaying) return;

        _musicSource.clip = clip;
        _musicSource.loop = loop;
        _musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;

        _sfxSource.PlayOneShot(clip);
    }

    public void SetMusicVolume(float value)
    {
        _audioMixer.SetFloat(_musicVolume, ToDb(value));
    }

    public void SetSFXVolume(float value)
    {
        _audioMixer.SetFloat(_sfxVolume, ToDb(value));
    }

    private float ToDb(float value)
    {
        if (value <= 0f) return -80f;

        value = Mathf.Clamp(value, 0.0001f, 1f);
        return Mathf.Log10(value) * 20f;
    }
}
