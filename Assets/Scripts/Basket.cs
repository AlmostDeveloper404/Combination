using UnityEngine;
using UnityEngine.EventSystems;

public class Basket : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag)
        {
            Manager.Instance.DeleteElement(eventData.pointerDrag.GetComponent<Element>());
            Manager.Instance.AddNewRandomElement();
        }
        
    }

    
}
