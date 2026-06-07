using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveManager
{
    public static void SaveGame()
    {
        PlayerPrefs.SetInt("HasSave", 1);

        // PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetInt("SavedScene", 1);

        PlayerPrefs.SetInt("Coins", Player.playerCoins);
        PlayerPrefs.SetInt("ClusterCollected", Player.clusterCollected ? 1 : 0);
        PlayerPrefs.SetInt("ConstellationCollected", Player.constellationCollected ? 1 : 0);
        PlayerPrefs.SetInt("ReturnHomeCompleted", Player.returnHomeCompleted ? 1 : 0);

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Vector3 pos = player.transform.position;

            PlayerPrefs.SetFloat("PlayerX", pos.x);
            PlayerPrefs.SetFloat("PlayerY", pos.y);
            PlayerPrefs.SetFloat("PlayerZ", pos.z);
        }

        PlayerPrefs.Save();

        Debug.Log("Game saved!");
    }

    public static void LoadGame()
    {
        Player.playerCoins = PlayerPrefs.GetInt("Coins", 0);
        Player.clusterCollected = PlayerPrefs.GetInt("ClusterCollected", 0) == 1;
        Player.constellationCollected = PlayerPrefs.GetInt("ConstellationCollected", 0) == 1;
        Player.returnHomeCompleted = PlayerPrefs.GetInt("ReturnHomeCompleted", 0) == 1;

        Debug.Log("Game loaded!");
    }

    public static void LoadPlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && HasSave())
        {
            float x = PlayerPrefs.GetFloat("PlayerX", player.transform.position.x);
            float y = PlayerPrefs.GetFloat("PlayerY", player.transform.position.y);
            float z = PlayerPrefs.GetFloat("PlayerZ", player.transform.position.z);

            player.transform.position = new Vector3(x, y, z);
        }
    }

    public static bool HasSave()
    {
        return PlayerPrefs.GetInt("HasSave", 0) == 1;
    }

    public static int GetSavedScene()
    {
        return PlayerPrefs.GetInt("SavedScene", 1);
    }

    public static void DeleteSave()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    public static void SaveNPCCompleted(int puzzleIndex)
    {
        PlayerPrefs.SetInt("NPCCompleted_" + puzzleIndex, 1);
        PlayerPrefs.Save();
    }

    public static bool IsNPCCompleted(int puzzleIndex)
    {
        return PlayerPrefs.GetInt("NPCCompleted_" + puzzleIndex, 0) == 1;
    }
}
