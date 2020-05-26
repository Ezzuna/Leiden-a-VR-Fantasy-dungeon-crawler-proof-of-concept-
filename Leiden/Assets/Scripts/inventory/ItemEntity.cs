using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemEntity : MonoBehaviour
{
    private int objId;
    private string objName;
    private int count;
    private string note;
    private int itemPool;
    private bool isCanAdd;
    private int maxAdd;
    private bool isChecked;
    private string icon;



    public static ItemEntity FillData(ObjectItem item)
    {
        ItemEntity itemEntity = new ItemEntity();
        itemEntity.ObjId = item.objId;
        itemEntity.ObjName = item.objName;
        itemEntity.Count = item.count;
        itemEntity.Note = item.note;
        itemEntity.ItemPool = item.ItemPool;
        itemEntity.IsCanAdd = item.isCanAdd;
        itemEntity.MaxAdd = item.maxAdd;
        itemEntity.IsChecked = item.isChecked;
        itemEntity.icon = item.Icon;
        return itemEntity;
    }

    public int ObjId
    {
        get
        {
            return objId;
        }

        set
        {
            objId = value;
        }
    }

    public string ObjName
    {
        get
        {
            return objName;
        }

        set
        {
            objName = value;
        }
    }

    public int Count
    {
        get
        {
            return count;
        }

        set
        {
            count = value;
        }
    }

    public string Note
    {
        get
        {
            return note;
        }

        set
        {
            note = value;
        }
    }

    public int ItemPool
    {
        get
        {
            return itemPool;
        }

        set
        {
            itemPool = value;
        }
    }

    public bool IsCanAdd
    {
        get
        {
            return isCanAdd;
        }

        set
        {
            isCanAdd = value;
        }
    }

    public int MaxAdd
    {
        get
        {
            return maxAdd;
        }

        set
        {
            maxAdd = value;
        }
    }

    public bool IsChecked
    {
        get
        {
            return isChecked;
        }

        set
        {
            isChecked = value;
        }
    }
    public string Icon
    {
        get
        {
            return icon;
        }

        set
        {
            icon = value;
        }
    }

  public void use()
    {
        switch (ObjId)
        {
            case 1:
                IronShoelaces();
                break;

            case 2:
                SteelCufflinks();
                break;

            case 3:
                MithrilBreastplate();
                break;
            case 4:
                IronMaidensDress();
                break;

            case 5:
                Jugger_knotScoutUniform();
                break;

            case 6:
                BlackRodofDespair();
                break;
            case 7:
                IronSpellFocus();
                break;

            case 8:
                ShardofReality();
                break;

            case 9:
                Solidarity();
                break;
            case 10:
                GlassCannon();
                break;

            case 11:
                ACOGWandAttachment();
                break;

            case 12:
                ManaChalice();
                break;
            case 13:
                GobletofNotQuiteFire();
                break;

            case 14:
                LithiumBatteries();
                break;
            case 15:
                InfinniiiiitteePowwwaaa();
                break;

            case 16:
                SustainableEnergy();
                break;

            case 17:
                RockVest();
                break;
            case 18:
                AFamilyHeirloom();
                break;

            case 19:
                MarvinsMarvelousMagicOil();
                break;
            case 20:
                Bottle_Endurance();
                break;

            case 21:
                Bottle_Health();
                break;

            case 22:
                Bottle_Mana();
                break;

            default:
                break;
        }
    }
    
    public void IronShoelaces()
    {
        PlayerCharacterController player = GameObject.Find("OVRCameraRig").GetComponent<PlayerCharacterController>();
        player.AdjustMaxHealth(20);
    }

    public void SteelCufflinks()
    {
        PlayerCharacterController player = GameObject.Find("OVRCameraRig").GetComponent<PlayerCharacterController>();
        player.AdjustMaxHealth(50);
        GameObject firePoint = GameObject.Find("firePoint");
        int count = firePoint.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            firePoint.transform.GetChild(i).gameObject.GetComponent<Spells>().AdjustDamage(-10);
        }
    }

    public void MithrilBreastplate()
    {
        PlayerCharacterController player = GameObject.Find("OVRCameraRig").GetComponent<PlayerCharacterController>();
        player.AdjustMaxHealth(50);
    }

    public void IronMaidensDress()
    {
        PlayerCharacterController player = GameObject.Find("OVRCameraRig").GetComponent<PlayerCharacterController>();
        player.AdjustMaxHealth(35);
    }

    public void Jugger_knotScoutUniform()
    {
        PlayerCharacterController player = GameObject.Find("OVRCameraRig").GetComponent<PlayerCharacterController>();
        player.AdjustMaxHealth(100);
    }

    public void BlackRodofDespair()
    {
        GameObject firePoint = GameObject.Find("firePoint");
        int count = firePoint.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            firePoint.transform.GetChild(i).gameObject.GetComponent<Spells>().AdjustDamage(10);
        }
    }

    public void IronSpellFocus()
    {
        GameObject firePoint = GameObject.Find("firePoint");
        int count = firePoint.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            firePoint.transform.GetChild(i).gameObject.GetComponent<Spells>().AdjustDamage(10);
        }
    }

    public void ShardofReality()
    {
        GameObject firePoint = GameObject.Find("firePoint");
        int count = firePoint.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            firePoint.transform.GetChild(i).gameObject.GetComponent<Spells>().AdjustDamage(20);
        }
    }

    public void Solidarity()
    {
        GameObject firePoint = GameObject.Find("firePoint");
        int count = firePoint.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            firePoint.transform.GetChild(i).gameObject.GetComponent<Spells>().AdjustDamage(35);
        }

        PlayerCharacterController player = GameObject.Find("OVRCameraRig").GetComponent<PlayerCharacterController>();
        player.AdjustMaxHealth(-10);
    }

    public void GlassCannon()
    {
        GameObject firePoint = GameObject.Find("firePoint");
        int count = firePoint.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            firePoint.transform.GetChild(i).gameObject.GetComponent<Spells>().AdjustDamage(75);
        }

        PlayerCharacterController player = GameObject.Find("OVRCameraRig").GetComponent<PlayerCharacterController>();
        float adj = player.getMaxHealth() / 2;
        player.AdjustMaxHealth(-adj);
    }

    public void ACOGWandAttachment()
    {
        GameObject firePoint = GameObject.Find("firePoint");
        int count = firePoint.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            firePoint.transform.GetChild(i).gameObject.GetComponent<Spells>().AdjustDamage(15);
        }
    }

    public void ManaChalice()
    {
        PlayerCharacterController player = GameObject.Find("OVRCameraRig").GetComponent<PlayerCharacterController>();
        player.AdjustMaxMana(20);
    }

    public void GobletofNotQuiteFire()
    {
        PlayerCharacterController player = GameObject.Find("OVRCameraRig").GetComponent<PlayerCharacterController>();
        player.AdjustMaxMana(30);
    }

    public void LithiumBatteries()
    {
        PlayerCharacterController player = GameObject.Find("OVRCameraRig").GetComponent<PlayerCharacterController>();
        player.AdjustMaxMana(50);
    }

    public void InfinniiiiitteePowwwaaa()
    {
        PlayerCharacterController player = GameObject.Find("OVRCameraRig").GetComponent<PlayerCharacterController>();
        player.AdjustMaxMana(100);
        float adj = player.getMaxHealth() / 4;
        player.AdjustMaxHealth(-adj);
    }

    public void SustainableEnergy()
    {
        PlayerCharacterController player = GameObject.Find("OVRCameraRig").GetComponent<PlayerCharacterController>();
        
        float adj = player.getMaxMana() / 1;
        player.AdjustMaxMana(+adj);

        GameObject firePoint = GameObject.Find("firePoint");
        int count = firePoint.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            firePoint.transform.GetChild(i).gameObject.GetComponent<Spells>().AdjustDamage(-(firePoint.transform.GetChild(i).gameObject.GetComponent<Spells>().spDamage / 2));
        }
    }

    public void RockVest()
    {
        PlayerCharacterController player = GameObject.Find("OVRCameraRig").GetComponent<PlayerCharacterController>();

        float adj = player.getMaxHealth() / 4;
        player.AdjustMaxHealth(+adj);

        float adj1 = player.getMaxMana() / 4;
        player.AdjustMaxMana(-adj1);


    }

    public void AFamilyHeirloom()
    {
        UIInventory inventory = GameObject.Find("Invntory").GetComponent<UIInventory>();
        inventory.alterScore(+2000);
    }

    public void MarvinsMarvelousMagicOil()
    {
        //Win the game
    }

    public void Bottle_Endurance()
    {
        UIInventory inventory = GameObject.Find("Invntory").GetComponent<UIInventory>();
        inventory.alterCurrency(50);
    }

    public void Bottle_Health()
    {
        PlayerCharacterController player = GameObject.Find("OVRCameraRig").GetComponent<PlayerCharacterController>();
        player.AdjustCurHealth(50);
    }

    public void Bottle_Mana()
    {
        PlayerCharacterController player = GameObject.Find("OVRCameraRig").GetComponent<PlayerCharacterController>();
        player.AdjustCurMana(50);
    } 
}
