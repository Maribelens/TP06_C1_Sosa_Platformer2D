using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIChangeScene : MonoBehaviour
{
    [Header("Menu Panel")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;

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
        exitButton.onClick.AddListener(OnExitClicked);

        menuMusicSource.clip = menuMusic;
        menuMusicSource.Play();
    }

    private void OnPlayClicked()
    {
        SceneManager.LoadScene(1);
    }

    private void OnCreditsClicked()
    {
        panelCredits.SetActive(true);
    }

    private void OnExitClicked()
    {
        // Cierra el juego en una build real (Windows, Android, etc.)
        Application.Quit();

        // Si estás en el Editor, detiene el modo Play
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    private void OnCreditsBackClicked()
    {
        panelCredits.SetActive(false);
    }

    public void OnDestroy()
    {
        playButton.onClick.RemoveAllListeners();
        creditsButton.onClick.RemoveAllListeners();
    }
}
