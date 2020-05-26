using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{
   // public static MapCell temp = new MapCell();
    public static List<MapCell> mapCells;
    public static List<MapCell> mapTerrainCells;
    //public static List<MapNode> mapNodes;



    static void MapAssetFinder(int entry, int exit, int previousAsset, GameObject[] prefabArray, out GameObject selectedObject, out int selectedObjectID)
    {
        int k;
        if (entry - exit == 2||entry - exit == -2)
        {
            if (previousAsset == 0)     //If previous asset was a straight wall then produce the second straight wall with the continuing pattern
            {
                k = Random.Range(0, 10);
                if(k > 5)
                {
                    selectedObjectID = 1;
                    selectedObject = prefabArray[1];
                }
                else
                {
                    selectedObjectID = 6;
                    selectedObject = prefabArray[6];
                }
            }
            else
            {
                k = Random.Range(0, 10);

                if (k > 5)
                {
                    selectedObjectID = 0;
                    selectedObject = prefabArray[0];
                }
                else
                {
                    selectedObjectID = 5;
                    selectedObject = prefabArray[5];
                }

            }
            
        }
        else if (entry - exit == -1 || entry - exit == 3)
        {
            selectedObjectID = 3;
            selectedObject = prefabArray[3];
        }
        else if (entry - exit == 1 || entry - exit == -3)
        {
            selectedObjectID = 2;
            selectedObject = prefabArray[2];
        }
        /*else if (entry-exit == )
        {
            selectedObjectID = 3;
            selectedObject = prefabArray[2];
        }
        */



        else
        {
            Debug.Log(prefabArray.Length);
            if (prefabArray[2] == null)
            {
                Debug.Log("wtf?");
            }
            selectedObjectID = 2;
            selectedObject = prefabArray[2];
        }






    }


    static Vector3 MapRotationFinder(int entry, int exit)
    {
        Vector3 j = new Vector3(0, 0, 0);

        float rotation;

        if (entry - exit == 2 || entry - exit == -2)
        {
            rotation = -90.0f * entry;
            j = new Vector3(0, rotation, 0);
        }
        else if (entry - exit == -1 || entry - exit == 3)
        {
            rotation = -90.0f * entry;
            rotation += 270.0f;
            j = new Vector3(0, rotation, 0);

        }
        else if (entry - exit == 1 || entry - exit == -3)
        {
            rotation = -90.0f * entry;
            rotation += 90.0f;
            j = new Vector3(0, rotation, 0);
        }
        





        return j;
    }


    private void Start()
    {
       
    }

    public static void FireEnvironmentalNodes(List<MapCell> mapCells)
    {
        EnvironmentalNodeFirer.FireNodes(mapCells, "Environmental");
        EnvironmentalNodeFirer.FireNodes(mapCells, "Pickup");
        EnvironmentalNodeFirer.FireNodes(mapCells, "Treasure");
    }



    public static void BuildIt(int [,] mazeArray, GameObject mapBlock01)
    {
        int previousAsset = 0;
        mapCells = new List<MapCell>();
        mapTerrainCells = new List<MapCell>();
        //mapNodes = new List<MapNode>();
        MapCell temp;
        GameObject[] fetchedPrefabs = GameManager.fetchMapTerrainPrefabs();
        MapController mapController = GameObject.Find("MapCreation").GetComponent<MapController>();
        GameObject parentNode = GameObject.Find("MapContainer");
        GameObject selectedObject;
        int selectedObjectID;

        int counter = 0;
        for (int i = 0; i < mazeArray.GetLength(0); i++)
        {
            for (int j = 0; j < mazeArray.GetLength(1); j++)
            {
                if (mazeArray[i, j] > 0)
                {
                    
                    selectedObjectID = 0;
                    int [] directionArray = CellAssigner.PathDirection(mazeArray, i, j);
                    MapAssetFinder(directionArray[0], directionArray[1], previousAsset, fetchedPrefabs, out selectedObject, out selectedObjectID);
                    temp = new MapCell
                    {
                        mapID = counter,
                        pathID = mazeArray[i, j],
                        mapPosition = new Vector3(i * 8, 0, j * 8),
                        gameObject = selectedObject,
                        cellPrefabID = selectedObjectID,
                        entryDirection = directionArray[0],
                        exitDirection = directionArray[1],
                        toBeDrawn = true,
                        mapRotation = MapRotationFinder(directionArray[0], directionArray[1]),
                        mapCellNodes = MapNodeXML.FetchNodes(selectedObjectID)
                    };
                    previousAsset = temp.cellPrefabID;
                    mapCells.Add(temp);
                    //Destroy(selectedObject);
                    
                    //mapNodes.Add(MapNodeXML.FetchNodes(temp.cellPrefabID));






                }
                else if (mazeArray[i, j] <0)
                {
                    temp = new MapCell
                    {
                        mapID = counter,
                        pathID = mazeArray[i, j],
                        mapPosition = new Vector3(i * 8, -2, j * 8),
                        gameObject = mapBlock01,
                        cellPrefabID = 0,
                        entryDirection = 0,
                        exitDirection = 0,
                        toBeDrawn = CellAssigner.BorderBlocks(mazeArray, i, j),
                        mapRotation = new Vector3(0,0,0)
                    };

                    mapTerrainCells.Add(temp);



                    //Instantiate(myObject2, new Vector3(i, 0, j), Quaternion.identity);
                }
                counter++;
            }
        }
        GameObject tempty = new GameObject();
        foreach (var item in mapCells)
        {
            tempty = Instantiate(item.gameObject, item.mapPosition, Quaternion.identity);
            tempty.transform.Rotate(item.mapRotation);
            tempty.GetComponent<MapCellAttachment>().cellData = item;
            tempty.GetComponent<MapCellAttachment>().Invoke("InitializeVariables", 0);
            mapController.pathObjArray[item.pathID-1] = tempty;
            tempty.transform.parent = parentNode.transform;


        }
        foreach (var item in mapTerrainCells)
        {
            if (item.toBeDrawn == true)
            {
                tempty = Instantiate(item.gameObject, item.mapPosition, Quaternion.identity);
                //tempty.GetComponent<MapCellAttachment>().cellData = item;
                tempty.transform.parent = parentNode.transform;

            }
        }
        //Destroy(tempty);
        FireEnvironmentalNodes(mapCells);
        MapPathPusher.SetupPlayer();
            
    }
}
