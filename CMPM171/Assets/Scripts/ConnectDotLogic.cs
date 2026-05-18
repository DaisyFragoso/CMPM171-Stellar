using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectDotLogic : MonoBehaviour
{
    public static ConnectDotLogic Instance;

    public int maxPoints = 3;
    public Text pointsText;
    public GameObject levelCompleteUI;

    private int points = 0;
    public int currentDot = 1;

    public List<GameObject> createdLines = new List<GameObject>();

    void Start()
    {
        Instance = this;
        UpdatePointsText();
        levelCompleteUI.SetActive(false);
    }

    void UpdatePointsText()
    {
        pointsText.text = points + "/" + maxPoints;

        if (points == maxPoints)
        {
            levelCompleteUI.SetActive(true);
        }
    }

    public bool CanConnect(int fromDot, int toDot)
    {
        return fromDot == currentDot && toDot == currentDot + 1;
    }

    public void AddConnection()
    {
        points++;
        currentDot++;
        UpdatePointsText();
    }

    public void ClearLines()
    {
        foreach (GameObject line in createdLines)
        {
            if (line != null)
            {
                Destroy(line);
            }
        }

        createdLines.Clear();
        points = 0;
        currentDot = 1;
        UpdatePointsText();
        levelCompleteUI.SetActive(false);
    }
}
