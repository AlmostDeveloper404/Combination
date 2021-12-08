using UnityEngine;
using UnityEngine.EventSystems;

public class CompletedCombination : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag) Manager.Instance.SetElement(eventData.pointerDrag.GetComponent<Element>());       
    }
}
