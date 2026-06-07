using UnityEngine;

public class GameSceneLoader : MonoBehaviour
{
    void Start()
    {
        if (SaveManager.HasSave())
        {
            SaveManager.LoadGame();
            SaveManager.LoadPlayerPosition();
        }
    }
}
