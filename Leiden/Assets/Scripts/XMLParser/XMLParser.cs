using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class XMLParser : MonoBehaviour
{
    //static List<MapCell> mapCells = new List<MapCell>();

    List<Spell> spellList = new List<Spell>();
    Spell spellListTemp;
    public String[] nodeNames = new string[] { "spName", "spSlot","spDamage",
                                                "spPrefab","spSize","spIntensity",
                                                "spColour","spDescription","spStoreCost","spItemPool"};
    // Start is called before the first frame update
    void Start()
    {
        spellList = new List<Spell>();
        
        
        XMLLoop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    bool XMLNodeCheck(XmlNode node, String[] testArray)
    {
        
        if (node.ChildNodes.Count != testArray.Length)
        {
            Debug.Log("Error discovered with XML node within file: " + node.BaseURI + " On element: " + node.OuterXml.Substring(0,15));

            return false;
        }
        else
        {
            return true;
        }




        
    }

    void XMLLoop()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load("config2.xml");


        foreach (XmlNode node in doc.DocumentElement.ChildNodes)            //Desperately requires error handling and input control, but it's late and we'll do that later
        {
            if (XMLNodeCheck(node, nodeNames))
            {

                spellListTemp = new Spell();
                Color temp = new Color();
                spellListTemp.spellID = int.Parse(node.Attributes["id"].Value);
                spellListTemp.spName = node.ChildNodes[0].InnerText;
                spellListTemp.spSlot = int.Parse(node.ChildNodes[1].InnerText);
                spellListTemp.spDamage = float.Parse(node.ChildNodes[2].InnerText);
                spellListTemp.spPrefab = node.ChildNodes[3].InnerText;
                spellListTemp.spSize = float.Parse(node.ChildNodes[4].InnerText);
                spellListTemp.spIntensity = float.Parse(node.ChildNodes[5].InnerText);
                if (ColorUtility.TryParseHtmlString(node.ChildNodes[6].InnerText, out temp))
                {
                    spellListTemp.spColour = temp;
                }
                else
                {
                    spellListTemp.spColour = Color.white;
                }
                spellListTemp.spDescription = node.ChildNodes[7].InnerText;
                spellListTemp.spStoreCost = int.Parse(node.ChildNodes[8].InnerText);
                spellListTemp.spItemPool = int.Parse(node.ChildNodes[9].InnerText);
                spellList.Add(spellListTemp);
            }
        }

            


        

        foreach(var item in spellList)
        {
            Debug.Log(item.spDescription);
        }
    }
}
