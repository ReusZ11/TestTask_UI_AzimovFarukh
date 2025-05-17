using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonAnimator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Animation Settings")]
    [SerializeField] private float clickScaleAmount = 0.9f;
    [SerializeField] private float clickDuration = 0.1f;

    [SerializeField] private float hoverScaleAmount = 1.05f;
    [SerializeField] private float hoverDuration = 0.2f;

    private Button button;
    private RectTransform rectTransform;
    private Vector3 originalScale;

    private void Awake()
    {
        button = GetComponent<Button>();
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!button.interactable) return;
        rectTransform.DOKill();
        rectTransform.DOScale(originalScale * clickScaleAmount, clickDuration).SetEase(Ease.OutQuad);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!button.interactable) return;

        rectTransform.DOKill();
        if (IsPointerOver(eventData))
        {
            rectTransform.DOScale(originalScale * hoverScaleAmount, clickDuration).SetEase(Ease.OutQuad);
        }
        else
        {
            rectTransform.DOScale(originalScale, clickDuration).SetEase(Ease.OutQuad);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!button.interactable) return;

        rectTransform.DOKill();
        rectTransform.DOScale(originalScale * hoverScaleAmount, hoverDuration).SetEase(Ease.OutQuad);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!button.interactable) return;

        rectTransform.DOKill();
        rectTransform.DOScale(originalScale, hoverDuration).SetEase(Ease.OutQuad);
    }

    private bool IsPointerOver(PointerEventData eventData)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(
            rectTransform,
            eventData.position,
            eventData.pressEventCamera);
    }
}