using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Spell
{
    public int spellID;
    public string spName;
    public int spSlot;
    public float spDamage;
    public string spPrefab;     //Select a model to use by name
    public float spSize;            //Select scale value
    public float spIntensity;       //Select intesity of visuals (primarily for lowering alpha or intensifying particle effects)
    public Color spColour;
    public string spDescription;
    public int spStoreCost;
    public int spItemPool;          //Selects which item pool the item is in

}
