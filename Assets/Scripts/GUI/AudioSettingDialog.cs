using UnityEngine;
using UnityEngine.UI;

public class AudioSettingDialog : MonoBehaviour
{
    [Header("UI Slider (0..1)")]
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    private const string MusicKey = "music_volume";
    private const string SfxKey = "sfx_volume";


    private void OnEnable()
    {
        float music = PlayerPrefs.GetFloat(MusicKey, 1f);
        float sfx = PlayerPrefs.GetFloat(SfxKey, 1f);

        _musicSlider.SetValueWithoutNotify(music);
        _sfxSlider.SetValueWithoutNotify(sfx);

        AudioManager.Ins.SetMusicVolume(music);
        AudioManager.Ins.SetSFXVolume(sfx);

        _musicSlider.onValueChanged.AddListener(OnMusicChanged);
        _sfxSlider.onValueChanged.AddListener(OnSfxChanged);
    }

    private void OnSfxChanged(float value)
    {
        AudioManager.Ins.SetSFXVolume(value);
        PlayerPrefs.SetFloat(SfxKey, value);
        PlayerPrefs.Save();
    }

    private void OnMusicChanged(float value)
    {
        AudioManager.Ins.SetMusicVolume(value);
        PlayerPrefs.SetFloat(MusicKey, value);
        PlayerPrefs.Save();
    }

    private void OnDisable()
    {
        _musicSlider.onValueChanged.RemoveListener(OnMusicChanged);
        _sfxSlider.onValueChanged.RemoveListener(OnSfxChanged);
    }
}
