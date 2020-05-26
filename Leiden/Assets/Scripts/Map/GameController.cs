using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject myPrefab;
    public GameObject myPrefab2;


    public int[,] mazeArray;
    public int loopMaxRun = 3000;
    public int expectedMapSize = 200; //Debug settings. Actual formulae found in documentation. 
    public int mazeSize = 30;

    Position playerPos = new Position(0, 0);
    Position startNode = new Position(0, 0);
    Position endNode = new Position(0, 0);
    public enum Direction { North, South, East, West, Blocked };
    private List<Direction> clearDirection = new List<Direction>();


    private int mapCode = 0;
    private int finalCode = 0;
    private int[,] goalNode;

    int debugCount = 0;


    private List<string> direction = new List<string>();

    void Start() //here
    {
        mazeArray = new int[mazeSize, mazeSize]; // rows and collums. Always mazeSize^2.
        clearDirection.Clear();
        ResetArray();               // set all elements to 0
        GeneratePath();             // generate random path
        PrintArray();
        TerrainBlocks();
        MapBuilder.BuildIt(mazeArray, myPrefab);
        MapPathPusher.SetupPlayer();

    }



    void ResetArray()               // Set all elements to 0 which = available space
    {
        for (int i = 0; i < mazeSize; i++)
        {
            for (int j = 0; j < mazeSize; j++)
            {
                mazeArray[i, j] = 0;
            }
        }

        finalCode = 0;              // final code = last space in path
        return;
    }
    public void PrintArray()               // Set all elements to 0 which = available space
    {
        int rowLength = mazeArray.GetLength(0);
        int colLength = mazeArray.GetLength(1);
        string arrayString = "";
        for (int i = 0; i < colLength; i++)
        {
            
            for (int j = 0; j < rowLength; j++)
            {
                arrayString += string.Format("{0} ", mazeArray[i, j]);
            }
            arrayString += System.Environment.NewLine + System.Environment.NewLine;
        }

        Debug.Log(arrayString);
    }


    Position GenerateNode(int NodeArea, Position node)
    {

        int x = 0, y = 0, xCentrePositive = 0, yCentrePositive = 0;
        x = Random.Range(0, mazeSize/5); 
        y = Random.Range(0, mazeSize / 5);
        xCentrePositive = Random.Range(0, 1);
        yCentrePositive = Random.Range(0, 1);


        switch (NodeArea)
        {
            case 0: //Top left
                node.x = 0 + x;
                node.y = mazeSize-1 - y;
                break;

            case 1: //Top Right
                node.x = mazeSize-1 - x;
                node.y = mazeSize-1 - y;
                break;

            case 2: //Bottom Left
                node.x = 0 + x;
                node.y = 0 + y;
                break;

            case 3: //Bottom right
                node.x = mazeSize-1 - x;
                node.y = 0 + y;
                break;

            case 4: // Centre
                node.x = mazeSize / 2 + (x * -1 + x * (2 * xCentrePositive)); //Equation essentially makes it +x or -x 
                node.y = mazeSize / 2 + (y * -1 + y * (2 * yCentrePositive));
                break;

        }


        return node;
    }





    void GeneratePath()
    {
        int temp;

        var nodeSpawnOptions = new List<int> { 0, 1, 2, 3, 4 }; //List used over array to be able to remove elements
        temp = Random.Range(0, nodeSpawnOptions.Count);

        startNode = GenerateNode(nodeSpawnOptions[temp], startNode);
        nodeSpawnOptions.RemoveAt(temp);
        playerPos.x = startNode.x; //Taking x and y instead of just copying whole variable to allow for more development later
        playerPos.y = startNode.y;
        Debug.Log("X = " + playerPos.x);
        Debug.Log("Y = " + playerPos.y);

        temp = Random.Range(0, nodeSpawnOptions.Count);
        endNode = GenerateNode(nodeSpawnOptions[temp], endNode);

        mazeArray[playerPos.x, playerPos.y] = mapCode = 1; // assign 1 to start square

        Debug.Log("Player start = X: " + playerPos.x + " Y: " + playerPos.y);
        Debug.Log("Player goal = X: " + endNode.x + " Y: " + endNode.y);
        int loopCounter = 0; //make unable to infinite loop
        
        while (finalCode == 0 && loopCounter < loopMaxRun)
        {

            MoveOptions();
            SelectDir();
            loopCounter++;


        }
        Debug.Log("Loop ended on :" + loopCounter + ". With a debug count of :" + debugCount);

        //OffShoot();

    }

    void MoveOptions()
    {
        if ((playerPos.y + 1 < mazeSize) && (mazeArray[playerPos.x, playerPos.y+1] == 0))
            clearDirection.Add(Direction.North);

        if ((playerPos.y - 1 >= 0) && (mazeArray[playerPos.x, playerPos.y-1] == 0))
        {
            //if (playerPos.y != 0 && playerPos.y != mazeSize - 1) //prevent going south into dead end
                clearDirection.Add(Direction.South);
        }

        if ((playerPos.x + 1 < mazeSize) && (mazeArray[playerPos.x+1, playerPos.y] == 0))
            clearDirection.Add(Direction.East);
        if ((playerPos.x - 1 >= 0) && (mazeArray[playerPos.x-1, playerPos.y] == 0))
            clearDirection.Add(Direction.West);

    }

    Direction OptimalPath() //WIP
    {
        Position optimalPath = new Position((playerPos.x - endNode.x), (playerPos.y - endNode.y));
        Direction temp = Direction.Blocked;

        if (optimalPath.x < 0 && clearDirection.Contains(Direction.East)) 
        {
            temp = Direction.East;
        }
        else if (optimalPath.x > 0 && clearDirection.Contains(Direction.West))
        {
            temp = Direction.West;
        }
        else if (optimalPath.y < 0 && clearDirection.Contains(Direction.North))
        {
            temp = Direction.North;
        }
        else if (optimalPath.y > 0 && clearDirection.Contains(Direction.South))
        {
            temp = Direction.South;
        }


        return temp;
    }

    Direction WorstPath()
    {
        Position optimalPath = new Position((playerPos.x - endNode.x), (playerPos.y - endNode.y));
        Direction temp = Direction.Blocked;

        if (optimalPath.x > 0 && clearDirection.Contains(Direction.East))
        {
            temp = Direction.East;
        }
        else if (optimalPath.x < 0 && clearDirection.Contains(Direction.West))
        {
            temp = Direction.West;
        }
        else if (optimalPath.y > 0 && clearDirection.Contains(Direction.North))
        {
            temp = Direction.North;
        }
        else if (optimalPath.y < 0 && clearDirection.Contains(Direction.South))
        {
            temp = Direction.South;
        }


        return temp;
    }

    void TerrainBlocks()
    {


        for (int i = 1; i < mazeSize-1; i++)
        {
            for (int j = 1; j < mazeSize-1; j++)
            {

                if (mazeArray[i,j] == 0 && (mazeArray[i,j+1] > 0|| mazeArray[i, j - 1] > 0 || mazeArray[i+1, j] > 0 || mazeArray[i-1, j] > 0))
                {
                    mazeArray[i, j] = -1;
                }

                /*
                if (branches > 0 && mazeArray[i, j] > 0)
                {





                    rngMachine = Random.Range(0.0f, 1.0f);
                    if (rngMachine > 0.9f)
                    {
                        CreateBranch(new Position (i,j));
                        Debug.Log("oi");
                        branches--;
                    }
                    
                }
                //mazeArray[i, j] = 0;
                */
            }
        }

    }

    void CreateBranch(Position temp)
    {
        mapCode = mazeArray[temp.x, temp.y];
        int loopCounter = 0;
        playerPos.x = temp.x;
        playerPos.y = temp.y;
        int j = 0;
        while (j < 7 && loopCounter < loopMaxRun)
        {

            MoveOptions();
            SelectDir();
            loopCounter++;
            j++;


        }
    }



    void SelectDir() //picks a random direction. We want this to include bias.
    {
        int select = 0;
        int xDistanceToEnd = 0;
        int yDistanceToEnd = 0;
        float endBias = (mapCode * 1.0f)/ (expectedMapSize*2);
        //Debug.Log("endBias is " + endBias.ToString("F4"));
        float rngBias = Random.Range(0.0f, 0.7f);

        xDistanceToEnd = System.Math.Abs(playerPos.x - endNode.x);
        yDistanceToEnd = System.Math.Abs(playerPos.y - endNode.y);
        

        Direction selection;
        if (clearDirection.Count != 0) // select random direction from list  --- This is where direction bias will go
        {
            if (rngBias < endBias)
            {
                selection = OptimalPath();
                if (selection == Direction.Blocked) //In the case where the direct route is blocked, a random step is chosen
                {
                    {
                        select = Random.Range(0, clearDirection.Count);
                        selection = clearDirection[select];
                    }
                }
            }
            else if (rngBias > (endBias*2) )
            {
                selection = WorstPath();
                if (selection == Direction.Blocked) //In the case where the direct route is blocked, a random step is chosen
                {
                    {
                        select = Random.Range(0, clearDirection.Count);
                        selection = clearDirection[select];
                    }
                }
            }
            /* Inherently flawed offshoot system
            else if (rngBias > (endBias * 8) && mapCode > 20)
            {
                DeadEnd();
                Debug.Log("deadend clause");
                selection = OptimalPath();
                if (selection == Direction.Blocked) //In the case where the direct route is blocked, a random step is chosen
                {
                    {
                        select = Random.Range(0, clearDirection.Count);
                        selection = clearDirection[select];
                    }
                }

            }
            */
            else
            {
                select = Random.Range(0, clearDirection.Count);
                selection = clearDirection[select];
            }
        }
        else selection = Direction.Blocked; // no moves available

        if (selection != Direction.Blocked)
        {
            if (selection == Direction.North)
            {

                playerPos.y += 1;
               // Debug.Log("hit North");
            }
            else if (selection == Direction.South)
            {
                playerPos.y -= 1;
              //  Debug.Log("hit South");
            }
            else if (selection == Direction.East)
            {
                playerPos.x += 1;
              //  Debug.Log("hit east");
            }
            else if (selection == Direction.West)
            {
                playerPos.x -= 1;
               // Debug.Log("hit weast");
            }
            mapCode = mapCode + 1;
        }
        else if (selection == Direction.Blocked)
        {
           // Debug.Log("Fucked it");
            mazeArray[playerPos.x, playerPos.y] = -1; // set element to indicate no movement available
            BackUp();                   // find previous quad
            mapCode = mapCode - 1;      // set to previous map code

        }
        else
            Debug.Log("Path Failed");

        //Debug.Log("x = " + playerPos.x);
        //Debug.Log("y = " + playerPos.y);


        mazeArray[playerPos.x, playerPos.y] = mapCode;
        clearDirection.Clear();

        if ((playerPos.x == endNode.x) && (playerPos.y == endNode.y))
        { //this is the goal selection, currently just checks row
            finalCode = mapCode;
            Debug.Log("final = " + finalCode);
        }
    }

    void DeadEnd()
    {
        int stepsBack = Random.Range(0, (mazeSize / 3));
        for (int i = 0; i < stepsBack; i++)
        {
            BackUp();                   // find previous quad
            mapCode = mapCode - 1;
        }

    }

    void BackUp()
    {
        // Debug.Log("In Back Up");
        // Debug.Log("x = " + playerPos.x);
        // Debug.Log("y = " + playerPos.y);
        //Debug.Log("Hit Backup on Mapcode: " + mapCode);
        //PrintArray();
        debugCount += 1;

        int[] tempDirections = new int[] { 0, 0, 0, 0 }; //North, East, South, West (respectively)



        if (playerPos.y < mazeSize-1)
        {
            tempDirections[0] = mazeArray[playerPos.x,playerPos.y+1];
        }

        if (playerPos.y > 0)
        {
            tempDirections[2] = mazeArray[playerPos.x, playerPos.y-1];
        }

        if (playerPos.x < mazeSize-1)
        {
            tempDirections[1] = mazeArray[playerPos.x+1, playerPos.y];
        }

        if (playerPos.x > 0)
        {
            tempDirections[3] = mazeArray[playerPos.x-1, playerPos.y];
        }

        for (int i = 0; i < tempDirections.Length; i++)
        {
            if (tempDirections[i] >= mapCode)
            {
                tempDirections[i] = 0;
            }
        }
        // determine highest value of temp int's. Highest value is the
        // square previous to dead end. Set playerRow or or playerCol accordingly.

        if (tempDirections[0] > tempDirections[2])
        {
            if (tempDirections[0] > tempDirections[1])
            {
                if (tempDirections[0] > tempDirections[3])
                {
                    playerPos.y +=1;
                }
                else
                {
                    playerPos.x -=1;
                }
            }
            else if (tempDirections[1] > tempDirections[3])
            {
                playerPos.x +=1;
            }
            else
            {
                playerPos.x -=1;
            }
        }
        else if (tempDirections[2] > tempDirections[1])
        {
            if (tempDirections[2] > tempDirections[3])
            {
                playerPos.y -=1;
            }
            else
            {
                playerPos.x -=1;
            }
        }
        else if (tempDirections[1] > tempDirections[3])
        {
            playerPos.x +=1;
        }
        else if (tempDirections[3] > tempDirections[1])
        {
            playerPos.x -=1;
        }
        else
        {
            Debug.Log("ERROR ERROR ERROR: " + mapCode);
        }
    }
}