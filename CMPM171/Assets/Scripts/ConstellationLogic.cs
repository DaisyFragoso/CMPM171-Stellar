using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConstellationLogic : MonoBehaviour
{
    public static ConstellationLogic Instance;

    public TMP_Text pointsText;
    public GameObject levelCompleteUI;
    public GameObject levelIncorrectUI;

    public List<GameObject> createdLines = new List<GameObject>();

    private HashSet<string> playerConnections = new HashSet<string>();
        
    private List<string> connectionHistory = new List<string>();

    private HashSet<string> correctConnections = new HashSet<string>()
    {
        "3,3-3,6",
        "3,6-6,6",
        "6,3-6,6",
        "3,3-6,3"
    };

    public AudioClip lineDrawSound;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        levelCompleteUI.SetActive(false);
        levelIncorrectUI.SetActive(false);
        UpdateText();
    }

    public bool AddConnection(int rowA, int colA, int rowB, int colB)
    {
        string connection = GetConnectionKey(rowA, colA, rowB, colB);

        if (!playerConnections.Contains(connection))
        {
            playerConnections.Add(connection);
            connectionHistory.Add(connection);
            UpdateText();
            SoundFXManager.Instance.PlaySound(lineDrawSound, transform, 1f);
            return true;
        }
        return false;
    }

    public void UndoLastLine()
    {
        if (createdLines.Count == 0)
        {
            //Debug.Log("No lines to undo");
            return;
        }

        int lastIndex = createdLines.Count - 1;

        GameObject lastLine = createdLines[lastIndex];

        if (lastLine != null)
        {
            Destroy(lastLine);
        }

        createdLines.RemoveAt(lastIndex);

        if (connectionHistory.Count > 0)
        {
            int lastConnectionIndex = connectionHistory.Count - 1;
            string lastConnection = connectionHistory[lastConnectionIndex];

            playerConnections.Remove(lastConnection);
            connectionHistory.RemoveAt(lastConnectionIndex);
        }

        levelIncorrectUI.SetActive(false);
        UpdateText();

        //Debug.Log("Undid last line");
    }

    public void CheckAnswer()
    {
        if (playerConnections.SetEquals(correctConnections))
        {
            //Debug.Log("Correct square!");
            foreach (GameObject line in createdLines)
            {
                if (line != null)
                {
                    Destroy(line);
                }
            }

            levelCompleteUI.SetActive(true);

        }
        else
        {
            Debug.Log("not the Little Dipper");
            levelIncorrectUI.SetActive(true);
        }
    }

    public void RedoPuzzle()
    {
        foreach (GameObject line in createdLines)
        {
            if (line != null)
            {
                Destroy(line);
            }
        }

        createdLines.Clear();
        playerConnections.Clear();
        connectionHistory.Clear();

        levelCompleteUI.SetActive(false);
        levelIncorrectUI.SetActive(false);
        UpdateText();

        //Debug.Log("Puzzle reset");
    }

    void UpdateText()
    {
        pointsText.text = playerConnections.Count + "/4";
    }

    string GetConnectionKey(int rowA, int colA, int rowB, int colB)
    {
        string a = rowA + "," + colA;
        string b = rowB + "," + colB;

        return string.Compare(a, b) < 0 ? a + "-" + b : b + "-" + a;
    }
}
