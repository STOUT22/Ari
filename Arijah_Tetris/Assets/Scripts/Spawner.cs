using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisSpawner : MonoBehaviour
{
    public GameObject[] tetrominoPrefabs;
    private GridLogic grid;
    private GameObject nextPiece;
    // Start is called before the first frame update
    void Start()
    {
        grid = FindObjectOfType<GridLogic>();
        SpawnPiece();
    }

    public void SpawnPiece()
    {
        Vector3 spawnPosition = new Vector3(Mathf.Floor(grid.width / 2f), grid.height - 2, 0);
        if (nextPiece != null)
        {
            nextPiece.SetActive(true);
            nextPiece.transform.position = spawnPosition;
        }
        else
        {
            nextPiece = InstantiateRandomPiece();
            nextPiece.transform.position = spawnPosition;
        }

        nextPiece = InstantiateRandomPiece();
        nextPiece.SetActive(false);
    }

    private GameObject InstantiateRandomPiece()
    {
        int index = Random.Range(0, tetrominoPrefabs.Length);//randomly selects a tetromino and puts it into play
        return Instantiate(tetrominoPrefabs[index]);
    }
}
