using UnityEngine;

public class AutoSaveOnQuit : MonoBehaviour
{
    void OnApplicationQuit()
    {
        SaveManager.SaveGame();
        Debug.Log("Auto saved on quit.");
    }
}
