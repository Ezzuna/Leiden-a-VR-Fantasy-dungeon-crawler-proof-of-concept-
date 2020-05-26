using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShop : MonoBehaviour
{
   
    static UIShop _Instance;
    public static UIShop Instance
    {
        get
        {
            return _Instance;
        }
    }

    public GameObject go_Menu;
    public UIShopDataManager dataManager;
    public UIShopOperationManager operationManager;
    private void Awake()
    {
        if (_Instance==null)
        {
            _Instance = this;
        }
    }
    
    public void Show(List<DataClass.GoodData> datas)
    {
        go_Menu.SetActive(true);
        dataManager.SetData(datas);
        operationManager.SetData();
    }

    public void Hide()
    {
        go_Menu.SetActive(false);
    }
}
