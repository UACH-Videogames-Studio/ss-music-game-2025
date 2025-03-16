using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.EventSystems;
public class CustomButtonColor : CustomBottonBase
{
    private TMP_Text buttonText;
    private Color originalColor;
    [SerializeField] private Color toColor = Color.black;
    [SerializeField] private float duration;
    [SerializeField] private float fade;

    private void Awake()
    {
        buttonText = GetComponentInChildren<TMP_Text>();
        originalColor = buttonText.color;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        buttonText.DOColor(toColor, duration).SetEase(Ease.InOutSine);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        buttonText.DOColor(originalColor, duration).SetEase(Ease.InOutSine);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        buttonText.DOFade(fade, duration).SetEase(Ease.InOutSine);
    }
}
