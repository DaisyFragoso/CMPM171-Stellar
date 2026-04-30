using UnityEngine;

public class PuzzleUIManager : MonoBehaviour
{
    public GameObject puzzleUI;

    public void ShowPuzzle()
    {
        puzzleUI.SetActive(true);
        Time.timeScale = 0f; // pauses game 
    }

    public void CompletePuzzle()
    {
        Debug.Log("Puzzle Complete!");
        puzzleUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
