using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    public int EncounterID;
    public string EncounterName;
    public string EncounterModelName;
    public string EncounterFunction;
    public int EncounterArg1;
    public int EncounterArg2;
    public string EncounterText1;
    public string EncounterText2;
    public string EncounterText3;



  
    public IEnumerator BuyItem(int[] passedInts)
    {
        UIInventory inventory = GameObject.Find("Invntory").GetComponent<UIInventory>();
        Pack pack = GameObject.Find("Wand").GetComponent<Pack>();
        ObjectItem Object = AllObjectItem.instance.GetObjectItem(passedInts[0]);
        if (passedInts[1] <= inventory.getCurCurrency())
         {
            inventory.alterCurrency(-passedInts[1]);

            pack.getItem(Object);

        }
        else
        {
            Debug.Log("Player gold is not enough");
        }
        yield return null;
    }

    public IEnumerator SellHealthForItem(int[] passedInts)
    {
        PlayerCharacterController hp = GameObject.Find("OVRCameraRig").GetComponent<PlayerCharacterController>();
        Pack pack = GameObject.Find("Wand").GetComponent<Pack>();        
        ObjectItem Object = AllObjectItem.instance.GetObjectItem(passedInts[0]);
        if (hp.getCurHealth()> passedInts[1])
        {
            hp.AdjustCurHealth(-passedInts[1]);

            pack.getItem(Object);

        }
        else
        {
            Debug.Log("Player health is not enough");
        }
        yield return null;
    }

    public IEnumerator SellHealthForMana(int[] passedInts) 
    {
        PlayerCharacterController playerCharacter = GameObject.Find("OVRCameraRig").GetComponent<PlayerCharacterController>();
        Pack pack = GameObject.Find("Wand").GetComponent<Pack>();
        ObjectItem Object = AllObjectItem.instance.GetObjectItem(passedInts[0]);
        if (playerCharacter.getCurHealth() > passedInts[1])
        {
            playerCharacter.AdjustCurHealth(-passedInts[1]);

            playerCharacter.AdjustMaxMana(passedInts[0]);

        }
        else
        {
            Debug.Log("Player health is not enough");
        }
        yield return null;
    }
}
