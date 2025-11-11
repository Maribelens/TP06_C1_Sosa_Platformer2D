using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIChangeScene : MonoBehaviour
{
    [Header("Menu Panel")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button creditsButton;

    [Header("Credits Panel")]
    [SerializeField] private GameObject panelCredits;
    [SerializeField] private Button creditsBackButton;

    [Header("Audio")]
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioSource menuMusicSource;
    public void Awake()
    {
        playButton.onClick.AddListener(OnPlayClicked);
        creditsButton.onClick.AddListener(OnCreditsClicked);
        creditsBackButton.onClick.AddListener(OnCreditsBackClicked);

        menuMusicSource.clip = menuMusic;
        menuMusicSource.Play();
    }

    private void OnCreditsBackClicked()
    {
        panelCredits.SetActive(false);
    }

    private void OnCreditsClicked()
    {
        panelCredits.SetActive(true);
    }

    private void OnPlayClicked()
    {
        SceneManager.LoadScene(1);
    }

    public void OnDestroy()
    {
        playButton.onClick.RemoveAllListeners();
        creditsButton.onClick.RemoveAllListeners();
    }
}
