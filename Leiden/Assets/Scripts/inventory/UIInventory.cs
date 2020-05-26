using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIInventory : MonoBehaviour
{
    public static int gridCount = 20; //背包格子数量

    public GameObject m_GridPrefab; //格子预制体
    public GameObject m_ItemPrefab; //物品预制体
    public Transform m_PnlGrid; //背包里层
    private RectTransform m_PnlGridRectTransfom; //背包里层的RectTransform，用于动态调整它的大小
    private List<Transform> m_GridList = new List<Transform>(); //所有格子
    
    private Pack pack; //从缓存里取出的ItemList
    private bool isfull = false;
    private int currency = 400;
    private int Score = 0;
    private GameObject primarySpell;
    private GameObject secondarySpell;
    public GameObject[] Spells = new GameObject[2];
    public Text Scoretext;
    void Start()
    {
       
        InitWidget();
        InitGrid();
       
    }

    void Update()
    {
        BagClear();
        Scoretext.text = "Score: " + Score.ToString();
    }

    private void isFull()
    {
        int isFullInt = 0;
        for (int i = 0; i < gridCount; i++)
        {
            if (m_GridList[i].childCount > 0)
            {
                isFullInt++;
            }
            if (isFullInt == gridCount)
            {
                isfull = true;
            }
        }
        Debug.Log("BagisFull?" + isfull);
    }



    private void InitWidget()
    {

        this.m_PnlGridRectTransfom = m_PnlGrid.GetComponent<RectTransform>();
        this.pack = GameObject.Find("Wand").GetComponent<Pack>();
        Spells[0] = primarySpell;
        Spells[1] = secondarySpell;
    }

    private void InitGrid()
    {
        //动态创建Grid
        for (int i = 0; i < gridCount; i++)
        {
            GameObject grid = GameObject.Instantiate(this.m_GridPrefab);
            grid.transform.SetParent(m_PnlGrid);
            m_GridList.Add(grid.transform);
        }
    }

   

    public Transform GetEnmptyGrid()
    {
       
        // return m_GridList.Find(x => x.childCount == 0);
        for (int i = 0; i < gridCount; i++)
        {
            if (m_GridList[i].childCount == 0)
            {
                return m_GridList[i];
            }
        }
         return null;
    }
    public void cleanGrid()
    {
        for (int i = 0; i < gridCount; i++)
        {
            
            int childCount = m_GridList[i].childCount;
            for (int j = 0; j < childCount; j++)
            {
                Destroy(m_GridList[i].GetChild(j).gameObject);
            }

        }
       
    }


    public void BagClear()
    {
        int k = 0;
        int i = 0;
        for (i = 0; i < gridCount; i++)
        {
            if (m_GridList[i].childCount == 0)
            {
                k = i;
                break;
            }
            
        }
        
        for (int j = i; j < gridCount; j++)
        {
            if (m_GridList[j].childCount > 0)
            {
                GameObject go1 = m_GridList[j].GetChild(0).gameObject;
                go1.transform.SetParent(m_GridList[k]);
                go1.transform.localPosition = Vector3.zero;
                break;
            }
        }
    }

    public int getCurCurrency()
    {
        return this.currency;
    }

    public void alterCurrency(int offset)
    {
        this.currency += offset;
    }

    public void alterScore(int offset)
    {
        this.Score += offset;
    }

    public int getScore()
    {
        return this.Score;
    }
    /*public void AddItem(int id)
    {
        Transform grid = GetEnmptyGrid();
        if (grid == null)
            return;
       
        //if (!pack.findItem(id))
         //   return;
      
        ItemEntity item = pack.findItem(id) as ItemEntity;
        GameObject go = GameObject.Instantiate(this.m_ItemPrefab);
        go.GetComponent<UIItem>().SetInfo(item);
        go.transform.SetParent(grid);
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = Vector3.one;
        go.transform.localRotation = this.transform.localRotation;
        

        for (int i=0; i<m_items.Count; i++)
        {
            if(m_items[i].GetComponent<UIItem>().m_TxtName.text == item.ObjName)
            {
                Destroy(go);
                m_items.RemoveAll(x => x == null);
                m_items[i].GetComponent<UIItem>().SetInfo(item);
            }
          

        }
        if (go != null)
        {
            m_items.Add(go);
        }

    }*/
    public GameObject[] GetSpell()
    {

        return Spells;
    }
}
