using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] private MainMenuScreen mainMenuScreen;
    [SerializeField] private CharacterSelectScreen characterSelectScreen;

    [Header("Animation Settings")]
    [SerializeField] private float screenFadeInDuration = 0.5f;
    [SerializeField] private float buttonFadeInDelay = 0.1f;

    private void Awake()
    {
        if (!DOTween.instance)
            DOTween.Init();
    }

    private void Start()
    {
        mainMenuScreen.SetupButtons(OpenCharacterSelectScreen, QuitGame);
        characterSelectScreen.SetupBackButton(OpenMainMenuScreen);

        OpenMainMenuScreen();
    }

    private void OnDestroy()
    {
        DOTween.KillAll();
    }

    public void OpenMainMenuScreen()
    {
        characterSelectScreen.gameObject.SetActive(false);
        mainMenuScreen.gameObject.SetActive(true);
        mainMenuScreen.AnimateAppear(screenFadeInDuration, buttonFadeInDelay);
    }

    public void OpenCharacterSelectScreen()
    {
        mainMenuScreen.gameObject.SetActive(false);
        characterSelectScreen.gameObject.SetActive(true);
        characterSelectScreen.AnimateAppear(screenFadeInDuration, buttonFadeInDelay);
    }

    public void QuitGame()
    {
        mainMenuScreen.AnimateQuitButton(() => {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });
    }
}