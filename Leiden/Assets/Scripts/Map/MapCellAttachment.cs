using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCellAttachment : MonoBehaviour
{

    public MapCell cellData;
    public int pathID;
    public int didItWork = 0;

    void InitializeVariables()
    {
        pathID = cellData.pathID;
        didItWork = cellData.cellPrefabID;
    }
}
