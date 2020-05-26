using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class MapCell
{
    // public MapCell[] mapCell;
    // Start is called before the first frame update
    public int mapID;
    public int pathID;
    public Vector3 mapPosition;
    public int entryDirection;
    public int exitDirection;
    public GameObject gameObject;
    public int cellPrefabID;
    public bool toBeDrawn;
    public Vector3 mapRotation;
    public List<MapNode> mapCellNodes;


    public MapCell()
    { 
    

   
    }

    public List<MapNode> ListMapCells()
    {
        return this.mapCellNodes;
    }




}
