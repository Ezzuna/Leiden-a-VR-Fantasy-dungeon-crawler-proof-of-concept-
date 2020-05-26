using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour
{

    public GameObject[] pathObjArray;
    private List<Encounter> encounterList;
    public Encounter[] encounterArray;
    public int currentPathID;
    public int goalPathID;
    public int[] pathEncounters;

    public void FetchPathEncounters()
    {
        pathEncounters = new int[pathObjArray.Length];
        pathEncounters = PathEncounterGenerator.generatePathEvents(pathObjArray.Length);
        encounterList = GameObject.Find("MapCreation").GetComponent<EncounterNodeXML>().FetchNodes();
        encounterArray = encounterList.ToArray();
        //SpawnEncounters();
    }

    public void PlayerProgressed()
    {
        SpawnEncounters();
    }

    public int GetCurrentPathID()
    {
        return this.currentPathID;
    }

    public void SpawnEncounters()
    {
        if (currentPathID == 1)
        {
            for (int i = 1; i < 10; i++)            //Spawns initial trees. Not an else if below because overlap occurs between path 5 and 10.
            {
                SpawnTrees(pathObjArray[i]);
            }
        }
        if (pathEncounters[currentPathID] == 9)
        {
            SceneManager.LoadScene(1);
        }
            
        if (currentPathID + 5 < goalPathID)
        {
            switch (pathEncounters[currentPathID + 5])
            {
                case 1:
                    SpawnHostiles(pathObjArray[currentPathID + 5]);
                    break;
                case 2:
                    SpawnFriendlies(pathObjArray[currentPathID + 5]);
                    break;
                case 3:
                    SpawnTreasure(pathObjArray[currentPathID + 5]);
                    break;
            }
            if (currentPathID + 10 < goalPathID)
            {
                switch (pathEncounters[currentPathID + 10])
                {
                    case 0:
                        SpawnTrees(pathObjArray[currentPathID + 10]);
                        break;

                }
            }
        }

            
        
    }

    private void SpawnTrees(GameObject pathContainer)
    {
        MapCell cellToFire = pathContainer.GetComponent<MapCellAttachment>().cellData;
        GameObject treeListEmpty = GameObject.Find("TreeList");




        Vector3 tempV3 = new Vector3();
        GameObject objectInstantiated;
        GameObject[] testPrefab = GameManager.fetchEnvironmentDistancePrefabs();

        List<MapNode> availableCells = new List<MapNode>();
        List<MapNode> cellList = new List<MapNode>();
        //MapNode[] cellsLeft = new MapNode[availableCells.Count];


        availableCells = cellToFire.ListMapCells();

        foreach (var node in availableCells)
        {
            if (node.nodeType == "Distance")
            {
                cellList.Add(node);
            }
        }
        MapNode[] cellsLeft = cellList.ToArray();


        int j = new int();
        int k = new int();
        k = Random.Range(0, cellsLeft.Length);
        List<int> firedNodes = new List<int>();

        int maxNumberOfTrees = GameManager.maxDistanceNodesPerLevel;

        if (maxNumberOfTrees > cellsLeft.Length)
        {
            Debug.Log("Critical error 101: Not enough nodes available for spawner.");
        }
        else
        {
            for (int i = 0; i < Random.Range(1, maxNumberOfTrees); i++)
            {
                if (cellsLeft.Length > 0)
                {
                    while (firedNodes.Contains(k))
                    {
                        k = Random.Range(0, cellsLeft.Length);
                    }



                    var item = cellsLeft[k];

                    firedNodes.Add(k);
                    j = Random.Range(0, testPrefab.Length);     //selects the prefab
                    tempV3 = cellToFire.mapPosition;
                    tempV3.x += (item.nodePosX * 0.01f);
                    tempV3.y = -1.85f;
                    tempV3.z += (item.nodePosY * 0.01f);
                    objectInstantiated = Instantiate(testPrefab[j], tempV3, Quaternion.identity);
                    objectInstantiated.transform.Rotate(-90, Random.Range(0, 360), 0);
                    objectInstantiated.GetComponent<ElementDespawner>().SetSpawnDetails(currentPathID+10, this);
                    objectInstantiated.transform.SetParent(treeListEmpty.transform);



                }
            }

        }



    }
    private void SpawnHostiles(GameObject pathContainer)
    {
        MapCell cellToFire = pathContainer.GetComponent<MapCellAttachment>().cellData;


 
        Vector3 tempV3 = new Vector3();
        GameObject objectInstantiated;
        GameObject[] testPrefab = GameManager.fetchHostilePrefabs();

        List<MapNode> availableCells = new List<MapNode>();
        List<MapNode> cellList = new List<MapNode>();
        //MapNode[] cellsLeft = new MapNode[availableCells.Count];
        
    
        availableCells = cellToFire.ListMapCells();

        foreach (var node in availableCells)
        {
            if (node.nodeType == "Hostile")
            {
                cellList.Add(node);
            }
        }
        MapNode[] cellsLeft = cellList.ToArray();


        int j = new int();
        int k = new int();
        k = Random.Range(0, cellsLeft.Length);
        List<int> firedNodes = new List<int>();

        int maxNumberOfHostiles = GameManager.maxEnemiesPerCell;

        if (maxNumberOfHostiles > cellsLeft.Length)
        {
            Debug.Log("Critical error 101: Not enough nodes available for spawner.");
        }
        else
        {
            for (int i = 0; i < Random.Range(0, maxNumberOfHostiles); i++)
            {
                if (cellsLeft.Length > 0)
                {
                    while (firedNodes.Contains(k))
                    {
                        k = Random.Range(0, cellsLeft.Length);
                    }



                    var item = cellsLeft[k];

                    firedNodes.Add(k);
                    j = Random.Range(0, testPrefab.Length);     //selects the prefab
                    tempV3 = cellToFire.mapPosition;
                    tempV3.x += (item.nodePosX * 0.01f);
                    tempV3.y = -1.49f;
                    tempV3.z += (item.nodePosY * 0.01f);
                    objectInstantiated = Instantiate(testPrefab[j], tempV3, Quaternion.identity);
                    objectInstantiated.transform.Rotate(0, Random.Range(0, 360), 0);
                    
           

                }
            }

        }
            

        
    }

    private void SpawnFriendlies(GameObject pathContainer)
    {
        MapCell cellToFire = pathContainer.GetComponent<MapCellAttachment>().cellData;



        Vector3 tempV3 = new Vector3();
        GameObject objectInstantiated;
        GameObject[] testPrefab = GameManager.fetchFriendlyPrefabs();

        List<MapNode> availableCells = new List<MapNode>();
        List<MapNode> cellList = new List<MapNode>();
        //MapNode[] cellsLeft = new MapNode[availableCells.Count];
        Encounter npcEncounter;

        availableCells = cellToFire.ListMapCells();

        foreach (var node in availableCells)
        {
            if (node.nodeType == "NPC")
            {
                cellList.Add(node);
            }
        }
        MapNode[] cellsLeft = cellList.ToArray();

        int j = new int();
        int k = new int();

        int k2 = Random.Range(0, encounterArray.Length);




        k = Random.Range(0, cellsLeft.Length);
        List<int> firedNodes = new List<int>();

        int maxFriendliesPerCell = GameManager.maxFriendliesPerCell;

        if (maxFriendliesPerCell > cellsLeft.Length)
        {
            Debug.Log("Critical error 101: Not enough nodes available for spawner.");
        }
        else
        {
            for (int i = 0; i < Random.Range(1, maxFriendliesPerCell); i++)
            {
                if (cellsLeft.Length > 0)
                {
                    while (firedNodes.Contains(k))
                    {
                        k = Random.Range(0, cellsLeft.Length);
                    }



                    var item = cellsLeft[k];

                    firedNodes.Add(k);
                    j = Random.Range(0, testPrefab.Length);     //selects the prefab
                    tempV3 = cellToFire.mapPosition;
                    tempV3.x += (item.nodePosX * 0.01f);
                    tempV3.y = 1f;
                    tempV3.z += (item.nodePosY * 0.01f);
                    objectInstantiated = Instantiate(testPrefab[j], tempV3, Quaternion.identity);
                    objectInstantiated.transform.Rotate(0, Random.Range(0, 360), 0);

                    objectInstantiated.GetComponent<EncounterDecider>().InitializeNPCEncounter(encounterArray[k2]);



                }
            }

        }
    }

        void SpawnTreasure(GameObject pathContainer)
        {
            MapCell cellToFire = pathContainer.GetComponent<MapCellAttachment>().cellData;



            Vector3 tempV3 = new Vector3();
            GameObject objectInstantiated;
            GameObject[] testPrefab = GameManager.fetchTreasurePrefabs();

            List<MapNode> availableCells = new List<MapNode>();
            List<MapNode> cellList = new List<MapNode>();
            //MapNode[] cellsLeft = new MapNode[availableCells.Count];


            availableCells = cellToFire.ListMapCells();

            foreach (var node in availableCells)
            {
                if (node.nodeType == "Treasure")
                {
                    cellList.Add(node);
                }
            }
            MapNode[] cellsLeft = cellList.ToArray();


            int j = new int();
            int k = new int();
            k = Random.Range(0, cellsLeft.Length);
            List<int> firedNodes = new List<int>();

            int maxNumberOfPickups = GameManager.maxPickupsPerCell;

            if (maxNumberOfPickups > cellsLeft.Length)
            {
                Debug.Log("Critical error 101: Not enough nodes available for spawner.");
            }
            else
            {
                for (int i = 0; i < Random.Range(0, maxNumberOfPickups); i++)
                {
                    if (cellsLeft.Length > 0)
                    {
                        while (firedNodes.Contains(k))
                        {
                            k = Random.Range(0, cellsLeft.Length);
                        }



                        var item = cellsLeft[k];

                        firedNodes.Add(k);
                        j = Random.Range(0, testPrefab.Length);     //selects the prefab
                        tempV3 = cellToFire.mapPosition;
                        tempV3.x += (item.nodePosX * 0.01f);
                        tempV3.y = 1f;
                        tempV3.z += (item.nodePosY * 0.01f);
                        objectInstantiated = Instantiate(testPrefab[j], tempV3, Quaternion.identity);
                        objectInstantiated.transform.Rotate(0, Random.Range(0, 360), 0);



                    }
                }

            }
        }
    }
        
    