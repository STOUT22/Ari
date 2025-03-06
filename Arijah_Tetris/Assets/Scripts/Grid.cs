using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GridLogic : MonoBehaviour
{
    public int width = 10;
    public int height = 21;
    private Transform[,] grid;

    // Start is called before the first frame update
    void Start()
    {
        grid = new Transform[width, height];
    }

    public bool IsCellOccupied(Vector2Int position)
    {
        if (position.x < 0 || position.x >= width || position.y < 0 || position.y >= height)// out of bounds parameters
        {
            return true; // true if there is an object at out of bounds position
        }

        return grid[position.x, position.y] != null; // returns block to playing field
    }

    public void AddToGrid(Transform piece)
    {
        foreach (Transform block in piece)
        {
            Vector2Int position = Vector2Int.RoundToInt(block.position);
            grid[position.x, position.y] = block;
        }
    }

    public bool IsLineFull(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] == null)
            {
                return false;
            }
        }
        return true;
    }
    public void ClearLine(int y)
    {
        for (int x = 0; x < width; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    public void ShiftRowDown(int clearedRow)
    {
        for (int y = clearedRow; y < height - 1; y++)
        {
            for (int x = 0; x < width; x++)
            {
                grid[x, y] = grid[x, y + 1];
                if (grid[x, y] != null)
                {
                    grid[x, y].position += Vector3.down;
                }
                grid[x, y + 1] = null;
            }
        }
    }

    public void ClearFullLines()
    {
        int linesCleared = 0;
        for (int y = 0; y < height; y++)
        {
            if (IsLineFull(y))
            {
                linesCleared++;
                ClearLine(y);
                ShiftRowDown(y);
                y--;//recheck the current row after shifting
            }
        }

        FindObjectOfType<Score>().AddScore(linesCleared);
    }
}
