using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellAssigner : MonoBehaviour
{

    static public int[] PathDirection(int[,] mazeArray, int x, int y)  //Class that detirmines the entry and exit directions of the path. Layout of code is done to avoid going out array's bounds.
    {
        int[] direction = new int[] { 0, 0 };


        if (x != 0)
        {
            if (mazeArray[x - 1, y] == mazeArray[x, y] - 1)
            {
                direction[0] = 1;
            }
            if (mazeArray[x - 1, y] == mazeArray[x, y] + 1)
            {
                direction[1] = 1;
            }
        }

        if (x < mazeArray.GetLength(0) - 1)
        {
            if (mazeArray[x + 1, y] == mazeArray[x, y] - 1)
            {
                direction[0] = 3;
            }
            if (mazeArray[x + 1, y] == mazeArray[x, y] + 1)
            {
                direction[1] = 3;
            }
        }


        if (y != 0)
        {
            if (mazeArray[x, y - 1] == mazeArray[x, y] - 1 && y != 0)
            {
                direction[0] = 2;
            }
            if (mazeArray[x, y - 1] == mazeArray[x, y] + 1 && y != 0)
            {
                direction[1] = 2;
            }
        }

        if (y < mazeArray.GetLength(1) - 1)
        {
            if (mazeArray[x, y + 1] == mazeArray[x, y] - 1)
            {
                direction[0] = 4;
            }
            if (mazeArray[x, y + 1] == mazeArray[x, y] + 1)
            {
                direction[1] = 4;
            }
        }
        if (direction[0] == 0 || direction[1] == 0) //returns an error because 1st and last node. Needs fixing.
        {
            Debug.Log("Error found in x: " + x + " and y: " + y);
        }
        return direction;       //direction code is 1= left, 2= up, 3= right, 4= down. 0 is an error code.
    }


    static public bool BorderBlocks(int[,] mazeArray, int x, int y) //Sets any blocks to surrounding a path to be drawn.
    {
        bool toBeDrawn = false;
        if (x != 0)
        {
            if (mazeArray[x - 1, y] > 0)
            {
                toBeDrawn = true;
            }
        }

        if (x < mazeArray.GetLength(0)-1)
        {
            if (mazeArray[x + 1, y] > 0)
            {
                toBeDrawn = true;
            }
        }
        if (y != 0)
        {
            if (mazeArray[x, y - 1] > 0)
            {
                toBeDrawn = true;
            }
        }
        if (y < mazeArray.GetLength(1)-1)
        {
            if (mazeArray[x, y + 1] > 0)
            {
                toBeDrawn = true;
            }
        }
        



        return toBeDrawn;

    }
}


