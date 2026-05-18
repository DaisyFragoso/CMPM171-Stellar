using UnityEngine;
using UnityEngine.EventSystems;

public class ConnectDotItem : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerEnterHandler, IPointerUpHandler
{
    static ConnectDotItem hoverDot;

    public GameObject linePrefab;
    public int dotNumber;

    private GameObject line;
    private RectTransform canvasRect;

    private bool connected = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (connected) return;

        Canvas canvas = GetComponentInParent<Canvas>();
        canvasRect = canvas.GetComponent<RectTransform>();

        line = Instantiate(linePrefab, canvas.transform);
        line.transform.SetAsLastSibling();

        ConnectDotLogic.Instance.createdLines.Add(line);

        UpdateLine(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (connected || line == null) return;

        UpdateLine(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (connected || line == null) return;

        if (hoverDot != null &&
            hoverDot != this &&
            ConnectDotLogic.Instance.CanConnect(dotNumber, hoverDot.dotNumber))
        {
            UpdateLine(RectTransformUtility.WorldToScreenPoint(null, hoverDot.transform.position));

            connected = true;
            ConnectDotLogic.Instance.AddConnection();
        }
        else
        {
            Destroy(line);
        }

        hoverDot = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (connected) return;

        hoverDot = this;
    }

    public void UpdateLine(Vector3 screenPosition)
    {
        if (line == null) return;

        RectTransform lineRect = line.GetComponent<RectTransform>();

        Vector2 startPos;
        Vector2 endPos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            RectTransformUtility.WorldToScreenPoint(null, transform.position),
            null,
            out startPos
        );

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPosition,
            null,
            out endPos
        );

        Vector2 direction = endPos - startPos;

        lineRect.anchoredPosition = startPos;
        lineRect.sizeDelta = new Vector2(direction.magnitude, 8f);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        lineRect.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
