using UnityEngine;
public class NPCInteraction : MonoBehaviour
{
    public PuzzleUIManager puzzleManager;
    public int puzzleIndex; // 1, 2, or 3
    private bool hasInteracted = false;

    private bool isPlayerInside = false;

    void Update()
    {
        if (isPlayerInside && !hasInteracted)
        {
            if (puzzleManager.isActive == false)
            {
                puzzleManager.InteractTextToggle();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("PressE worked");
                Interact();
            }
        }

        if (!isPlayerInside && puzzleManager.isActive == true)
        {
            puzzleManager.InteractTextToggle();
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
    }

}