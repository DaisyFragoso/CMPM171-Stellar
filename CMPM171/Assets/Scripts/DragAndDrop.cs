using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public PuzzleUIManager puzzleUIManager;
    private RectTransform rectTransform;
    private static int coinCount = 0;

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
            coinCount += 1;
            Debug.Log("Coin collected! Current coin count: " + coinCount);
        }

        if (coinCount == 3)
        {
            Debug.Log("You have collected all coins! You can now exit the minigame.");
            puzzleUIManager.DragDropCompletePuzzle();
        }
    }
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
}
