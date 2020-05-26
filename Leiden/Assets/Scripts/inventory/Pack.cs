using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pack : MonoBehaviour
{
    //List array holding items
    public List<ItemEntity> items = null;
    //Backpack maximum capacity
    public int maxItem = 32;
    public GameObject Inventory;
    private UIInventory UIInventory;
    // Use this for initialization
    void Start()
    {
        items = new List<ItemEntity>();
        UIInventory = Inventory.GetComponent<UIInventory>();
    }

    //Pick up items
    public ObjectItem getItem(ObjectItem item)
    {
        //Entity classes using intermediate transitions
        ItemEntity itemEntity = ItemEntity.FillData(item);

        //If items cannot be merged
        if (!itemEntity.IsCanAdd)
        {
           
            if (items.Count < maxItem)
            {
                //Get item
                items.Add(itemEntity);
                item.count = 0;
            }
            else
            {
                //Pickup failed
            }
        }
        else
        {
            //When empty backpack, add
            if (items.Count < 1)
            {
                items.Add(itemEntity);
                item.count = 0;
            }
            else
            {
                //Determine the current backpack merged item
                foreach (ItemEntity currItem in items)
                {
                    //Identical items, can be stacked, and the group is not full
                    if (currItem.ObjId.Equals(itemEntity.ObjId) && currItem.Count < currItem.MaxAdd)
                    {
                        //Quantity superposition
                        currItem.Count = currItem.Count + itemEntity.Count;
                        
                        if (currItem.Count - currItem.MaxAdd > 0)
                        {
                            
                            item.count = currItem.Count - currItem.MaxAdd;
                            itemEntity.Count = item.count;
                           
                            currItem.Count = currItem.MaxAdd;
                        }
                        else
                        {
                            
                            item.count = 0;
                        }
                    }
                    else
                    {
                        
                        continue;
                    }
                }
                
                if (item.count > 0 && items.Count < maxItem)
                {
                    items.Add(itemEntity);
                    item.count = 0;
                }
            }
        }
        return item;
    }
    public void useItem(int id)
    {
        foreach (ItemEntity currItem in items)
        {
            if (currItem.ObjId == id) 
            {

                currItem.Count--;
                
                currItem.use();
                
                showPack();
                Debug.Log("use");
            }
        }
                
    }
   
    public void showPack()
    {
        UIInventory.cleanGrid();

        string show = "Item：\n";
        int i = 0;
        foreach (ItemEntity currItem in items)
        {
            
            show += ++i + " [" + currItem.ObjName + "], Count: " + currItem.Count + "\n";
            Transform grid = UIInventory.GetEnmptyGrid();
            if (grid == null)
                return;
            ItemEntity item = currItem as ItemEntity;
            GameObject go = GameObject.Instantiate(UIInventory.m_ItemPrefab);
            go.GetComponent<UIItem>().SetInfo(item);
            go.transform.SetParent(grid);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
            go.transform.localRotation = UIInventory.transform.localRotation;
        }

        Debug.Log(show);
    }

    public ItemEntity findItem(int id)
    {
        foreach (ItemEntity currItem in items)
        {
            if (currItem.ObjId == id)
            {
                return currItem;
            }
        }
        return null;
    }
    public bool IfInclude(int id)
    {
        foreach (ItemEntity currItem in items)
            if (currItem.ObjId == id)
            {
                return true;
            }
        return false;
    }
}
