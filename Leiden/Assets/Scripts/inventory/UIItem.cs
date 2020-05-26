using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class UIItem : MonoBehaviour
{
    public Sprite[] Icons;
    public Image m_Icon;
    public Text m_TxtName;
    public Text m_TxtCount;
    private int id;
    private Pack pack;



    public void SetInfo(ItemEntity item)
    {
        this.m_TxtName.text = item.ObjName;
        this.m_Icon.sprite = GetIcon(item.Icon);
        this.m_TxtCount.text = item.Count.ToString();
        this.id = item.ObjId;
        if (item.Count<=0)
        {
            Destroy(this.gameObject);
        }
        
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
            case AllEnum.Goods.Bottle_Endurance:
                return Icons[3];
            case AllEnum.Goods.Bottle_Health:
                return Icons[4];
            case AllEnum.Goods.Bottle_Mana:
                return Icons[5];
            default:
                return Icons[0];

        }
    }
    public void OnBtnClick()
    {
        //Use item
        pack = GameObject.Find("Wand").GetComponent<Pack>();
        pack.useItem(id);
       
    }
}
