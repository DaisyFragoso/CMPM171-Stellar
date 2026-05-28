using UnityEngine;

public class PuzzleUIManager : MonoBehaviour
{
    public GameObject pressEText;          // prompt player to press E
    public GameObject puzzleUI;           // Puzzle 1
    public GameObject puzzleDragDrop;     // Puzzle 2
    public GameObject puzzleConnectingItems; // Puzzle 3
    public GameObject puzzleConstellation; //Puzzle 4
    public GameObject IncorrectScreenConstellation;  //puzzle 4 incorrect screen

    public bool isActive = false;   // UI toggle state

    public GameObject DragDropEndButton;
    public GameObject ConnectingItemsContinueButton;
    public GameObject ConstellationContinueButton;

    public void InteractTextToggle()
    {
        isActive = !isActive;
        Debug.Log("text toggle" + isActive);
        pressEText.SetActive(isActive);
    }

    public void ShowPuzzle1()
    {
        puzzleUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ShowPuzzle2()
    {
        puzzleDragDrop.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ShowPuzzle3()
    {
        puzzleConnectingItems.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ShowPuzzle4()
    {
        puzzleConstellation.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CompletePuzzle1()
    {
        puzzleUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void CompletePuzzle2()
    {
        puzzleDragDrop.SetActive(false);
        Time.timeScale = 1f;
        DragDropEndButton.SetActive(false);
    }

    public void CompletePuzzle3()
    {
        puzzleConnectingItems.SetActive(false);
        Time.timeScale = 1f;
        ConnectingItemsContinueButton.SetActive(false);
    }

    public void CompletePuzzle4()
    {
        puzzleConstellation.SetActive(false);
        Time.timeScale = 1f;
        ConstellationContinueButton.SetActive(false);
        IncorrectScreenConstellation.SetActive(false);
    }
}