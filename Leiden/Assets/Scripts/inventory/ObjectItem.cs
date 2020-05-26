using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectItem : MonoBehaviour

{
    public int objId;
    public string objName;
    public int count;
    public string note;
    public int ItemPool;
    public bool isCanAdd;
    public int maxAdd;
    public string Icon;

    public bool isChecked;

    // Use this for initialization
    void Start()
    {
        isChecked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isChecked)
        {
            if (GetComponent<MeshRenderer>()!=null)
            {
                GetComponent<MeshRenderer>().material.color = Color.red;
            }
            else
            {
                for (int i = 0; i < this.transform.childCount; i++)
                {
                    if (this.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>() != null)
                    {
                        this.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                    }

                }

            }
            
        }
        else
        {
            if (GetComponent<MeshRenderer>() != null)
            {
                GetComponent<MeshRenderer>().material.color = Color.white;
            }
            else
            {
                for (int i = 0; i < this.transform.childCount; i++)
                {
                    if (this.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>() != null)
                    {
                        this.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
                    }

                }
            }
            
        }
        isChecked = false;
    }


}
