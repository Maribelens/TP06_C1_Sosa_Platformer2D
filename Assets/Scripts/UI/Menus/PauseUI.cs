using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private CanvasGroup panelMainPause;
    [SerializeField] private CanvasGroup panelSettings;

    [Header("Buttons")]
    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnOptions;
    [SerializeField] private Button btnExit;
    [SerializeField] private Button btnSettingsBack;

    private bool isPause;

    private void Awake()
    {
        AddButtonsListeners();
        SetStateCanvasGroup(panelMainPause, false);
        SetStateCanvasGroup(panelSettings, false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    private void AddButtonsListeners()
    {
        btnPlay.onClick.AddListener(OnPlayClicked);
        btnOptions.onClick.AddListener(OnOptionsClicked);
        btnExit.onClick.AddListener(OnExitClicked);
        btnSettingsBack.onClick.AddListener(OnSettingsBackClicked);
    }

    private void TogglePause()
    {
        isPause = !isPause;
        SetStateCanvasGroup(panelMainPause, isPause);
        //panelMainPause.SetActive(isPause);
        if (isPause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void OnPlayClicked()
    {
        TogglePause();
    }

    private void OnOptionsClicked()
    {
        SetStateCanvasGroup(panelSettings, true);
        //panelSettings.SetActive(true);
    }
    private void OnExitClicked()
    {
        ExitGame();
    }

    private void ExitGame()
    {
        //#if UNITY_EDITOR
        //        UnityEditor.EditorApplication.isPlaying = false;
        //#endif
        SceneManager.LoadScene(0);
    }

    private void OnSettingsBackClicked()
    {
        SetStateCanvasGroup(panelSettings, false);
        //panelSettings.SetActive(false);
    }

    private void SetStateCanvasGroup(CanvasGroup canvasGroup, bool state)
    {
        canvasGroup.alpha = state ? 1 : 0;
        canvasGroup.interactable = state;
        canvasGroup.blocksRaycasts = state;
    }

    private void OnDestroy()
    {
        RemoveButtonsListeners();    
    }

    private void RemoveButtonsListeners()
    {
        btnPlay.onClick.RemoveAllListeners();
        btnOptions.onClick.RemoveAllListeners();
        btnExit.onClick.RemoveAllListeners();
        btnSettingsBack.onClick.RemoveAllListeners();
    }
}
