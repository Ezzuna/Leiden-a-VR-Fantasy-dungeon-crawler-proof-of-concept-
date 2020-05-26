using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllObjectItem : MonoBehaviour
{
    public static AllObjectItem instance;
    public List<ObjectItem> items = null;

    private void Start()
    {
        instance = this;
        items = new List<ObjectItem>();
    }

    public ObjectItem GetObjectItem(int objId)
    {
        foreach (var item in items)
        {
            if (objId == item.objId)
            {
                return item;
            }
            else
            {
                Debug.Log("Can't find objcet");
            }
        }
        
        return null;
    }
}
