using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentalNodeFirer : MonoBehaviour
{
    public static void FireNodes(List<MapCell> mapCells, string nodeSelection)
    {
        int maxNumberOfNodes = 0;
        int maxNumberOfPotions = 0;
        GameObject environmentalEmpty = GameObject.Find("EnvironmentalItems");
        GameObject[] testPrefab = new GameObject[0];
        switch (nodeSelection)
        {
            case "Environmental": testPrefab = GameManager.fetchEnvironmentPrefabs();
                                maxNumberOfNodes = GameManager.maxNodesPerCell;
                                break;
            case "Pickup": testPrefab = GameManager.fetchMapPickupPrefabs();
                                maxNumberOfNodes = GameManager.maxPickupsPerCell;
                                maxNumberOfPotions = GameManager.maxPickupsPerLevel;
                                break; 
            default:
                
                                  break;
        }
        
        
        List<int> spawnedNodes = new List<int>();

        Vector3 tempV3 = new Vector3();
        GameObject objectInstantiated; 

        List<MapNode> availableCells = new List<MapNode>();
        List<MapNode> cellList = new List<MapNode>();
        bool criticalRoll = true;
        int potionCounter = 0;

        foreach (var itemList in mapCells)
        {

            availableCells = itemList.ListMapCells();

            foreach (var node in availableCells)
            {
                if (node.nodeType == nodeSelection)
                {
                    cellList.Add(node);
                }
            }
            MapNode[] cellsLeft = cellList.ToArray();

            int j = new int();
            int k = new int();

            if (nodeSelection == "Pickup" && potionCounter <= maxNumberOfPotions)
            {
                k = Random.Range(0, 100);
                if (k > 96)
                {
                    criticalRoll = true;
                    potionCounter++;
                }
                else criticalRoll = false;
            }
            else if (nodeSelection == "Pickup" && potionCounter > maxNumberOfPotions) criticalRoll = false;

            if (criticalRoll)
            {
                k = Random.Range(0, cellsLeft.Length);
                spawnedNodes = new List<int>();

                if (maxNumberOfNodes > cellsLeft.Length)
                {
                    Debug.Log("Critical error 101: Not enough nodes available for spawner.");
                }
                else
                {
                    for (int i = 0; i < Random.Range(0, maxNumberOfNodes); i++)
                    {
                        if (cellsLeft.Length > i)
                        {
                            while (spawnedNodes.Contains(k))
                            {
                                k = Random.Range(0, cellsLeft.Length);
                            }


                            var item = cellsLeft[k];

                            spawnedNodes.Add(k);
                            j = Random.Range(0, testPrefab.Length);     //selects the prefab
                            tempV3 = itemList.mapPosition;
                            spawnedNodes.Add(item.nodeID);
                            tempV3.x += (item.nodePosX * 0.01f);
                            tempV3.y = -1.5f;
                            tempV3.z += (item.nodePosY * 0.01f);
                            objectInstantiated = Instantiate(testPrefab[j], tempV3, Quaternion.identity);
                            objectInstantiated.transform.Rotate(0, Random.Range(0, 360), 0);
                            objectInstantiated.transform.SetParent(environmentalEmpty.transform);

                        }
                    }

                }
            }
            


        }
    }




}
