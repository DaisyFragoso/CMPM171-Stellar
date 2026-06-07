using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject continueButton;

    void Start()
    {
        // Only show Continue if a save exists
        if (continueButton != null)
        {
            continueButton.SetActive(SaveManager.HasSave());
        }
    }

    public void NewGame()
    {
        // Clears old save
        SaveManager.DeleteSave();

        // Reset player progress
        Player.playerCoins = 0;
        Player.clusterCollected = false;
        Player.constellationCollected = false;
        Player.returnHomeCompleted = false;

        // Load your game scene
        SceneManager.LoadSceneAsync(1);
    }

    public void ContinueGame()
    {
        // If there is no save, start a new game
        if (!SaveManager.HasSave())
        {
            Debug.Log("No save found. Starting new game.");
            NewGame();
            return;
        }

        SaveManager.LoadGame();

        int savedScene = SaveManager.GetSavedScene();

        // Safety check:
        // 0 = Main Menu
        // 1 = GameScene
        // 2 = EndScene
        //
        // If it saved Main Menu or EndScene by accident, load GameScene instead.
        if (savedScene != 1)
        {
            Debug.Log("Saved scene was not gameplay scene. Loading GameScene instead.");
            savedScene = 1;
        }

        SceneManager.LoadSceneAsync(savedScene);
    }
}