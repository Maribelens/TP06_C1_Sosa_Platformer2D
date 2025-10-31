using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    [Header("Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider uiSlider;

    //[SerializeField] private GameManager gameManager;

    private void Awake()
    {
        //gameManager = GameObject.Find("MusicGameplay").GetComponent<AudioSource>();

        masterSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        uiSlider.onValueChanged.AddListener(OnUIVolumeChanged);
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
    }

    private void OnMasterVolumeChanged(float volume)
    {
        volume = masterSlider.value;
        audioMixer.SetFloat("masterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("masterVolume", volume);
    }

    private void OnMusicVolumeChanged(float volume)
    {
        volume = musicSlider.value;
        audioMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    private void OnSFXVolumeChanged(float volume)
    {
        volume = sfxSlider.value;
        audioMixer.SetFloat("sfxVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    private void OnUIVolumeChanged(float volume)
    {
        volume = uiSlider.value;
        audioMixer.SetFloat("uiVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("uiVolume", volume);
    }

    private void LoadVolume()
    {
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        masterSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }

    private void OnDestroy()
    {
        masterSlider.onValueChanged.RemoveAllListeners();
        musicSlider.onValueChanged.RemoveAllListeners();
        sfxSlider.onValueChanged.RemoveAllListeners();
    }
}
