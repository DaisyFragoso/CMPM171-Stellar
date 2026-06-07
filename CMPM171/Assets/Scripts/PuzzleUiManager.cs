using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleUIManager : MonoBehaviour
{
    public GameObject introText;          // intro text for return home mission
    public GameObject pressEText;          // prompt player to press E
    public GameObject alreadyCompletedText; // prompt player that puzzle is already completed
    public GameObject returnHomeText; // prompt player to return home
    public GameObject goHomeText; // prompt player that they can go home
    public GameObject goalUI;           // Return Home Goal UI
    public GameObject puzzleDragDrop;     // Puzzle 2
    public GameObject puzzleConnectingItems; // Puzzle 3
    public GameObject puzzleConstellation; //Puzzle 4
    public GameObject IncorrectScreenConstellation;  //puzzle 4 incorrect screen

    public bool isActive = false;   // UI toggle state

    public GameObject DragDropEndButton;
    public GameObject DragDropEndAnimation;
    public GameObject ConnectingItemsContinueButton;
    public GameObject ConstellationContinueButton;
    public GameObject collectClusterAnim;

    public void IntroTextToggle(bool show)
    {
        introText.SetActive(show);
    }

    public void InteractTextToggle(bool show)
    {
        pressEText.SetActive(show);
    }

    public void PuzzleCompleteTextToggle(bool show)
    {
        alreadyCompletedText.SetActive(show);
    }

    public void ReturnHomeTextToggle(bool show)
    {
        returnHomeText.SetActive(show);
    }
    public void goHomePromptToggle(bool show)
    {
        goHomeText.SetActive(show);
    }

    public void ShowReturnHome()
    {
        isActive = true;
        goalUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ShowPuzzle2()
    {
        isActive = true;
        puzzleDragDrop.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ShowPuzzle3()
    {
        isActive = true;
        puzzleConnectingItems.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ShowPuzzle4()
    {
        isActive = true;
        puzzleConstellation.SetActive(true);
        Time.timeScale = 0f;
    }
    public void CompleteReturnHome()
    {
        Time.timeScale = 1f;
        // SaveManager.SaveGame();
        SceneManager.LoadSceneAsync(2);
    }
    public void HideReturnHome()
    {
        isActive = false;
        goalUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void CompletePuzzle2()
    {
        isActive = false;
        puzzleDragDrop.SetActive(false);
        Time.timeScale = 1f;
        DragDropEndButton.SetActive(false);
        DragDropEndAnimation.SetActive(false);
        Player.playerCoins += 3;

        SaveManager.SaveNPCCompleted(2);
        CheckReturnHomeUnlocked();
        SaveManager.SaveGame();
    }

    public void CompletePuzzle3()
    {
        isActive = false;
        puzzleConnectingItems.SetActive(false);
        Time.timeScale = 1f;
        ConnectingItemsContinueButton.SetActive(false);
        collectClusterAnim.SetActive(false);
        Player.clusterCollected = true;

        SaveManager.SaveNPCCompleted(3);
        CheckReturnHomeUnlocked();
        SaveManager.SaveGame();
    }

    public void CompletePuzzle4()
    {
        isActive = false;
        puzzleConstellation.SetActive(false);
        Time.timeScale = 1f;
        ConstellationContinueButton.SetActive(false);
        IncorrectScreenConstellation.SetActive(false);
        Player.constellationCollected = true;

        SaveManager.SaveNPCCompleted(4);
        CheckReturnHomeUnlocked();
        SaveManager.SaveGame();
    }

    
    public void CheckReturnHomeUnlocked()
    {
        if (SaveManager.IsNPCCompleted(2) &&
            SaveManager.IsNPCCompleted(3) &&
            SaveManager.IsNPCCompleted(4))
        {
            Player.returnHomeCompleted = true;

            Debug.Log("All puzzles completed. Return home is unlocked!");

            SaveManager.SaveGame();
        }
    }
}