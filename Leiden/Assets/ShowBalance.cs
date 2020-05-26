using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowBalance : MonoBehaviour
{
    public Text balance;
    public UIInventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        updateBalance();
    }

    // Update is called once per frame
    void Update()
    {
        updateBalance();
    }
    public void updateBalance()
    {
        balance.text = inventory.getCurCurrency().ToString();
    }
}
