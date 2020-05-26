using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathGenerator : MonoBehaviour
{

    public int[,] grid;             //path storage array

    public int startRow = 0;        //start quad - needed to reset player to start
    public int startCol = 0;        //after incorrect guess

    public int playerRow = 0;       //player position in grid,
    public int playerCol = 0;       //doubles as path generation marker

    public int gridRows = 30;        //set grid rows & columns
    public int gridColumns = 30;

    public int mapCode = 0;         //store quad position in path and availability during generation
    public int finalCode = 0;       //indicates final quad - match with mapcode to indicate success

    public int loopSafetyMax = 3000;  //control while loop safety

    public enum Direction { North, South, East, West, Blocked };
    private List<Direction> clearDirection = new List<Direction>();


    void Start()
    {
        grid = new int[gridRows, gridColumns];  //rows, columns
        clearDirection.Clear();                 //clear list
        ResetGrid();                            //set all elements to 0
        GeneratePath();
        PrintArray();
        MapPathPusher.SetupPlayer();
    }


    void ResetGrid()    //Set all elements to 0 which = available space
    {
        for (int i = 0; i < gridRows; i++)
        {
            for (int j = 0; j < gridColumns; j++)
            {
                grid[i, j] = 0;
            }
        }

        finalCode = 0;              //final code = last space in path
    }

    public void PrintArray()               // Set all elements to 0 which = available space
    {
        int rowLength = grid.GetLength(0);
        int colLength = grid.GetLength(1);
        string arrayString = "";
        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                arrayString += string.Format("{0} ", grid[i, j]);

            }
            arrayString += System.Environment.NewLine + System.Environment.NewLine;
        }

        Debug.Log(arrayString);
    }

    public void GeneratePath()
    {
        playerCol = startCol = Random.Range(0, gridColumns);    // pick starting coloumn, row is 0
        grid[startRow, startCol] = mapCode = 1;     // assign 1 to start square

        int loopSafety = 0;                 //use for preventing endless loop

        while (finalCode == 0 && loopSafety < loopSafetyMax)
        {
            AvailableMoveCheck();           //determine if directions are available
            SelectDir();                    // random selection of available directions

            loopSafety++;
            if (loopSafety >= loopSafetyMax)
                Debug.Log("Loop exceeded maximim allowed attempts.");

            //Debug.Log(finalCode);
            //Debug.Log(grid[playerRow, playerCol]);
        }
       
    }

    void AvailableMoveCheck()
    {
        if ((playerRow + 1 < gridRows) && (grid[playerRow + 1, playerCol] == 0))
            clearDirection.Add(Direction.North);

        if ((playerRow - 1 >= 0) && (grid[playerRow - 1, playerCol] == 0))
        {
            if (playerCol != 0 && playerCol != gridColumns - 1) //prevent going south into dead end
                clearDirection.Add(Direction.South);
        }

        if ((playerCol + 1 < gridColumns) && (grid[playerRow, playerCol + 1] == 0))
            clearDirection.Add(Direction.East);

        if ((playerCol - 1 >= 0) && (grid[playerRow, playerCol - 1] == 0))
            clearDirection.Add(Direction.West);
    }

    void SelectDir()
    {
        int select = 0;
        Direction selection;
        if (clearDirection.Count != 0) // select random direction from list
        {
            select = Random.Range(0, clearDirection.Count);
            selection = clearDirection[select];
        }
        else selection = Direction.Blocked; // no moves available

        if (selection != Direction.Blocked)
        {
            if (selection == Direction.North)
                playerRow = playerRow + 1;
            else if (selection == Direction.South)
                playerRow = playerRow - 1;
            else if (selection == Direction.East)
                playerCol = playerCol + 1;
            else if (selection == Direction.West)
                playerCol = playerCol - 1;

            mapCode = mapCode + 1;
        }
        else if (selection == Direction.Blocked)
        {
            grid[playerRow, playerCol] = -1; // set element to indicate no movement available
            BackUp();                   // find previous quad
            mapCode = mapCode - 1;      // set to previous map code

        }
        else
            Debug.Log("Path Failed");

        grid[playerRow, playerCol] = mapCode;
        clearDirection.Clear();

        if (playerRow == gridRows - 1)
            finalCode = mapCode;
    }

    void BackUp()
    {
        Debug.Log("In Back Up");
        int tempN = 0;
        int tempS = 0;
        int tempE = 0;
        int tempW = 0;

        if (playerRow < 7)
        {
            tempN = grid[playerRow + 1, playerCol];
        }

        if (playerRow > 0)
        {
            tempS = grid[playerRow - 1, playerCol];
        }

        if (playerCol < 9)
        {
            tempE = grid[playerRow, playerCol + 1];
        }

        if (playerCol > 0)
        {
            tempW = grid[playerRow, playerCol - 1];
        }

        // determine highest value of temp int's. Highest value is the
        // square previous to dead end. Set playerRow or or playerCol accordingly.

        if (tempN > tempS)
        {
            if (tempN > tempE)
            {
                if (tempN > tempW)
                {
                    playerRow = playerRow + 1;
                }
                else
                {
                    playerCol = playerCol - 1;
                }
            }
            else if (tempE > tempW)
            {
                playerCol = playerCol + 1;
            }
            else
            {
                playerCol = playerCol - 1;
            }
        }
        else if (tempS > tempE)
        {
            if (tempS > tempW)
            {
                playerRow = playerRow - 1;
            }
            else
            {
                playerCol = playerCol - 1;
            }
        }
        else if (tempE > tempW)
        {
            playerCol = playerCol + 1;
        }
        else
        {
            playerCol = playerCol - 1;
        }
    }
}