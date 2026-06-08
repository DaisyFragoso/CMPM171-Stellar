using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridDot : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerEnterHandler, IPointerUpHandler
{
    public static GridDot hoverDot;

    public int row;
    public int col;

    public GameObject linePrefab;

    private GameObject line;
    private RectTransform canvasRect;

    public void Setup(int newRow, int newCol, GameObject newLinePrefab)
    {
        row = newRow;
        col = newCol;
        linePrefab = newLinePrefab;

        // Starting dot is red
        if (row == 3 && col == 3)
        {
            GetComponent<Image>().color = Color.red;
        }
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

            ConstellationLogic.Instance.AddConnection(row, col, hoverDot.row, hoverDot.col);
        }
        else
        {
            SoundFXManager.Instance.PlaySound(ConstellationLogic.Instance.undoSound, transform, 1f);
            Destroy(line);
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