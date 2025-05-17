using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;


public class MainMenuScreen : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button characterSelectButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Transform buttonsContainer;

    [Header("Animation Settings")]
    [SerializeField] private float buttonPunchScale = 0.2f;

    private Sequence animationSequence;

    private void OnDestroy()
    {
        if (characterSelectButton) characterSelectButton.onClick.RemoveAllListeners();
        if (quitButton) quitButton.onClick.RemoveAllListeners();
        if (playButton) playButton.onClick.RemoveAllListeners();
        if (settingsButton) settingsButton.onClick.RemoveAllListeners();

        animationSequence?.Kill();
    }

    public void SetupButtons(Action onCharacterSelectClick, Action onQuitClick)
    {
        characterSelectButton.onClick.AddListener(() => onCharacterSelectClick?.Invoke());

        quitButton.onClick.AddListener(() => onQuitClick?.Invoke());

        playButton.onClick.AddListener(() => Debug.Log("затычка"));

        settingsButton.onClick.AddListener(() => Debug.Log("затычка х2"));
    }


    public void AnimateAppear(float duration, float buttonDelay)
    {
        animationSequence?.Kill();
        animationSequence = DOTween.Sequence();

        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0;

        animationSequence.Append(canvasGroup.DOFade(1, duration));

        Button[] buttons = buttonsContainer.GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            Transform buttonTransform = buttons[i].transform;
            buttonTransform.localScale = Vector3.zero;

            animationSequence.Insert(duration + i * buttonDelay,
                buttonTransform.DOScale(1, 0.3f).SetEase(Ease.OutBack));
        }
    }

    public void AnimateQuitButton(Action onComplete)
    {
        if (quitButton)
        {
            Transform buttonTransform = quitButton.transform;
            buttonTransform.DOPunchScale(Vector3.one * buttonPunchScale, 0.2f).OnComplete(() => {
                onComplete?.Invoke();
            });
        }
        else
        {
            onComplete?.Invoke();
        }
    }
}