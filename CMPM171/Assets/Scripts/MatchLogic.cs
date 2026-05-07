using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MatchLogic : MonoBehaviour
{
    public static MatchLogic Instance;

    public int maxPoints=3;
    public Text pointsText;
    public GameObject levelCompleteUI;
    private int points=0;

    public List<GameObject> createdLines = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        UpdatePointsText();
    }

    // Update is called once per frame
    void UpdatePointsText()
    {
        pointsText.text = points + "/" + maxPoints;
        if (points == maxPoints) {
            levelCompleteUI.SetActive(true);
        }
    }

    public static void AddPoint()
    {
        AddPoints(1);
    }

    public static void AddPoints(int points){
        Instance.points += points;
        Instance.UpdatePointsText();
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
    }
}
