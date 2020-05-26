using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGoodsPan : MonoBehaviour
{
    public NewObjectPooler newObjectPooler;

    public void SetData(List<DataClass.GoodData> datas)
    {
        newObjectPooler.Reset();

        for (int i = 0; i < datas.Count; i++)
        {
            GameObject obj = newObjectPooler.GetPooledGameObject();
            obj.SetActive(true);
            UIGoodItem item = obj.GetComponent<UIGoodItem>();
            item.SetData(datas[i]);
        }
    }
}
