using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class UIGoodItem : MonoBehaviour
{
    public Text txt_Name;
    public Text txt_Info;
    public Image img_Icon;
    public Text txt_Pricee;
    public GameObject go_IsSelled;

    public Sprite[] Icons;

    DataClass.GoodData goodData;

    int price;
    public void SetData(DataClass.GoodData data)
    {
        goodData = data;
        txt_Name.text = data.name;
        txt_Info.text = data.info;
        txt_Pricee.text = data.price.ToString();
        price = data.price;
        img_Icon.sprite = GetIcon(data.icon);
        CheckIsSelled(data.isSelled);
    }
    Sprite GetIcon(string icon)
    {
        AllEnum.Goods goodType = (AllEnum.Goods)Enum.Parse(typeof(AllEnum.Goods), icon);
        switch (goodType)
        {
            case AllEnum.Goods.A:
                return Icons[0];
            case AllEnum.Goods.B:
                return Icons[1];
            case AllEnum.Goods.C:
                return Icons[2]; 
            default:
                return Icons[0];

        }
    }
    void CheckIsSelled(bool isSelled)
    {
        if (isSelled)
        {
            go_IsSelled.SetActive(true);
        }
        else
        {
            go_IsSelled.SetActive(false);
        }
    }

 
    public void OnBtnBuyClick()
    {
        UIInventory inventory = GameObject.Find("Invntory").GetComponent<UIInventory>();
        if (price<=inventory.getCurCurrency())
        {
            inventory.alterCurrency(-price);
            Debug.Log("Buy");
            TestServer.Instance.BuyGoodRequest(goodData.id);
        }
       
    }
}
