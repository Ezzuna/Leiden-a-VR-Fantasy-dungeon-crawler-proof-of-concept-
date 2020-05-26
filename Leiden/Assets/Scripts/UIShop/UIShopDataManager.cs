using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShopDataManager: MonoBehaviour
{
    List<DataClass.GoodData> datas = new List<DataClass.GoodData>();
   public void SetData(List<DataClass.GoodData> _datas)
    {
        this.datas = _datas;
    }

    public List<DataClass.GoodData> GetData()
    {
        return this.datas;
    }
}
