using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;

public class EncounterNodeXML : MonoBehaviour
{
    //static List<MapCell> mapCells = new List<MapCell>();

    static List<Encounter> EncounterList = new List<Encounter>();
    static Encounter nodeListTemp;
    public String[] nodeNames = new string[] { "spName", "spSlot","spDamage",
                                                "spPrefab","spSize","spIntensity",
                                                "spColour","spDescription","spStoreCost","spItemPool"};
    // Start is called before the first frame update
    void Start()
    {



        // XMLLoop();
    }

    // Update is called once per frame
    void Update()
    {

    }


    bool XMLNodeCheck(XmlNode node, String[] testArray)
    {

        if (node.ChildNodes.Count != testArray.Length)
        {
            Debug.Log("Error discovered with XML node within file: " + node.BaseURI + " On element: " + node.OuterXml.Substring(0, 15));

            return false;
        }
        else
        {
            return true;
        }





    }


    public List<Encounter> FetchNodes()
    {

        List<Encounter> j = XMLLoop();






        return j;
    }

    static List<Encounter> XMLLoop()
    {
        EncounterList = new List<Encounter>();
        XmlDocument doc = new XmlDocument();

        doc.Load("Encounter.xml");


        



        foreach (XmlNode node in doc.DocumentElement.ChildNodes)            //Desperately requires error handling and input control, but it's late and we'll do that later
        {


            nodeListTemp = GameObject.Find("EncounterList").AddComponent<Encounter>();

            nodeListTemp.EncounterID = int.Parse(node.Attributes["id"].Value);
            nodeListTemp.EncounterName = node.ChildNodes[0].InnerText;
            nodeListTemp.EncounterModelName = node.ChildNodes[1].InnerText;
            nodeListTemp.EncounterFunction = node.ChildNodes[2].InnerText;
            nodeListTemp.EncounterArg1 = int.Parse(node.ChildNodes[3].InnerText);
            nodeListTemp.EncounterArg2= int.Parse(node.ChildNodes[4].InnerText);
            nodeListTemp.EncounterText1 = node.ChildNodes[5].InnerText;
            nodeListTemp.EncounterText2 = node.ChildNodes[6].InnerText;
            nodeListTemp.EncounterText3 = node.ChildNodes[7].InnerText;



            EncounterList.Add(nodeListTemp);

        }



        return EncounterList;

        /*
                foreach (var item in nodeList)
                {
                    Debug.Log(item.nodeID);
                }
                */
    }
}
