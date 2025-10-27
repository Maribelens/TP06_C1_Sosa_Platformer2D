using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    [Header("Menu Panel")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button creditsButton;

    [Header("Credits Panel")]
    [SerializeField] private GameObject panelCredits;
    [SerializeField] private Button creditsBackButton;

    [Header("Audio")]
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioSource musicSource;
    public void Awake()
    {
        playButton.onClick.AddListener(OnPlayClicked);
        creditsButton.onClick.AddListener(OnCreditsClicked);
        creditsBackButton.onClick.AddListener(OnCreditsBackClicked);

        musicSource.clip = menuMusic;
        musicSource.Play();
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
        SceneManager.LoadScene("Gameplay");
    }

    public void OnDestroy()
    {
        playButton.onClick.RemoveAllListeners();
        creditsButton.onClick.RemoveAllListeners();
    }
}
