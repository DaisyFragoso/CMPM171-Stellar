using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConstellationLogic : MonoBehaviour
{
    public static ConstellationLogic Instance;

    public TMP_Text pointsText;
    // public GameObject levelUI;
    public GameObject levelCompleteUI;
    public GameObject levelCompleteAnim;
    public GameObject levelIncorrectUI;
    public AudioClip undoSound;
    public AudioClip successSound;

    public List<GameObject> createdLines = new List<GameObject>();

    private HashSet<string> playerConnections = new HashSet<string>();
        
    private List<string> connectionHistory = new List<string>();

    private HashSet<string> correctConnections = new HashSet<string>()
    {
        "2,5-3,3",
        "2,5-3,6",
        "3,6-4,4",
        "3,3-4,4",
        "4,4-5,4",
        "5,4-6,3",
        "6,3-7,2",
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

        if (playerConnections.Contains(connection))
        {
            Debug.Log("Already connected this line: " + connection);
            SoundFXManager.Instance.PlaySound(undoSound, transform, 1f);
            return false;
        }

        if (correctConnections.Contains(connection))
        {
            Debug.Log("correcct connection: " + connection);
        }
        else
        {
            Debug.LogWarning("We dont need this connection: " + connection);
        }

        playerConnections.Add(connection);
        connectionHistory.Add(connection);
        UpdateText();

        SoundFXManager.Instance.PlaySound(lineDrawSound, transform, 1f);

        return true;
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
            SoundFXManager.Instance.PlaySound(undoSound, lastLine.transform, 1f);
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
            Debug.Log("Correct constellation!");
            foreach (GameObject line in createdLines)
            {
                if (line != null)
                {
                    Destroy(line);
                }
            }
            levelCompleteUI.SetActive(true);
            levelCompleteAnim.SetActive(true);
            levelIncorrectUI.SetActive(false);
            // levelUI.SetActive(false);
            SoundFXManager.Instance.PlaySound(successSound, transform, 1f);
        }
        else
        {
            Debug.Log("not the Little Dipper");
            levelIncorrectUI.SetActive(true);
            SoundFXManager.Instance.PlaySound(undoSound, transform, 1f);
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
        pointsText.text = playerConnections.Count + "/6";
    }

    string GetConnectionKey(int rowA, int colA, int rowB, int colB)
    {
        string a = rowA + "," + colA;
        string b = rowB + "," + colB;

        return string.Compare(a, b) < 0 ? a + "-" + b : b + "-" + a;
    }
}
