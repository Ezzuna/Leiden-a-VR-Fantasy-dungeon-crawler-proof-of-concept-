using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShopOperationManager : MonoBehaviour
{
    public UIGoodsPan uiGoodsPan;
    public UIShopDataManager dataManager;
    private bool isOpen = false;
    List<DataClass.GoodData> datas = new List<DataClass.GoodData>();
    private void Update()
    {
        
        openShop();
    }
   

    public void SetData()
    {
        uiGoodsPan.SetData(dataManager.GetData());
    }

    public void OnBtnRefreshClick()
    {
        TestServer.Instance.RefreshRequest();
    }
    public void OnBtnCloseClick()
    {
        UIShop.Instance.Hide();
    }
    void openShop()
    {
       
      /*  if ( Input.GetKeyDown(KeyCode.P))
        {
            if (!isOpen)
            {
                UIShop.Instance.Show(datas);
                isOpen = true;
            }
            else if (isOpen)
            {
                UIShop.Instance.Hide();
                isOpen = false;
            }
        }*/
        
    }
}
