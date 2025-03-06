using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TetrisPiece : MonoBehaviour
{
    private GridLogic grid;
    private float dropInterval = 1.0f; //time between automatic drops
    private float dropTimer;

    // Start is called before the first frame update
    void Start()
    {
        grid = FindObjectOfType<GridLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        HandleAutomaticDrop();
    }

    private void HandleInput()//input thingy
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) Move(Vector3.left);
        if (Input.GetKeyDown(KeyCode.RightArrow)) Move(Vector3.right);
        if (Input.GetKeyDown(KeyCode.DownArrow)) Move(Vector3.down);

        if (Input.GetKeyDown(KeyCode.Space)) RotatePiece();
    }

    private void HandleAutomaticDrop()
    {
        dropTimer -= Time.deltaTime;

        if (dropTimer <= 0)
        {
            Move(Vector3.down);
            dropTimer = dropInterval; //reset the timer
        }
    }

    //rotates the piece
    private void RotatePiece()
    {
        transform.Rotate(0, 0, 90);

        if (!IsValidPosition())//undos if not valid
        {
            transform.Rotate(0, 0, -90);
        }
    }

    private void Move(Vector3 direction)//moves piece
    {
        transform.position += direction;

        if (!IsValidPosition())
        {
            transform.position -= direction;//undos if not valid
            if (direction == Vector3.down) //if moving down fails{
            {
                LockPiece();//locks piece
            }
        }
    }

       private void LockPiece()
    {
        foreach (Transform block in transform)
        {
            Vector2Int position = Vector2Int.RoundToInt(block.position);
            if (position.y >= grid.height - 2) // If piece locks at the top, Game Over
            {
                FindObjectOfType<GameManager>().GameOver();
                return;
            }
        }

        grid.AddToGrid(transform);
        grid.ClearFullLines();
        FindObjectOfType<TetrisSpawner>().SpawnPiece();
        Destroy(this);
    }


private bool IsValidPosition()//checks to see if the piece is able to move to the next space
    {
        foreach (Transform block in transform)
        {
            Vector2Int position = Vector2Int.RoundToInt(block.position);

            if (grid.IsCellOccupied(position))
            {
                return false;
            }
        }
        return true;
    }
}
