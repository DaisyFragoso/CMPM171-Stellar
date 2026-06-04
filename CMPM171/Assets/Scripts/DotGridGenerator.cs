using UnityEngine;
using UnityEngine.UI;

public class DotGridGenerator : MonoBehaviour
{
    public GameObject dotPrefab;
    public GameObject linePrefab;

    public int rows = 9;
    public int columns = 9;

    public float spacing = 115f;
    public float scrambleAmount = 20f;

    // Same seed = same random layout every time
    public int randomSeed = 12345;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        Random.InitState(randomSeed);

        for (int row = 1; row <= rows; row++)
        {
            for (int col = 1; col <= columns; col++)
            {
                GameObject dot = Instantiate(dotPrefab, transform);

                RectTransform rect = dot.GetComponent<RectTransform>();

                float centerRow = (rows + 1) / 2f;
                float centerCol = (columns + 1) / 2f;

                float x = (col - centerCol) * spacing;
                float y = (centerRow - row) * spacing;

                float randomX = Random.Range(-scrambleAmount, scrambleAmount);
                float randomY = Random.Range(-scrambleAmount, scrambleAmount);

                rect.anchoredPosition = new Vector2(x + randomX, y + randomY);

                GridDot gridDot = dot.GetComponent<GridDot>();
                gridDot.Setup(row, col, linePrefab);
            }
        }
    }
}
