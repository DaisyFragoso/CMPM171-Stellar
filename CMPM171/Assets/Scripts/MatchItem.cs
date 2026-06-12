// 
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MatchItem : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
{
    static MatchItem hoverItem;

    public GameObject linePrefab;
    public string itemName;

    private GameObject line;
    private RectTransform canvasRect;
    private Canvas canvas;
    private Camera canvasCamera;
    private bool isDragging = false;
    private bool matched = false;

    public AudioClip lineDrawSound;
    public AudioClip incorrectSound;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (matched) return;

        canvas = GetComponentInParent<Canvas>();
        canvasRect = canvas.GetComponent<RectTransform>();
        canvasCamera = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;

        line = Instantiate(linePrefab, canvas.transform);
        line.transform.SetAsLastSibling();

        MatchLogic.Instance.createdLines.Add(line);
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
        if (matched || line == null) return;

        if (hoverItem != null && hoverItem != this && itemName == hoverItem.itemName && !matched && !hoverItem.matched)
        {
            UpdateLine(RectTransformUtility.WorldToScreenPoint(canvasCamera, hoverItem.transform.position));
            MatchLogic.AddPoint();
            hoverItem.matched = true;
            SoundFXManager.Instance.PlaySound(lineDrawSound, transform, 1f);
        }
        else
        {
            SoundFXManager.Instance.PlaySound(incorrectSound, line.transform, 1f);
            Destroy(line);
            MatchLogic.Instance.createdLines.Remove(line);
        }

        hoverItem = null;
        line = null;
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