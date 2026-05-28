using UnityEngine;
public class NPCInteraction : MonoBehaviour
{
    public PuzzleUIManager puzzleManager;
    public int puzzleIndex; // 1, 2, or 3
    private bool hasInteracted = false;

    private bool isPlayerInside = false;
    private bool isTextShowing = false;

    void Update()
    {
        if (isPlayerInside && !hasInteracted)
        {
            if (!puzzleManager.isActive && !isTextShowing)
            {
                puzzleManager.InteractTextToggle();
                isTextShowing = true;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("PressE worked");
                puzzleManager.InteractTextToggle();
                isTextShowing = false;
                Interact();
            }
        }

        if (!isPlayerInside && isTextShowing)
        {
            puzzleManager.InteractTextToggle();
            isTextShowing = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            Debug.Log("EEEE");

            // if (Input.GetKeyDown(KeyCode.E))
            // {
            //     Debug.Log("PressE worked");
            //     Interact();
            // }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }

    void Interact()
    {
        Debug.Log("PressE worked");

        hasInteracted = true;

        if (puzzleIndex == 1)
        {
            puzzleManager.ShowPuzzle1();
        }
        else if (puzzleIndex == 2)
        {
            puzzleManager.ShowPuzzle2();
        }
        else if (puzzleIndex == 3)
        {
            puzzleManager.ShowPuzzle3();
        }
        else if (puzzleIndex == 4)
        {
            puzzleManager.ShowPuzzle4();
        }
    }

}