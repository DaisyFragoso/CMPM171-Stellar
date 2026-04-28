using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    private RectTransform rectTransform;

    public void OnBeginDrag(PointerEventData eventData)
    {
        //
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.position.x >= 555.5 && transform.position.y >= -196)
        {
            gameObject.SetActive(false);
        }
    }
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
}
