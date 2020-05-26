using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackController : MonoBehaviour
{
    private Transform tr;
    //背包类
    private Pack pack;
    private UIInventory ui;
    

    // Use this for initialization
    void Start()
    {
        tr = GetComponent<Transform>();
        pack = GetComponent<Pack>();
        ui= GameObject.Find("Invntory").GetComponent<UIInventory>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.B))
        {
            pack.showPack();
            ui.BagClear();
        }

        Debug.DrawRay(tr.position, tr.forward * 2.0f, Color.green);
        RaycastHit hit;
        if (Physics.Raycast(tr.position, tr.forward, out hit, 2.0f))
        {
            //Debug.Log ("射线击中:" + hit.collider.gameObject.name + "\n tag:" + hit.collider.tag);
            GameObject gameObj = hit.collider.gameObject;
            ObjectItem obj = (ObjectItem)gameObj.GetComponent<ObjectItem>();
            if (obj != null)
            {
                obj.isChecked = true;
                //Debug.Log(obj.objName);
                
                    
                    obj = pack.getItem(obj);
                    
                    
                    pack.showPack();
                    
                    
                    if (obj.count == 0)
                    {
                        //gameObj.SetActive(false);
                        Destroy(gameObj);
                    }
                
            }
        }
    }
}
