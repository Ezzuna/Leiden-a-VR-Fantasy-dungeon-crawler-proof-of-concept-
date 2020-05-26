using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterDecider : MonoBehaviour
{
    public NPC spawnedNPC;
    public Encounter npcEncounterDetails;




    public void InitializeNPCEncounter(Encounter encounterDetails)
    {
        npcEncounterDetails = encounterDetails;
        spawnedNPC = this.GetComponent<NPC>();
        spawnedNPC.mData = new string[3];
        spawnedNPC.mData[0] = encounterDetails.EncounterText1;
        Debug.Log(encounterDetails.EncounterText1);
        spawnedNPC.mData[1] = encounterDetails.EncounterText2;
        spawnedNPC.mData[2] = encounterDetails.EncounterText3;
        

        //spawnedNPC.mData = npcText;
    }

    public void PlayerConfirmed()
    {
        /* Using a coroutine here to allow us to store the function name in text in the XML document, allowing more designer power. Due to the string method of StartCoroutine only taking 1 object
         * we are forced to carry things as an array. Comes with other downsided but within the scope of altering stats and player inventory it is appropriate. 
         */
        int[] toss = new int[] { npcEncounterDetails.EncounterArg1, npcEncounterDetails.EncounterArg2 };
        Debug.Log("attempting NPC");
        Debug.Log("string + " + npcEncounterDetails.EncounterFunction);
        npcEncounterDetails.StartCoroutine(npcEncounterDetails.EncounterFunction, toss);
        

    }
}
