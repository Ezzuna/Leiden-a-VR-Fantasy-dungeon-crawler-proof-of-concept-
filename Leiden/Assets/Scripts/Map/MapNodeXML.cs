using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class MapNodeXML : MonoBehaviour
{
    //static List<MapCell> mapCells = new List<MapCell>();

    static List<MapNode> nodeList = new List<MapNode>();
    static MapNode nodeListTemp;
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


    static public List<MapNode> FetchNodes(int cellPrefabID)
    {

        List<MapNode> j = XMLLoop(cellPrefabID);






        return j;
    }

    static List<MapNode> XMLLoop(int cellPrefabID)
    {
        nodeList = new List<MapNode>();
        XmlDocument doc = new XmlDocument();
        switch (cellPrefabID)
        {
            case 1: doc.Load("MapNodes.xml");
                break;
            case 2: doc.Load("MapNodes.xml");
                break;
            default: doc.Load("MapNodes.xml");
                break;

        }
        


        foreach (XmlNode node in doc.DocumentElement.ChildNodes)            //Desperately requires error handling and input control, but it's late and we'll do that later
        {


                nodeListTemp = new MapNode();

            nodeListTemp.nodeID = int.Parse(node.Attributes["id"].Value);
            nodeListTemp.nodeType = node.ChildNodes[0].InnerText;
            nodeListTemp.nodePosX = (int.Parse(node.ChildNodes[1].InnerText)) * 1.5f;
            nodeListTemp.nodePosY = (int.Parse(node.ChildNodes[2].InnerText)) * 1.5f;


            nodeList.Add(nodeListTemp);

        }



        return nodeList;

/*
        foreach (var item in nodeList)
        {
            Debug.Log(item.nodeID);
        }
        */
    }
}
