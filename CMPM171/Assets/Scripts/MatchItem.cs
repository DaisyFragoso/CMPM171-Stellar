// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.EventSystems;

// public class MatchItem : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerEnterHandler, IPointerUpHandler
// {
//     static MatchItem hoverItem;
//     public GameObject linePrefab;
//     public string itemName;

//     private GameObject line;

//     public void OnPointerDown(PointerEventData eventData)
//     {
//         line = Instantiate(linePrefab, transform.position, Quaternion.identity, transform.parent.parent);
//         UpdateLine(eventData.position);
//     }

//     public void OnDrag(PointerEventData eventData)
//     {
//         UpdateLine(eventData.position);
//     }

//     // Update is called once per frame
//     public void OnPointerUp(PointerEventData eventData)
//     {
//         if (!this.Equals(hoverItem) && itemName.Equals(hoverItem.itemName))
//         {
//             UpdateLine(hoverItem.transform.position);
//             Destroy(hoverItem);
//             Destroy(this);
//             MatchLogic.AddPoint();
//         }
//         else {
//             Destroy(line);
//         }
//     }

//     public void OnPointerEnter(PointerEventData eventData)
//     {
//         hoverItem = this;
//     }

//     public void UpdateLine(Vector3 position)
//     {
//         Vector3 direction = position - transform.position;
//         line.transform.right = direction;

//         line.transform.localScale = new Vector3(direction.magnitude, 1, 1);
//     }
// }

using UnityEngine;
using UnityEngine.EventSystems;

public class MatchItem : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerEnterHandler, IPointerUpHandler
{
    static MatchItem hoverItem;

    public GameObject linePrefab;
    public string itemName;

    private GameObject line;
    private RectTransform canvasRect;

    private bool matched = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (matched) return;

        Canvas canvas = GetComponentInParent<Canvas>();
        canvasRect = canvas.GetComponent<RectTransform>();

        line = Instantiate(linePrefab, canvas.transform);
        line.transform.SetAsLastSibling();

        UpdateLine(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // if (matched || line == null) return;
        if (matched) return;

        UpdateLine(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (matched || line == null) return;

        if (hoverItem != null && hoverItem != this && itemName == hoverItem.itemName && !matched && !hoverItem.matched)
        {
            UpdateLine(RectTransformUtility.WorldToScreenPoint(null, hoverItem.transform.position));

            // Destroy(hoverItem.gameObject);
            // Destroy(gameObject);
            MatchLogic.AddPoint();
            hoverItem.matched = true;
            
        }
        else
        {
            Destroy(line);
        }

        hoverItem = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (matched) return;

        hoverItem = this;
    }

    public void UpdateLine(Vector3 screenPosition)
    {
        if (line == null) return;

        RectTransform lineRect = line.GetComponent<RectTransform>();

        Vector3 start = transform.position;
        Vector3 end = screenPosition;

        // Force visual direction left → right
        if (start.x > end.x)
        {
            Vector3 temp = start;
            start = end;
            end = temp;
        }

        Vector3 direction = end - start;

        lineRect.position = start;
        lineRect.sizeDelta = new Vector2(direction.magnitude, 8f);
        lineRect.right = direction;
    }
}
