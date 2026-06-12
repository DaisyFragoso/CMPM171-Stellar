using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridDot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
{
    public static GridDot hoverDot;

    public int row;
    public int col;

    public GameObject linePrefab;

    private GameObject line;
    private RectTransform canvasRect;
    private Canvas canvas;
    private Camera canvasCamera;
    private bool isDragging = false;

    public void Setup(int newRow, int newCol, GameObject newLinePrefab)
    {
        row = newRow;
        col = newCol;
        linePrefab = newLinePrefab;

        if (row == 3 && col == 3)
            GetComponent<Image>().color = Color.red;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        canvas = GetComponentInParent<Canvas>();
        canvasRect = canvas.GetComponent<RectTransform>();
        canvasCamera = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;

        line = Instantiate(linePrefab, canvas.transform);
        line.transform.SetAsLastSibling();

        ConstellationLogic.Instance.createdLines.Add(line);
        isDragging = true;
        UpdateLine(Input.mousePosition);
    }

    void Update()
    {
        if (isDragging && line != null)
            UpdateLine(Input.mousePosition);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        if (line == null) return;

        if (hoverDot != null && hoverDot != this)
        {
            UpdateLine(RectTransformUtility.WorldToScreenPoint(canvasCamera, hoverDot.transform.position));
            ConstellationLogic.Instance.AddConnection(row, col, hoverDot.row, hoverDot.col);
        }
        else
        {
            SoundFXManager.Instance.PlaySound(ConstellationLogic.Instance.undoSound, transform, 1f);
            Destroy(line);
            ConstellationLogic.Instance.createdLines.Remove(line);
        }

        hoverDot = null;
        line = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverDot = this;
    }

    public void UpdateLine(Vector3 screenPosition)
    {
        if (line == null) return;

        RectTransform lineRect = line.GetComponent<RectTransform>();
        Vector3 startWorld = transform.position;

        Vector3 screenPos3D = new Vector3(screenPosition.x, screenPosition.y,
            canvasCamera.WorldToScreenPoint(startWorld).z);
        Vector3 endWorld = canvasCamera.ScreenToWorldPoint(screenPos3D);

        Vector3 direction = endWorld - startWorld;
        float distance = direction.magnitude;

        lineRect.position = startWorld;
        lineRect.sizeDelta = new Vector2(distance / canvas.transform.lossyScale.x, 8f);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        lineRect.rotation = Quaternion.Euler(0, 0, angle);
    }
}