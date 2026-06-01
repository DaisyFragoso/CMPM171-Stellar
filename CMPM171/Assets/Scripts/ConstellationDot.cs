using UnityEngine;
using UnityEngine.EventSystems;

public class ConstellationDot : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerEnterHandler, IPointerUpHandler
{
    static ConstellationDot hoverDot;

    public GameObject linePrefab;
    public int row;
    public int col;

    private GameObject line;
    private RectTransform canvasRect;

    public void Setup(int newRow, int newCol, GameObject newLinePrefab)
    {
        row = newRow;
        col = newCol;
        linePrefab = newLinePrefab;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        canvasRect = canvas.GetComponent<RectTransform>();

        line = Instantiate(linePrefab, canvas.transform);
        line.transform.SetAsLastSibling();

        ConstellationLogic.Instance.createdLines.Add(line);

        UpdateLine(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (line == null) return;

        UpdateLine(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (line == null) return;

        if (hoverDot != null && hoverDot != this)
        {
            UpdateLine(RectTransformUtility.WorldToScreenPoint(null, hoverDot.transform.position));

            bool added = ConstellationLogic.Instance.AddConnection(row, col, hoverDot.row, hoverDot.col);

            if (!added)
            {
                Destroy(line);
                ConstellationLogic.Instance.createdLines.Remove(line);
            }
        }
        else
        {
            Destroy(line);
            ConstellationLogic.Instance.createdLines.Remove(line);
        }

        hoverDot = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
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
