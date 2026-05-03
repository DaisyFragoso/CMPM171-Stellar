using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public PuzzleUIManager puzzleManager;
    public bool opensDragDropPuzzle;

    private bool hasInteracted = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasInteracted) return;

        if (!other.CompareTag("Player")) return;

        hasInteracted = true;

        if (opensDragDropPuzzle)
        {
            puzzleManager.DragDropShowPuzzle();
        }
        else
        {
            puzzleManager.ShowPuzzle();
        }
    }
}