using UnityEngine;
using UnityEngine.EventSystems;

public class DragElement : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    Vector2 startPos;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startPos = _rectTransform.anchoredPosition;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.interactable = false;
        _canvasGroup.alpha = .6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / Manager.Instance.CanvasScaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!eventData.used)
        {
            _rectTransform.anchoredPosition = startPos;
        }
        _canvasGroup.interactable = true;
        _canvasGroup.alpha = 1f;
    }


}
