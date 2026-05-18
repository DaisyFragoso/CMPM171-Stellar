using UnityEngine;
using UnityEngine.UI;

public class DotGridGenerator : MonoBehaviour
{
    public GameObject dotPrefab;
    public GameObject linePrefab;

    public int rows = 9;
    public int columns = 9;

    public float spacing = 60f;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int row = 1; row <= rows; row++)
        {
            for (int col = 1; col <= columns; col++)
            {
                GameObject dot = Instantiate(dotPrefab, transform);

                RectTransform rect = dot.GetComponent<RectTransform>();

                float x = (col - 5) * spacing;
                float y = (5 - row) * spacing;

                rect.anchoredPosition = new Vector2(x, y);

                GridDot gridDot = dot.GetComponent<GridDot>();
                gridDot.Setup(row, col, linePrefab);
            }
        }
    }
}
