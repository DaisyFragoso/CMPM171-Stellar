using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public PuzzleUIManager puzzleUIManager;
    public GameObject levelUI;
    public GameObject endButton;
    public GameObject endAnimation;
    private RectTransform rectTransform;
    public RectTransform dropZone;
    private static int coinCount = 0;
    public AudioClip coinSound;
    public AudioClip incorrectSound;
    public AudioClip successSound;

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
        if(RectOverlaps(rectTransform, dropZone))
        {
            gameObject.SetActive(false);
            coinCount += 1;
            SoundFXManager.Instance.PlaySound(coinSound, transform, 1f);
            //Debug.Log("Coin collected! Current coin count: " + coinCount);
        }
        else
        {
            SoundFXManager.Instance.PlaySound(incorrectSound, transform, 1f);
        }

        if (coinCount == 3)
        {
            //Debug.Log("You have collected all coins! You can now exit the minigame.");
            levelUI.SetActive(false);
            endAnimation.SetActive(true);
            endButton.SetActive(true);
            SoundFXManager.Instance.PlaySound(successSound, transform, 1f);
        }
        
    }
    private bool RectOverlaps(RectTransform a, RectTransform b)
    {
        Rect aRect = GetWorldRect(a);
        Rect bRect = GetWorldRect(b);
        return aRect.Overlaps(bRect);
    }
    private Rect GetWorldRect(RectTransform rt)
    {
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);
        return new Rect(corners[0].x, corners[0].y,
                        corners[2].x - corners[0].x,
                        corners[2].y - corners[0].y);
    }
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        coinCount = 0;
    }
}
