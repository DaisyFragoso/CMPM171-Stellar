using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConstellationLogic : MonoBehaviour
{
    public static ConstellationLogic Instance;

    public TMP_Text pointsText;
    public GameObject levelCompleteUI;

    public List<GameObject> createdLines = new List<GameObject>();

    private HashSet<string> playerConnections = new HashSet<string>();

    private HashSet<string> correctConnections = new HashSet<string>()
    {
        "3,3-3,6",
        "3,6-6,6",
        "6,3-6,6",
        "3,3-6,3"
    };

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        levelCompleteUI.SetActive(false);
        UpdateText();
    }

    public void AddConnection(int rowA, int colA, int rowB, int colB)
    {
        string connection = GetConnectionKey(rowA, colA, rowB, colB);

        if (!playerConnections.Contains(connection))
        {
            playerConnections.Add(connection);
            UpdateText();
        }
    }

    public void CheckAnswer()
    {
        if (playerConnections.SetEquals(correctConnections))
        {
            Debug.Log("Correct square!");
            levelCompleteUI.SetActive(true);
        }
        else
        {
            Debug.Log("not the Little Dipper");
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

        levelCompleteUI.SetActive(false);
        UpdateText();

        Debug.Log("Puzzle reset");
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
