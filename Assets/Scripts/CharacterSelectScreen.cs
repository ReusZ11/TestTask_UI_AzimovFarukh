using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;


public class CharacterSelectScreen : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private CharactersManager charactersManager;
    [SerializeField] private Image selectedCharacterDisplay;
    [SerializeField] private Button backButton;
    [SerializeField] private Button selectButton;

    [Header("Animation Settings")]
    [SerializeField] private float switchDuration = 0.3f;

    private Sequence animationSequence;


    private void OnDestroy()
    {
        backButton.onClick.RemoveAllListeners();
        selectButton.onClick.RemoveAllListeners();
        animationSequence?.Kill();
    }


    public void SetupBackButton(Action onBackClick)
    {
        backButton.onClick.AddListener(() => { 
            onBackClick?.Invoke();
            Debug.Log("baaack");
        });

        selectButton.onClick.AddListener(() => {
            Character selectedCharacter = charactersManager.GetSelectedCharacter();
        });
    }

    public void AnimateAppear(float duration, float elementDelay)
    {
        animationSequence?.Kill();
        animationSequence = DOTween.Sequence();

        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        selectedCharacterDisplay.transform.localScale = Vector3.zero;

        animationSequence.Append(canvasGroup.DOFade(1, duration));

        backButton.transform.localScale = Vector3.zero;
        animationSequence.Insert(duration, backButton.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack));
         
        selectButton.transform.localScale = Vector3.zero;
        animationSequence.Insert(duration + elementDelay, selectButton.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack));
        
        animationSequence.Insert(duration + elementDelay * 2,
            selectedCharacterDisplay.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack));

    }
}