using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnShop : MonoBehaviour
{

    List<DataClass.GoodData> datas = new List<DataClass.GoodData>();
  /**  void Test()
    {
        DataClass.GoodData data1 = new DataClass.GoodData();
        data1.name = "goodA";
        data1.info = "this is good A";
        data1.icon = "A";
        data1.price = 100;
        data1.isSelled = false;
        data1.IsCanAdd = true;

        DataClass.GoodData data2 = new DataClass.GoodData();
        data2.name = "goodB";
        data2.info = "this is good B";
        data2.icon = "B";
        data2.price = 120;
        data2.isSelled = false;
        data2.IsCanAdd = true;

        DataClass.GoodData data3 = new DataClass.GoodData();
        data3.name = "goodC";
        data3.info = "this is good C";
        data3.icon = "C";
        data3.price = 200;
        data3.isSelled = false;
        data3.IsCanAdd = true;


        DataClass.GoodData data4 = new DataClass.GoodData();
        data4.name = "goodA";
        data4.info = "this is good A";
        data4.icon = "A";
        data4.price = 100;
        data4.isSelled = false;
        data4.IsCanAdd = true;

        DataClass.GoodData data5 = new DataClass.GoodData();
        data5.name = "goodA";
        data5.info = "this is good A";
        data5.icon = "A";
        data5.price = 100;
        data5.isSelled = true;
        data5.IsCanAdd = true;

        DataClass.GoodData data6 = new DataClass.GoodData();
        data6.name = "goodA";
        data6.info = "this is good A";
        data6.icon = "A";
        data6.price = 100;
        data6.isSelled = false;
        data6.IsCanAdd = true;



        datas.Add(data1);
        datas.Add(data2);
        datas.Add(data3);
        datas.Add(data4);
        datas.Add(data5);
        datas.Add(data6);


    }*/
    public void OnBtnClick()
    {
        //Test();
        //UIShop.Instance.Show(datas);
        UIShop.Instance.Show(TestServer.Instance.GetBuyDatas());
    }
}
