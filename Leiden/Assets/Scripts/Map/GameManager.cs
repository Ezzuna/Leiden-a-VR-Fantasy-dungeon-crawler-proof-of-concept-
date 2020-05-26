using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = new GameManager();
    static GameObject[] prefabArrayEnvironmental = new GameObject[2];
    static GameObject[] prefabArrayEnvironmentalDistance = new GameObject[1];
    static GameObject[] prefabArrayMapTerrain = new GameObject[7];
    static GameObject[] prefabArrayPickups = new GameObject[3];
    static GameObject[] prefabHostiles = new GameObject[2];
    static GameObject[] prefabFriendlies = new GameObject[1];
    static GameObject[] prefabTreasure = new GameObject[1];
    static GameObject[] prefabWallLights = new GameObject[5];
    


    public static int maxNodesPerCell = 9;
    public static int maxEnemiesPerCell = 4;
    public static int maxPickupsPerCell = 2;
    public static int maxFriendliesPerCell = 1;
    public static int maxPickupsPerLevel = 5;
    public static int maxDistanceNodesPerLevel = 6;

    static public int seedForRNG = 12345678;


    // make sure the constructor is private, so it can only be instantiated here
    private GameManager()
    {
    }

    private void Start()
    {

        ConstructEnvironmentalPrefabList(prefabArrayEnvironmental);
        ConstructTerrainPrefabList(prefabArrayMapTerrain);
    }


    public static GameObject[] ConstructTerrainPrefabList(GameObject[] prefabArrayEnvironmental) //needs expanding to contain all prefabs for map at the least
    {


            prefabArrayMapTerrain[0] = Resources.Load("Prefabs/Map/Wall1", typeof(GameObject)) as GameObject;
            prefabArrayMapTerrain[1] = Resources.Load("Prefabs/Map/Wall2", typeof(GameObject)) as GameObject;
            prefabArrayMapTerrain[2] = Resources.Load("Prefabs/Map/WallCorner1", typeof(GameObject)) as GameObject;
            prefabArrayMapTerrain[3] = Resources.Load("Prefabs/Map/WallCorner2", typeof(GameObject)) as GameObject;
            prefabArrayMapTerrain[4] = Resources.Load("Prefabs/Map/Cube", typeof(GameObject)) as GameObject;
            prefabArrayMapTerrain[5] = Resources.Load("Prefabs/Map/Wall1Fire", typeof(GameObject)) as GameObject;
            prefabArrayMapTerrain[6] = Resources.Load("Prefabs/Map/Wall2Fire", typeof(GameObject)) as GameObject;

        return prefabArrayMapTerrain;


    }
    public static GameObject[] ConstructWallLightsPrefabList(GameObject[] prefabWallLights) //needs expanding to contain all prefabs for map at the least
    {


        prefabWallLights[0] = Resources.Load("Particles/Environment/FireBlue", typeof(GameObject)) as GameObject;
        prefabWallLights[1] = Resources.Load("Particles/Environment/FireCyan", typeof(GameObject)) as GameObject;
        prefabWallLights[2] = Resources.Load("Particles/Environment/FireGreen", typeof(GameObject)) as GameObject;
        prefabWallLights[3] = Resources.Load("Particles/Environment/FireYellow", typeof(GameObject)) as GameObject;
        prefabWallLights[4] = Resources.Load("Particles/Environment/FirePink", typeof(GameObject)) as GameObject;
        


        return prefabWallLights;


    }


    public static GameObject[] ConstructEnvironmentalPrefabList(GameObject[] prefabArrayEnvironmental) //needs expanding to contain all prefabs for map at the least
    {


            prefabArrayEnvironmental[0] = Resources.Load("Prefabs/Environmental/Stone4u", typeof(GameObject)) as GameObject;
            prefabArrayEnvironmental[1] = Resources.Load("Prefabs/Environmental/WoodenCrate", typeof(GameObject)) as GameObject;
            //prefabArrayEnvironmental[2] = Resources.Load("Prefabs/Environmental/Torch", typeof(GameObject)) as GameObject;
          

        return prefabArrayEnvironmental;


    }
    public static GameObject[] ConstructEnvironmentalDistancePrefabList(GameObject[] prefabArrayDistanceEnvironmental) //needs expanding to contain all prefabs for map at the least
    {


        prefabArrayDistanceEnvironmental[0] = Resources.Load("Prefabs/Environmental/Tree01", typeof(GameObject)) as GameObject;

        return prefabArrayDistanceEnvironmental;


    }

    public static GameObject[] ConstructPickupsPrefabList(GameObject[] prefabArrayPickups)
    {
        prefabArrayPickups[0] = Resources.Load("Prefabs/Pickups/Bottle_Health", typeof(GameObject)) as GameObject;
        prefabArrayPickups[1] = Resources.Load("Prefabs/Pickups/Bottle_Mana", typeof(GameObject)) as GameObject;
        prefabArrayPickups[2] = Resources.Load("Prefabs/Pickups/Bottle_Endurance", typeof(GameObject)) as GameObject;

        return prefabArrayPickups;
    }



    public static GameObject[] ConstructHostilesPrefabList(GameObject[] prefabHostiles) //needs expanding to contain all prefabs for map at the least
    {


        prefabHostiles[0] = Resources.Load("Prefabs/Hostiles/ARKHAN_01", typeof(GameObject)) as GameObject;
        prefabHostiles[1] = Resources.Load("Prefabs/Hostiles/Turret_01", typeof(GameObject)) as GameObject;


        return prefabHostiles;


    }
    public static GameObject[] ConstructFriendlyPrefabList(GameObject[] prefabFriendlies) //needs expanding to contain all prefabs for map at the least
    {


       prefabFriendlies[0] = Resources.Load("Prefabs/NPCs/NPC", typeof(GameObject)) as GameObject;


        return prefabFriendlies;


    }

    public static GameObject[] ConstructTreasurePrefabList(GameObject[] prefabTreasure) //needs expanding to contain all prefabs for map at the least
    {


        //prefabTreasure[0] = Resources.Load("Prefabs/Map/Wall1", typeof(GameObject)) as GameObject;
        prefabTreasure[0] = Resources.Load("Prefabs/Treasure/Chest", typeof(GameObject)) as GameObject;

        return prefabTreasure;


    }

    

    public static GameObject[] fetchWallLightsPrefabs()
    {
        if (prefabWallLights[0] == null)
        {
            prefabWallLights = ConstructWallLightsPrefabList(prefabWallLights);
        }
        return prefabWallLights;
    }

    public static GameObject[] fetchMapTerrainPrefabs()
    {
        if (prefabArrayMapTerrain[0] == null)
        {
            prefabArrayMapTerrain = ConstructTerrainPrefabList(prefabArrayMapTerrain);
        }
        return prefabArrayMapTerrain;
    }


    public static GameObject[] fetchMapPickupPrefabs()
    {
        if (prefabArrayPickups[0] == null)
        {
            prefabArrayPickups = ConstructPickupsPrefabList(prefabArrayPickups);
        }
        return prefabArrayPickups;
    }

    public static GameObject[] fetchEnvironmentPrefabs()
    {
        if (prefabArrayEnvironmental[0] == null)
        {
            prefabArrayEnvironmental = ConstructEnvironmentalPrefabList(prefabArrayEnvironmental);
        }
        return prefabArrayEnvironmental;
    }

    public static GameObject[] fetchEnvironmentDistancePrefabs()
    {
        if (prefabArrayEnvironmentalDistance[0] == null)
        {
            prefabArrayEnvironmentalDistance = ConstructEnvironmentalDistancePrefabList(prefabArrayEnvironmentalDistance);
        }
        return prefabArrayEnvironmentalDistance;
    }

    public static GameObject[] fetchHostilePrefabs()
    {
        if (prefabHostiles[0] == null)
        {
            prefabHostiles = ConstructHostilesPrefabList(prefabHostiles);
        }
        return prefabHostiles;
    }
    public static GameObject[] fetchFriendlyPrefabs()
    {
        if (prefabFriendlies[0] == null)
        {
            prefabFriendlies = ConstructFriendlyPrefabList(prefabFriendlies);
        }
        return prefabFriendlies;
    }

    public static GameObject[] fetchTreasurePrefabs()
    {
        if (prefabTreasure[0] == null)
        {
            prefabTreasure = ConstructTreasurePrefabList(prefabHostiles);
        }
        return prefabTreasure;
    }



}


 
