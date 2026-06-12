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
    private Canvas canvas;
    private Camera canvasCamera;

    public void Setup(int newRow, int newCol, GameObject newLinePrefab)
    {
        row = newRow;
        col = newCol;
        linePrefab = newLinePrefab;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        canvas = GetComponentInParent<Canvas>();
        canvasRect = canvas.GetComponent<RectTransform>();
        canvasCamera = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;

        line = Instantiate(linePrefab, canvas.transform);
        line.transform.SetAsLastSibling();

        ConstellationLogic.Instance.createdLines.Add(line);

        UpdateLine(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (line == null) return;
        Debug.Log($"mouse screen:{eventData.position} | dot screen:{RectTransformUtility.WorldToScreenPoint(canvasCamera, transform.position)} | dot world:{transform.position}");
        UpdateLine(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (line == null) return;

        if (hoverDot != null && hoverDot != this)
        {
            UpdateLine(RectTransformUtility.WorldToScreenPoint(canvasCamera, hoverDot.transform.position));

            bool added = ConstellationLogic.Instance.AddConnection(row, col, hoverDot.row, hoverDot.col);

            if (!added)
            {
                Destroy(line);
                ConstellationLogic.Instance.createdLines.Remove(line);
            }
        }
        else
        {
            SoundFXManager.Instance.PlaySound(ConstellationLogic.Instance.undoSound, transform, 1f);
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
            RectTransformUtility.WorldToScreenPoint(canvasCamera, transform.position),
            canvasCamera,
            out startPos
        );

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPosition,
            canvasCamera,
            out endPos
        );

        // Correct for canvas scaler offset
        float scaleFactor = canvas.scaleFactor;
        Vector2 correction = new Vector2(
            canvasRect.rect.width * 0.5f - (Screen.width * 0.5f / scaleFactor),
            canvasRect.rect.height * 0.5f - (Screen.height * 0.5f / scaleFactor)
        );
        endPos -= correction;

        Vector2 direction = endPos - startPos;

        lineRect.anchoredPosition = startPos;
        lineRect.sizeDelta = new Vector2(direction.magnitude, 8f);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        lineRect.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
