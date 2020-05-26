using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestServer : MonoBehaviour
{
    static TestServer _instance;
    List<DataClass.GoodData> buyDatas = new List<DataClass.GoodData>();
    public Pack pack;
    public static TestServer Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance==null)
        {
            _instance = this;
        }
    }

    public void RefreshRequest()
    {
        Debug.Log("RefreshRequest");
        StartCoroutine(IRefreshRequest());
        //StartCoroutine("IRefreshRequest");
    }

    IEnumerator IRefreshRequest()
    {
        yield return new WaitForSeconds(1f);
        RefreshResponse();
    }

    public void RefreshResponse()
    {
        Debug.Log("RefreshResponse");
        UIShop.Instance.Show(GetDatas());
    }

    List<DataClass.GoodData> GetDatas()
    {
        return buyDatas;
    }

   

    private void Start()
    {
        DataClass.GoodData data1 = new DataClass.GoodData();
        data1.name = "goodA";
        data1.info = "this is good A";
        data1.icon = "A";
        data1.price = 100;
        data1.isSelled = false;
        data1.id = 1;
        data1.maxAdd = 100;
        data1.count = 2;
        data1.isCanAdd = true;

        DataClass.GoodData data2 = new DataClass.GoodData();
        data2.name = "goodB";
        data2.info = "this is good B";
        data2.icon = "A";
        data2.price = 100;
        data2.isSelled = false;
        data2.id = 1;
        data2.maxAdd = 100;
        data2.count = 1;
        data2.isCanAdd = true;

        DataClass.GoodData data3 = new DataClass.GoodData();
        data3.name = "goodC";
        data3.info = "this is good C";
        data3.icon = "C";
        data3.price = 200;
        data3.isSelled = false;
        data3.id = 3;
        data3.maxAdd = 100;
        data3.count = 1;
        data3.isCanAdd = true;

        DataClass.GoodData data4 = new DataClass.GoodData();
        data4.name = "goodd";
        data4.info = "this is good A";
        data4.icon = "A";
        data4.price = 100;
        data4.isSelled = true;
        data4.id = 4;
        data4.maxAdd = 100;
        data4.count = 1;
        data4.isCanAdd = true;

        DataClass.GoodData data5 = new DataClass.GoodData();
        data5.name = "goode";
        data5.info = "this is good B";
        data5.icon = "B";
        data5.price = 130;
        data5.isSelled = false;
        data5.id = 5;
        data5.maxAdd = 100;
        data5.count = 1;
        data5.isCanAdd = true;

        DataClass.GoodData data6 = new DataClass.GoodData();
        data6.name = "goodf";
        data6.info = "this is good A";
        data6.icon = "A";
        data6.price = 100;
        data6.isSelled = false;
        data6.id = 6;
        data6.maxAdd = 100;
        data6.count = 1;
        data6.isCanAdd = true;

        buyDatas.Add(data1);
        buyDatas.Add(data2);
        buyDatas.Add(data3);
        buyDatas.Add(data4);
        buyDatas.Add(data5);
        buyDatas.Add(data6);
    }

    public List<DataClass.GoodData> GetBuyDatas()
    {
        return buyDatas;
    }
    public void BuyGoodRequest(int id)
    {
        Debug.Log("BuyGoodRequest");
        BuyGood(id);
        StartCoroutine(IBuyGood());
    }

    IEnumerator IBuyGood()
    {
        yield return new WaitForSeconds(1f);
        BuyGoodResponse();
    }

    public void BuyGoodResponse()
    {
        Debug.Log("BuyGoodResponse");
        UIShop.Instance.Show(GetBuyDatas());
        pack.showPack();
    }

    void BuyGood(int id)
    {
        for (int i = 0; i < buyDatas.Count; i++)
        {
            if (id==buyDatas[i].id && buyDatas[i].isSelled==false)
            {
                buyDatas[i].isSelled = true;
                ObjectItem Object = buyDatas[i].TurnToObjectItem(buyDatas[i]);
                pack.getItem(Object);
                pack.showPack();
                break;
            }
        }
    }
}
