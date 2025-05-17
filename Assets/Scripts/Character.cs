using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using System;
using TMPro;

/// <summary>
/// скрипт префаба для отображения персов
/// </summary>
public class Character : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI")]
    [SerializeField] private Image characterIcon;
    [SerializeField] private Image selectionFrame;
    [SerializeField] private Slider levelBar;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI nameText;

    [Header("Animation Settings")]
    [SerializeField] private float selectedScale = 1.1f;
    [SerializeField] private float hoverScale = 1.05f;
    [SerializeField] private float animationDuration = 0.2f;

    private CharacterData characterData;
    private bool isSelected = false;
    private RectTransform rectTransform;

    public CharacterData Data => characterData;
    public bool IsSelected => isSelected;

    public event Action<Character> OnCharacterSelected;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        rectTransform.localScale = Vector3.one;
    }

    private void Start()
    {
        rectTransform.localScale = Vector3.one;
    }

    public void Initialize(CharacterData data)
    {
        characterData = data;
        characterIcon.sprite = data.CharacterIcon;
        nameText.text = data.CharacterName;
        levelText.text = $"Lvl {data.Level}";
       /*levelBar.value = data.Level / 100f; */
        selectionFrame.gameObject.SetActive(false);
        rectTransform.localScale = Vector3.one;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        rectTransform.DOPunchScale(Vector3.one * 0.2f, 0.2f, 5, 0.5f);

        OnCharacterSelected?.Invoke(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isSelected)
        {
            rectTransform.DOScale(hoverScale, animationDuration).SetEase(Ease.OutQuad);
        }
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isSelected)
        {
            rectTransform.DOScale(1f, animationDuration).SetEase(Ease.OutQuad);
        }
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;

        if (selected)
        {
            rectTransform.DOScale(selectedScale, animationDuration).SetEase(Ease.OutBack);
            selectionFrame.gameObject.SetActive(true);
        }
        else
        {
            rectTransform.DOScale(1f, animationDuration).SetEase(Ease.OutQuad);
            selectionFrame.gameObject.SetActive(false);
        }
    }

    public void AnimateExperienceUpdate(float newExperience)
    {
        levelBar.DOValue(newExperience, 2f).SetEase(Ease.OutCubic);
    }



}