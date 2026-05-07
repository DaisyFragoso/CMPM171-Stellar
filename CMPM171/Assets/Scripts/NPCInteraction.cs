using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public PuzzleUIManager puzzleManager;

    public int puzzleIndex; // 1, 2, or 3

    private bool hasInteracted = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasInteracted) return;
        if (!other.CompareTag("Player")) return;

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
