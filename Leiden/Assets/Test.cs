using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public NewObjectPooler objectPooler;
    // Start is called before the first frame update
    void Start()
    {
        objectPooler.Reset();
        List<DataClass.GoodData> datas = new List<DataClass.GoodData>();
        DataClass.GoodData data1 = new DataClass.GoodData();
        data1.name = "goodA";
        data1.info = "this is good A";
        data1.icon = "A";
        data1.price = 100;
        data1.isSelled = false;

        DataClass.GoodData data2 = new DataClass.GoodData();
        data2.name = "goodB";
        data2.info = "this is good B";
        data2.icon = "B";
        data2.price = 130;
        data2.isSelled = false;

        DataClass.GoodData data3 = new DataClass.GoodData();
        data3.name = "goodC";
        data3.info = "this is good C";
        data3.icon = "C";
        data3.price = 200;
        data3.isSelled = false;

        DataClass.GoodData data4 = new DataClass.GoodData();
        data4.name = "goodA";
        data4.info = "this is good A";
        data4.icon = "A";
        data4.price = 100;
        data4.isSelled = true;


        datas.Add(data1);
        datas.Add(data2);
        datas.Add(data3);
        datas.Add(data4);

        for (int i = 0; i < datas.Count; i++)
        {
            GameObject obj = objectPooler.GetPooledGameObject();
            obj.SetActive(true);
            UIGoodItem item = obj.GetComponent<UIGoodItem>();
            item.SetData(datas[i]);
        }
    }

 
}
